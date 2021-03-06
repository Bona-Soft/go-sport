app
   .factory('ws_UserActiveSport', ['$rootScope', '$http', 'settings', function ($rootScope, $http, settings) {
      return {
         post: function (oData) {
            if (!oData) oData = {}; oData.SportID = $rootScope.getSportSelected().SportID;
            return $http.post(settings.wsVirtualPath + '/UserActiveSport.aspx', oData);
         },
         get: function (oData) {
            if (!oData) oData = {}; oData.SportID = $rootScope.getSportSelected().SportID;
            return $http.get(settings.wsVirtualPath + '/UserActiveSport.aspx?', { params: oData });
         }
      };
   }])
   .factory('ws_Sport', ['$rootScope', '$http', 'settings', function ($rootScope, $http, settings) {
      return {
         get: function (oData) {
            if (!oData) oData = {}; oData.SportID = $rootScope.getSportSelected().SportID;
            return $http.get(settings.wsVirtualPath + '/Sport.aspx?', { params: oData });
         }
      };
   }])
   .factory('ws_MatchTypes', ['$rootScope', '$http', 'settings', function ($rootScope, $http, settings) {
      return {
         get: function (oData) {
            if (!oData) oData = {}; oData.SportID = $rootScope.getSportSelected().SportID;
            return $http.get(settings.wsVirtualPath + '/MatchTypes.aspx?', { params: oData });
         }
      };
   }])
   .factory('ws_Fields', ['$rootScope', '$http', 'settings', function ($rootScope, $http, settings) {
      return {
         get: function (oData) {
            if (!oData) oData = {}; oData.SportID = $rootScope.getSportSelected().SportID;
            return $http.get(settings.wsVirtualPath + '/Fields.aspx?', { params: oData });
         }
      };
   }])
   .factory('ws_ChallengeTypes', ['$rootScope', '$http', 'settings', function ($rootScope, $http, settings) {
      return {
         get: function (oData) {
            if (!oData) oData = {}; oData.SportID = $rootScope.getSportSelected().SportID;
            return $http.get(settings.wsVirtualPath + '/ChallengeTypes.aspx?', { params: oData });
         }
      };
   }])
   .factory('ws_Location', ['$rootScope', '$http', 'settings', '$q', function ($rootScope, $http, settings, $q) {
      var canceler = $q.defer();

      return {
         get: function (oData) {
            canceler.resolve("cancelled");
            canceler = $q.defer();
            if (!oData) oData = {}; oData.SportID = $rootScope.getSportSelected().SportID;
            return $http.get('//maps.googleapis.com/maps/api/geocode/json', //settings.wsVirtualPath + '/Location.aspx?',
               {
                  //params: oData,
                  params: {
                     address: oData
                  },
                  timeout: canceler.promise
               });
         }
      };
   }])
   .factory('ws_Headquarter', ['$rootScope', '$http', 'settings', '$q', function ($rootScope, $http, settings, $q) {
      var canceler = $q.defer();

      return {
         post: function (oData) {
            if (!oData) oData = {}; oData.SportID = $rootScope.getSportSelected().SportID;
            return $http.post(settings.wsVirtualPath + '/Headquarter.aspx', oData);
         },
         get: function (oData) {
            canceler.resolve("cancelled");
            canceler = $q.defer();
            if (!oData) oData = {}; oData.SportID = $rootScope.getSportSelected().SportID;
            return $http.get(settings.wsVirtualPath + '/Headquarter.aspx?', { params: oData, timeout: canceler.promise });
         }
      };
   }])
   .factory('ws_PlayerSearch', ['$rootScope', '$http', 'settings', '$q', function ($rootScope, $http, settings, $q) {
      var canceler = $q.defer();

      return {
         get: function (oData) {
            canceler.resolve("cancelled");
            canceler = $q.defer();
            if (!oData) oData = {}; oData.SportID = $rootScope.getSportSelected().SportID;
            return $http.get(settings.wsVirtualPath + '/PlayerSearch.aspx?', { params: oData, timeout: canceler.promise });
         }
      };
    }])
   .factory('ws_TopPlayer', ['$rootScope', '$http', 'settings', function ($rootScope, $http, settings) {
        return {
           get: function (oData) {
              if (!oData) oData = {}; oData.SportID = $rootScope.getSportSelected().SportID;
                return $http.get(settings.wsVirtualPath + '/TopPlayer.aspx', { params: oData });
            }
        };
    }])
   .factory('ws_UserNews', ['$rootScope', '$http', 'settings', function ($rootScope, $http, settings) {
      return {
         get: function (oData) {
            if (!oData) oData = {}; oData.SportID = $rootScope.getSportSelected().SportID;
            return $http.get(settings.wsVirtualPath + '/UserNews.aspx', { params: oData });
         }
      };
   }]);

