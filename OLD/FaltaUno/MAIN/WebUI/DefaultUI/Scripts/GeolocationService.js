(function (root, factory) {
   "use strict";

   /*global define*/
   if (typeof define === 'function' && define.amd) {
      define(['angular', 'geolocationService'], factory);  // AMD
   } else if (typeof module === 'object' && module.exports) {
      module.exports = factory(require('angular'), require('geolocationService')); // Node
   } else {
      factory(root.angular, root.swal);					// Browser
   }
}(this, function (angular, swal) {
   "use strict";

   angular.module('myb.ngGeolocationService', [])

      // 2. set a constant
      .constant('MODULE_VERSION', '1.0.0')

      .factory('geolocationService', ['$q', '$http',
         function ($q, $http) {
            var options = {
               apis: {
                  google: {
                     geocode: {
                        url: '//maps.googleapis.com/maps/api/geocode/',
                        format: 'json',
                        limit: {
                           second: 5,
                           day: 2500,
                           month: 75000,
                        }
                     },
                     places: {
                        url: '//maps.googleapis.com/maps/api/place/autocomplete/',
                        format: 'json',
                        limit: {
                           second: 5,
                           day: 1000,
                           month: 30000,
                        }
                     },
                     key: 'AIzaSyAka_0ldZdeIraL-9XkPAyqPa7Qr1bEQNs',
                  },
                  tomtom: {
                     geocode: {
                        url: '//api.tomtom.com/search/2/search/',
                        format: 'json',
                        limit: {
                           second: 5,
                           day: 2500
                        }
                     },
                     key: 'XIEMOWT0gLlJQ9RMRhU86AFHIYvXpWjr'
                  },
                  mapbox: {
                     places: {
                        url: '//api.mapbox.com/geocoding/v5/mapbox.places/',
                        format: 'json',
                        limit: {
                           day: 50000
                        }
                     },
                     key: 'pk.eyJ1IjoiYm9uYTMwMDAiLCJhIjoiY2ptamJseHRoMGR5OTNwbXZmZXc3bWprMCJ9._O6ouzfZxf5Om5_GVxMChQ'
                  },
                  arcgis: {
                     suggest: {
                        url: '//geocode.arcgis.com/arcgis/rest/services/World/GeocodeServer/suggest',
                        format: 'json',
                        maxSuggestions: 5
                     },
                     geocode: {
                        url: '//geocode.arcgis.com/arcgis/rest/services/World/GeocodeServer/findAddressCandidates',
                        format: 'json',
                        maxLocations: 5,
                        forStorage: false,
                        outSr: 4326,
                        outFields: '*'
                     }
                  }
               }
            };

            var _wsGooggleGeocode = function () {
               var canceler = $q.defer();

               return {
                  get: function (oData) {
                     canceler.resolve("cancelled");
                     canceler = $q.defer();
                     return $http.get(options.apis.google.geocode.url + options.apis.google.geocode.format,
                        {
                           params: {
                              address: oData,
                              key: options.apis.google.key
                           },
                           timeout: canceler.promise
                        });
                  }
               }
            };

            var _wsTomTom = function () {
               var canceler = $q.defer();

               return {
                  get: function (oData) {
                     canceler.resolve("cancelled");
                     canceler = $q.defer();
                     return $http.get(options.apis.tomtom.geocode.url + oData.searchText + '.' + options.apis.tomtom.geocode.format,
                        {
                           params: {
                              key: options.apis.tomtom.key,
                              lat: oData.lat,
                              lon: oData.lon
                           },
                           timeout: canceler.promise
                        });
                  }
               }
            };

            var _wsMapBox = function () {
               var canceler = $q.defer();

               return {
                  get: function (oData, parameters) {
                     canceler.resolve("cancelled");
                     canceler = $q.defer();
                     return $http.get(options.apis.mapbox.places.url + oData + '.' + options.apis.mapbox.places.format,
                        {
                           params: {
                              access_token: options.apis.mapbox.key
                           },
                           timeout: canceler.promise
                        });
                  }
               };
            };

            var cancelerWsArcGISSuggest = $q.defer();
            var _wsArcGISSuggest = function () {
               return {
                  get: function (oData, parameters) {
                     var prms = {
                        f: options.apis.arcgis.suggest.format,
                        maxSuggestions: options.apis.arcgis.suggest.maxSuggestions,
                        text: oData
                     };
                     if (parameters) {
                        prms.location = parameters.lat + ',' + parameters.lng;
                     }

                     //TODO: angular extend parameters in prms

                     cancelerWsArcGISSuggest.resolve("cancelled");
                     cancelerWsArcGISSuggest = $q.defer();
                     return $http.get(options.apis.arcgis.suggest.url,
                        {
                           params: prms,
                           timeout: cancelerWsArcGISSuggest.promise
                        });
                  }
               }
            };

            var _wsArcGISGeocode = function () {
               var canceler = $q.defer();

               return {
                  get: function (oData, parameters) {
                     var prms = {
                        f: options.apis.arcgis.geocode.format,
                        maxLocations: options.apis.arcgis.suggest.maxLocations,
                        singleLine: oData
                     };

                     //TODO: angular extend parameters in prms

                     canceler.resolve("cancelled");
                     canceler = $q.defer();
                     return $http.get(options.apis.arcgis.geocode.url,
                        {
                           params: prms,
                           timeout: canceler.promise
                        });
                  }
               }
            };

            var _searchLocation = function (data, parameters) {
               return _wsArcGISGeocode().get(data).then(function (arcGISResponse) {
                  var result = arcGISResponse
                     .data
                     .suggestions
                     .map(function (item) {
                        return {
                           LocationID: 0,
                           Display: item.text,
                           Lat: item.latitude,
                           Lng: item.longitude,
                           Value: item.text,
                           PlaceID: item.magicKey
                        };
                     }) || [];

                  return result;
               }, function (errorResponse) {
                  return {
                     LocationID: 0,
                     Display: data,
                     Lat: null,
                     Lng: null,
                     Value: data,
                     PlaceID: null
                  }
               });
            };

            var _getLocation = function (data, parameters) {
               return _wsArcGISGeocode().get(data).then(function (arcGISResponse) {
                  var result = {
                     LocationID: 0,
                     Display: data,
                     Lat: null,
                     Lng: null,
                     Value: data,
                     PlaceID: null
                  };

                  if (arcGISResponse != null && arcGISResponse.data != null && arcGISResponse.data.candidates.length > 0) {
                     var candidate0 = arcGISResponse
                        .data
                        .candidates[0];

                     result = {
                        LocationID: 0,
                        Display: candidate0.address,
                        Lat: candidate0.location.y,
                        Lng: candidate0.location.x,
                        Value: candidate0.address,
                        PlaceID: candidate0.magicKey
                     };
                  }

                  return result;
               }, function (errorResponse) {
                  return {
                     LocationID: 0,
                     Display: data,
                     Lat: null,
                     Lng: null,
                     Value: data,
                     PlaceID: null
                  }
               });
            };

            var _suggestLocation = function (data, parameters) {
               return _getCurrentLocation().then(function (location) {
                  parameters = {
                     lat: location.latitude,
                     lng: location.longitude
                  };
                  return _suggestLocationByLN(data, parameters);
               },
                  function (error) {
                  });
            };

            var _suggestLocationByLN = function (data, parameters) {
               return _wsArcGISSuggest().get(data, parameters).then(
                  function (arcGISResponse) {
                     var result = arcGISResponse
                        .data
                        .suggestions
                        .map(function (item) {
                           return {
                              LocationID: 0,
                              Display: item.text,
                              Lat: null,
                              Lng: null,
                              Value: item.text,
                              PlaceID: item.magicKey
                           };
                        }) || [];

                     return result;
                  },
                  function (errorResponse) {
                     return {
                        LocationID: 0,
                        Display: data,
                        Lat: null,
                        Lng: null,
                        Value: data,
                        PlaceID: null
                     }
                  }
               );
            };

            //haversine distance
            var _calculateDistance = function (lat1, lng1, lat2, lng2) {
               //a = sin²(Δφ / 2) + cos φ1 ⋅ cos φ2 ⋅ sin²(Δλ / 2)
               //c = 2 ⋅ atan2(√a, √(1−a) )
               //d = R ⋅ c

               var R = 6371e3; // metres
               var φ1 = lat1.toRadians();
               var φ2 = lat2.toRadians();
               var Δφ = (lat2 - lat1).toRadians();
               var Δλ = (lng2 - lng1).toRadians();

               var a = Math.sin(Δφ / 2) * Math.sin(Δφ / 2) +
                  Math.cos(φ1) * Math.cos(φ2) *
                  Math.sin(Δλ / 2) * Math.sin(Δλ / 2);
               var c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));

               var d = R * c;

               return d;
            };

            var _getCurrentCity = function () {
               return NearestCity(_getCurrentLocation());
            };

            var _currentGeoLocation = {
               accuracy: 0,
               altitude: null,
               altitudeAccuracy: null,
               heading: null,
               latitude: 0,
               longitude: 0,
               speed: null
            };

            var _getCurrentLocation = function (options) {
               var defered = $q.defer();

               var options = {
                  enableHighAccuracy: true,
                  timeout: 5000,
                  maximumAge: 0
               };

               var _success = function (position) {
                  _currentGeoLocation = position.coords;
               };

               var _error = function (err, def) {
                  if (location.protocol == 'http') {
                     return $.ajax('http://ip-api.com/json').then(
                        function success(response) {
                           _currentGeoLocation = {
                              accuracy: 0,
                              altitude: null,
                              altitudeAccuracy: null,
                              heading: null,
                              latitude: response.lat,
                              longitude: response.lon,
                              speed: null
                           };
                           def.resolve(_currentGeoLocation);
                        },
                        function fail(data, status) {
                           def.reject(data);
                        });
                  }
                  else {
                     return $.ajax('https://ipapi.co/json').then(
                        function success(response) {
                           _currentGeoLocation = {
                              accuracy: 0,
                              altitude: null,
                              altitudeAccuracy: null,
                              heading: null,
                              latitude: response.latitude,
                              longitude: response.longitude,
                              speed: null
                           };
                           def.resolve(_currentGeoLocation);
                        },
                        function fail(data, status) {
                           def.reject(data);
                        });
                  }
               };

               if (navigator && navigator.geolocation) {
                  navigator.geolocation.getCurrentPosition(function (position) {
                     _success(position);
                     defered.resolve(position.coords);
                  },
                     function (err) {
                        _error(err, defered);
                     });
               };

               return defered.promise;
            };

            return {
               calculateDistance: _calculateDistance,
               getCurrentCity: _getCurrentCity,
               getCurrentLocation: _getCurrentLocation,
               searchLocation: _searchLocation,
               suggestLocation: _suggestLocation,
               getLocation: _getLocation
            };
         }
      ])

      .directive('mybLeafletMap', ['$compile','geolocationService',
         function ($compile, geolocationService) {
            var _makeid = function (length) {
               var result = '';
               var characters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz';
               var charactersLength = characters.length;
               for (var i = 0; i < length; i++) {
                  result += characters.charAt(Math.floor(Math.random() * charactersLength));
               }
               return result;
            };

            var uniqueid = _makeid(20);

            var _setMapLocationByIP = function (scope) {
               geolocationService.getCurrentLocation().then(function (currentLocationResponse) {
                  scope.leafletOptions[uniqueid].mapLocation = {
                     lat: currentLocationResponse.latitude,
                     lng: currentLocationResponse.longitude,
                     zoom: 10
                  };
                  scope.leafletOptions[uniqueid].mapMarker = {};
               });
            };

            var _setMapLocation = function (location, scope) {
               //var result = JSON.stringify(location, null, 2);
               if (location != null) {
                  scope.leafletOptions[uniqueid].mapLocation = {
                     lat: location.Lat,
                     lng: location.Lng,
                     zoom: 15
                  };

                  scope.leafletOptions[uniqueid].mapMarker = {
                     locationSearch: {
                        lat: location.Lat,
                        lng: location.Lng,
                        focus: true
                     }
                  };
               } else {
                  _setMapLocationByIP(scope);
               }
            };

            

            return {
               restrict: 'E',
               require: '?ngModel',
               template: '<leaflet width="{{leafletOptions.' + uniqueid + '.width}}" height="{{leafletOptions.' + uniqueid + '.height}}" markers="leafletOptions.' + uniqueid + '.mapMarker" lf-center="leafletOptions.' + uniqueid + '.mapLocation" defaults="leafletOptions.' + uniqueid +'.defaults"></leaflet>',
               compile: function (element, attributes) {

                  return {
                     pre: function (scope, element, attributes, controller, transcludeFn) {
                        if (!scope.leafletOptions) {
                           scope.leafletOptions = {};
                        }
                        scope.leafletOptions[uniqueid] = {
                           defaults: attributes["defaults"],
                           height: attributes["height"] ? attributes["height"]: "180px",
                           width: attributes["width"] ? attributes["width"] : "100%",
                           mapLocation : {
                              lat: 0,
                              lng: 0,
                              zoom: 15
                           },
                           mapMarker: {}
                        };


                        _setMapLocationByIP(scope);
                     },
                     post: function (scope, element, attributes, controller, transcludeFn) {
                        scope.$watch(attributes.ngModel,
                           function (newValue, oldValue, scope) {
                              if (newValue
                                 && (!oldValue
                                    || (newValue.Lat != oldValue.Lat && newValue.Lng != oldValue.Lng)))
                                 _setMapLocation(newValue, scope);
                           });
                     }
                  };
               }
            };
         }
      ]);
}));