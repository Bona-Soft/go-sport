app.controller('Match', ["$scope", '$rootScope', "$state", "$controller","$filter",
   "userService", "alertService", "errorService", "mybValidatorService", 'geolocationService',
   "ws_Match", 'MatchID',
   function ($scope, $rootScope, $state, $controller, $filter,
      userService, alertService, errorService, mybValidatorService, geolocationService,
      ws_Match,  MatchID) {
      $controller('BaseController', { $scope: $scope });

      //INTERNALS DEFINITIONS
      {
         var _loadMatch = function () {
            return ws_Match.get($scope.filters).then(function (response) {
               _fillDataMatch(response.data);

               $scope.base.isLoading = 0;
            }, function (error) {
               alertService.clear();
               if (!errorService.controlExc(error)) {
                  alertService.addHttpAlerts(error, $scope);
               }
               $scope.base.isLoading = 0;
            });
         };

         var _fillDataMatch = function (match) {
            if (match.LocationGroup == null) {
               match.LocationGroup = {
                  Headquarter: match.Headquarter,
                  Location: match.Location
               };
            }

            if (match.playersQty == null) {
               match.playersQty = {
                  MaxPlayers: match.MaxPlayers,
                  MinPlayers: match.MinPlayers
               };
            }

            delete match.Location;
            delete match.Headquarter;
            delete match.MaxPlayers;
            delete match.MinPlayers;

            //From UTC to Local
            if (match.MatchDateTime != null) {
               _date = moment(moment.utc(match.MatchDateTime).toDate()).local().format();

               if (!isNaN(Date.parse(_date))) {
                  $scope.auxMatchDate = $filter('date')(_date, $rootScope.$settings.dateFormat);
                  $scope.auxMatchTime = $filter('date')(_date, $rootScope.$settings.timeFormat);
               }

               var _hour = moment($scope.auxMatchTime, $rootScope.$settings.timeFormat).get('hour');
               var _minute = moment($scope.auxMatchTime, $rootScope.$settings.timeFormat).get('minute');
               match.MatchDateTime = moment($scope.auxMatchDate, $rootScope.$settings.dateFormat.toUpperCase() + ' ' + $rootScope.$settings.timeFormat).set({ 'hour': _hour, 'minute': _minute }).toDate();
               $scope.auxMatchDate = match.MatchDateTime;
            }

            match.Public = match.Public === 1;
            match.ChallengeType = $rootScope.getChallengeType(match.ChallengeType.ChallengeTypeID);
            match.Field = $rootScope.getField(match.Field.FieldID);
            match.MatchType = $rootScope.getMatchType(match.MatchType.MatchTypeID);

            $scope.locationSuggestSelected = match.LocationGroup;
            $scope.customMinPlayers = match.playersQty.MinPlayers;
            $scope.customMaxPlayers = match.playersQty.MaxPlayers;
            

            $scope.data.Match = match;

            $scope.changePlayerQty();
         };

      }

      //SCOPE DEFINITIONS
      {
         $scope.init = function () {
            $scope.base.isLoading = 1;

            $scope.mapLocation = {
               lat: 0,
               lng: 0,
               zoom: 15
            };

            $scope.mapMarker = {
               locationSearch: {
                  lat: 0,
                  lng: 0,
                  focus: true
               }
            };

            $scope.playersQty = 11;

            //Model
            $scope.data.Match = {
               Name: 'Partido de ' + userService.user().Name,
               Sport: $rootScope.settings.sportSelected
            };

            //TODO: Cargar desde la base ***********
            $scope.customMinPlayers = 10;
            $scope.customMaxPlayers = 10;
            $scope.customPlayersQty = {};

            $scope.playersQtyValues = [
               {
                  qty: {
                     MinPlayers: 10,
                     MaxPlayers: 10
                  },
                  Default: true,
                  Description: '10 jugadores (5 vs 5)',
               },
               {
                  qty: {
                     MinPlayers: 16,
                     MaxPlayers: 16
                  },
                  Description: '16 jugadores (8 vs 8)',
               },
               {
                  qty: {
                     MinPlayers: 22,
                     MaxPlayers: 22
                  },
                  Description: '22 jugadores (11 vs 11)',
               }
            ];

            $.each($scope.playersQtyValues, function (index, qtyItem) {
               if (qtyItem.Default) {
                  $scope.data.Match.playersQty = qtyItem.qty;
                  return;
               }
            });

            //END TODO: Cargar desde la base ***********

            $scope.setMatchDateTime = function () {
               var _date = null;

               try {
                  var _hour = moment($scope.auxMatchTime, $rootScope.$settings.timeFormat).get('hour');
                  var _minute = moment($scope.auxMatchTime, $rootScope.$settings.timeFormat).get('minute');

                  _date = moment($scope.auxMatchDate, $rootScope.$settings.dateFormat.toUpperCase() + ' ' + $rootScope.$settings.timeFormat).set({ 'hour': _hour, 'minute': _minute }).toDate();
               } catch
               {
               }

               $scope.data.Match.MatchDateTime = _date;
            };


            if (MatchID > 0) {
               $scope.filters = {
                  MatchID: MatchID,
                  ViewType: 2
               };

               _loadMatch();
            } else {
               $scope.base.isLoading = 0;
            }
         };

         $scope.changePlayerQty = function () {
            var _indexFound = -1;
            $.each($scope.playersQtyValues, function (index, qtyItem) {
               if (qtyItem.qty != null
                  && qtyItem.qty.MinPlayers == $scope.customMinPlayers
                  && qtyItem.qty.MaxPlayers == $scope.customMaxPlayers) {
                  _indexFound = index;
                  return;
               };
            });

            if (_indexFound >= 0) {
               $scope.data.Match.playersQty = $scope.playersQtyValues[_indexFound].qty;
            } else {
               $scope.data.Match.playersQty = { MinPlayers: $scope.customMinPlayers, MaxPlayers: $scope.customMaxPlayers };
            }
            $scope.playersQtyNoLimit = $scope.customMinPlayers == 0 && $scope.customMaxPlayers == 0;
         };

         $scope.changePlayersQtyNoLimit = function () {
            if ($scope.playersQtyNoLimit)
               $scope.data.Match.playersQty = { MinPlayers: 0, MaxPlayers: 0 };
         };

         $scope.geolocationService = geolocationService;

         $scope.suggestLocation = function (searchtext) {
            return geolocationService.suggestLocation(searchtext);
         };

         $scope.setLocationGroup = function (item, searchText) {
            if (item != null) {
               //TODO: pasar magickey/placeid como parameter
               geolocationService.getLocation(item.Location.Value).then(function (resp) {
                  $scope.data.Match.LocationGroup = item;
                  $scope.data.Match.LocationGroup.Location = resp;
                  $scope.setMapLocation(resp);
               });
            } else {
               $scope.data.Match.LocGroup = {
                  Location: {
                     LocationID: 0,
                     Display: searchText,
                     Lat: null,
                     Lng: null,
                     Value: searchText,
                     PlaceID: null
                  },
                  Headquarter: null
               };
            }
         };

         $scope.getLoc = function (text) {
            geolocationService.searchLocation(text);

            return geolocationService.suggestLocation(text);
         };

         $scope.validate = function (form) {
            mybValidatorService.validateForm(form);
         };

         $scope.save = function () {
            //TODO:Revisar directiva para evitar poner isprocessing > 0 en todos lados. Crear esa directiva para bloquear el boton tambien.
            if ($scope.base.isProcessing > 0) {
               return;
            }
            $scope.base.isProcessing = 1;
            $scope.data.Match.Sport = $rootScope.settings.sportSelected;

            if ($scope.data.Match.MatchID > 0) {
               ws_Match.put($scope.data.Match).then(
                  function (response) {
                     //TODO: Cambiar a Edit Players en el nombre del state
                     $state.go('Match/New/Players', { Match: $scope.data.Match });
                     $scope.base.isProcessing = 0;
                  },
                  function (error) {
                     alertService.clear();
                     if (!errorService.controlExc(error)) {
                        alertService.addHttpAlerts(error, $scope);
                     }
                     $scope.base.isProcessing = 0;
                  }
               );
            } else {
               ws_Match.post($scope.data.Match).then(
                  function (response) {
                     //TODO: Cambiar a Edit Players en el nombre del state
                     $scope.data.Match.MatchID = response.data.MatchID;
                     $state.go('Match/New/Players', { Match: $scope.data.Match });
                     $scope.base.isProcessing = 0;
                  },
                  function (error) {
                     alertService.clear();
                     if (!errorService.controlExc(error)) {
                        alertService.addHttpAlerts(error, $scope);
                     }
                     $scope.base.isProcessing = 0;
                  }
               );
            }
           
         };

         $scope.matchTypeChange = function () {
         };
      }

      $scope.init();

      //WATCHES
      {
         $scope.$watch(function (scope) { return scope.data.Match.playersQty; }, function (newVal, oldVal) {
            $scope.customMinPlayers = newVal.MinPlayers;
            $scope.customMaxPlayers = newVal.MaxPlayers;
            if (newVal.MinPlayers != 0 || newVal.MaxPlayers != 0)
               $scope.playersQtyNoLimit = false;
         });

         $scope.funcTest = function (val) {
            return val;
         };
      }
   }]);