app
   .factory('inputMethods', ['$filter','ws_Location', 'ws_Headquarter', 'ws_PlayerSearch', 'geolocationService',
      function ($filter, ws_Location, ws_Headquarter, ws_PlayerSearch, geolocationService) {
         var _getLocationGroups = function (_searchText) {
            var data = {
               searchText: _searchText,
               groupNumber: 1
            };

            var _getHeadquarterByLatLng = function (headquarter, lat, lng) {
               return (headquarter.filter(function (hqItem) {
                  var _apiLat = lat < 0 ? Math.ceil(lat * 10000) / 10000 : Math.floor(lat * 10000) / 10000;
                  var _apiLng = lng < 0 ? Math.ceil(lng * 10000) / 10000 : Math.floor(lng * 10000) / 10000;
                  var _hqLat = hqItem.Location.Lat < 0 ? Math.ceil(hqItem.Location.Lat * 10000) / 10000 : Math.floor(hqItem.Location.Lat * 10000) / 10000;
                  var _hqLng = hqItem.Location.Lng < 0 ? Math.ceil(hqItem.Location.Lng * 10000) / 10000 : Math.floor(hqItem.Location.Lng * 10000) / 10000;

                  return (_hqLat === _apiLat && _hqLng === _apiLng);
               })[0]);
            };

            return geolocationService.suggestLocation(data.searchText).then(function (locationResponse) {
               return ws_Headquarter.get(data).then(function (headquarterResponse) {
                  var _headquarters = headquarterResponse.data;

                  _auxLocationResponse = locationResponse;
                  if (!Array.isArray(locationResponse)) {
                     _auxLocationResponse = [locationResponse];
                  }

                  var _locationGroup = _auxLocationResponse
                     .map(function (item) {
                        var _headquarter = _getHeadquarterByLatLng(_headquarters, item.Lat, item.Lng);

                        return {
                           Location: {
                              LocationID: _headquarter == null ? 0 : _headquarter.Location.LocationID,
                              Display: item.Display,
                              Lat: item.Lat,
                              Lng: item.Lng,
                              Value: item.Value,
                              PlaceID: item.PlaceID
                           },
                           Headquarter: _headquarter
                        };
                     }) || [];

                  var _headquartersProcessedAsLocationGroup = _headquarters.map(function (item) {
                     return {
                        Location: {
                           LocationID: item.LocationID,
                           Display: item.Location.Display,
                           Lat: item.Location.Lat,
                           Lng: item.Location.Lng,
                           Value: item.Location.Value
                        },
                        Headquarter: item
                     }
                  }) || [];

                  _locationGroup = _locationGroup.concat(_headquartersProcessedAsLocationGroup);

                  var _locationGroupDistinct = [];

                  var _headquarterIdInJsonArr = function (jsonArr, headquarterID) {
                     var found = false;
                     $.each(jsonArr, function (index, lgJsonArr) {
                        if (lgJsonArr.Headquarter != null && headquarterID == lgJsonArr.Headquarter.HeadquarterID)
                           found = true;
                     });
                     return found;
                  };

                  $.each(_locationGroup, function (index, locationGroupItem) {
                     if (locationGroupItem.Headquarter == null || !_headquarterIdInJsonArr(_locationGroupDistinct, locationGroupItem.Headquarter.HeadquarterID)) {
                        _locationGroupDistinct.push(locationGroupItem);
                     }
                  });

                  return _locationGroupDistinct;
               },
                  function (headquarterErrorResponse) {
                     _auxLocationResponse = locationResponse;
                     if (!Array.isArray(locationResponse)) {
                        _auxLocationResponse = [locationResponse];
                     }
                     var _location = _auxLocationResponse
                        .map(function (item) {
                           console.log(item);
                           return {
                              Location: {
                                 LocationID: 0,
                                 Display: item.Display,
                                 Lat: item.Lat,
                                 Lng: item.Lng,
                                 Value: item.Value,
                                 PlaceID: item.PlaceID
                              },
                              Headquarter: {}
                           };
                        }) || [];

                     return _location;
                  });
            }, function () {
               return [
                  {
                     display: 'error',
                     lat: 0,
                     lng: 0,
                     value: ''
                  }
               ];
            });
         };

         var _getLocations = function (_searchText, _groupNumber) {
            var data = {
               searchText: _searchText,
               groupNumber: _groupNumber
            };

            var _getHeadquarterByLatLng = function (headquarter, lat, lng) {
               return (headquarter.filter(function (hqItem) {
                  var _apiLat = lat < 0 ? Math.ceil(lat * 10000) / 10000 : Math.floor(lat * 10000) / 10000;
                  var _apiLng = lng < 0 ? Math.ceil(lng * 10000) / 10000 : Math.floor(lng * 10000) / 10000;
                  var _hqLat = hqItem.Location.Lat < 0 ? Math.ceil(hqItem.Location.Lat * 10000) / 10000 : Math.floor(hqItem.Location.Lat * 10000) / 10000;
                  var _hqLng = hqItem.Location.Lng < 0 ? Math.ceil(hqItem.Location.Lng * 10000) / 10000 : Math.floor(hqItem.Location.Lng * 10000) / 10000;

                  return (_hqLat == _apiLat && _hqLng == _apiLng);
               })[0]);
            };

            return ws_Location.get(data.searchText).then(function (locationResponse) {
               return ws_Headquarter.get(data).then(function (headquarterResponse) {
                  var _headquarters = headquarterResponse.data;

                  var _locationGroup = locationResponse
                     .data
                     .results
                     .map(function (item) {
                        var _headquarter = _getHeadquarterByLatLng(_headquarters, item.geometry.location.lat, item.geometry.location.lng);

                        return {
                           Location: {
                              LocationID: _headquarter == null ? 0 : _headquarter.Location.LocationID,
                              Display: item.formatted_address,
                              Lat: item.geometry.location.lat,
                              Lng: item.geometry.location.lng,
                              Value: item.formatted_address,
                           },
                           Headquarter: _headquarter
                        };
                     }) || [];

                  var _headquartersProcessedAsLocationGroup = _headquarters.map(function (item) {
                     return {
                        Location: {
                           LocationID: item.LocationID,
                           Display: item.Location.Display,
                           Lat: item.Location.Lat,
                           Lng: item.Location.Lng,
                           Value: item.Location.Value
                        },
                        Headquarter: item
                     }
                  }) || [];

                  _locationGroup = _locationGroup.concat(_headquartersProcessedAsLocationGroup);

                  var _locationGroupDistinct = [];

                  var _headquarterIdInJsonArr = function (jsonArr, headquarterID) {
                     var found = false;
                     $.each(jsonArr, function (index, lgJsonArr) {
                        if (lgJsonArr.Headquarter != null && headquarterID == lgJsonArr.Headquarter.HeadquarterID)
                           found = true;
                     });
                     return found;
                  };

                  $.each(_locationGroup, function (index, locationGroupItem) {
                     if (locationGroupItem.Headquarter == null || !_headquarterIdInJsonArr(_locationGroupDistinct, locationGroupItem.Headquarter.HeadquarterID)) {
                        _locationGroupDistinct.push(locationGroupItem);
                     }
                  });

                  return _locationGroupDistinct;
               },
                  function () {
                     var _location = locationResponse
                        .data
                        .results
                        .map(function (item) {
                           console.log(item);
                           return {
                              Location: {
                                 LocationID: 0,
                                 Display: item.formatted_address,
                                 Lat: item.geometry.location.lat,
                                 Lng: item.geometry.location.lng,
                                 Value: item.formatted_address,
                              },
                              Headquarter: {}
                           };
                        }) || [];

                     return _location
                  });
            }, function () {
               return [
                  {
                     display: 'error',
                     lat: 0,
                     lng: 0,
                     value: ''
                  }
               ];
            });
         };

         var _getLocationGroupText = function (locationGroup) {
            var strLocation = '';
            if (locationGroup) {
               if (locationGroup.Headquarter != null
                  && typeof (locationGroup.Headquarter) != 'undefined'
                  && locationGroup.Headquarter.Name != null
                  && typeof (locationGroup.Headquarter.Name) != 'undefined') {
                  strLocation = locationGroup.Headquarter.Name + ' - ';
               }
               if (locationGroup.Location.Display != null
                  && typeof (locationGroup.Location.Display != 'undefined')) {
                  strLocation += locationGroup.Location.Display;
               }
            }
            return strLocation;
         };

         var _searchPlayers = function (playerSearchText) {
            return geolocationService.getCurrentLocation().then(function (response) {
               //var data = response;
               //TODO: Marian - porque no se envia response con el get?

               var data = {
                  SearchText: playerSearchText,
                  Lng: response.longitude,
                  Lat: response.latitude
               }

               return ws_PlayerSearch.get(data).then(function (resp) {
                  return resp.data;
               },
                  function (error) {
                     return [];
                  });
            });
         };

         var _suggestLocation = function (_searchText) {
            var data = {
               searchText: _searchText,
               groupNumber: 1
            };

            return geolocationService.suggestLocation(data.searchText).then(function (locationResponse) {
               var _auxLocationResponse = locationResponse;
               if (!Array.isArray(locationResponse)) {
                  _auxLocationResponse = [locationResponse];
               }

               return _auxLocationResponse;
            }, function () {
               return [
                  {
                     display: data.searchText,
                     lat: 0,
                     lng: 0,
                     value: data.searchText
                  }
               ];
            });
         };

         var _setLocation = function (item, model, callback) {
            if (item != null) {
               //TODO: pasar magickey/placeid como parameter
               geolocationService.getLocation(item.Value).then(function (resp) {
                  model = resp;
               });
            } 
            if (callback)
               callback();
         };

         return {
            getLocations: _getLocations,
            getLocationGroupText: _getLocationGroupText,
            searchPlayers: _searchPlayers,
            getLocationGroups: _getLocationGroups,
            suggestLocation: _suggestLocation,
            setLocation: _setLocation
         };
      }]);