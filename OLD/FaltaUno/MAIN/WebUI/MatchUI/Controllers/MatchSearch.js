app.controller('MatchSearch', ["$scope", "$rootScope", "$controller", "$filter", '$state',
   "alertService", "errorService", "geolocationService", "ws_MatchSearch",
   function ($scope, $rootScope, $controller, $filter, $state,
      alertService, errorService, geolocationService, ws_MatchSearch) {
      $controller('BaseController', { $scope: $scope });

      $scope.init = function () {
         $scope.labels = ["Ranking", "Reputacion", "Fair Play", "Distancia", "Nivel", "Edad"];

         $scope.showFilters = true;

         $scope.dataGrafo = [
            [65, 59, 90, 81, 56, 55],
            [28, 48, 40, 19, 96, 27]
         ];

         $scope.options = { pointStyle: 'line' };

         //$scope.data.MatchSearch.EndDate = moment($scope.data.MatchSearch.EndDate, $rootScope.$settings.dateFormat.toUpperCase() + ' ' + $rootScope.$settings.timeFormat).add(7,'day').toDate();
         $scope.base.isLoading = 0;
      };

      /*
      chart - data: series data
      chart - labels: series labels
      chart - options(default: {}): Chart.js options
         http://www.chartjs.org/docs/latest/charts/radar.html
      chart - series(default: []): series labels
      chart - click(optional): onclick event handler
      chart - hover(optional): onmousemove event handler
      chart - colors(default to global colors): colors for the chart
      chart- dataset - override(optional): override datasets individually
      */

      $scope.toggleFilters = function () {
         $scope.showFilters = !$scope.showFilters;
      };

      $scope.selectPlayer = function (player) {
         $scope.data.MatchSearch.PlayerID = null;
         if (player != null) {
            $scope.data.MatchSearch.PlayerID = player.PlayerID;
         }
      };

      $scope.searchPlayers = function (playerSearchText) {
         return $rootScope.inputMethods.searchPlayers(playerSearchText).then(function (playerList) {
            return $filter('filter')(playerList, $scope.notExistsPlayerRequest);
         },
            function (error) {
               return [];
            });
      };

      $scope.viewMatch = function (match) {
         $scope.selectedMatch = match;
      };

      $scope.goMatchView = function (match) {
         $state.go("Match/View", { MatchID: match.MatchID });
      };

      $scope.setLocationGroup = function (item, searchText) {
         $scope.data.MatchSearch.HeadquarterID = null;
         $scope.data.MatchSearch.LocationID = null;
         $scope.data.MatchSearch.Lat = null;
         $scope.data.MatchSearch.Lng = null;

         if (item != null) {
            //TODO: pasar magickey/placeid como parameter
            geolocationService.getLocation(item.Location.Value).then(function (resp) {
               if (item.Headquarter) {
                  $scope.data.MatchSearch.HeadquarterID = item.Headquarter.HeadquarterID;
               }
               $scope.data.MatchSearch.LocationID = item.LocationID;
               $scope.data.MatchSearch.Lat = resp.Lat;
               $scope.data.MatchSearch.Lng = resp.Lng;
               //$scope.setMapLocation(resp);
            });
         };
      };

      $scope.research = function () {

         if ($scope.data.MatchSearch.Name == null
            && $scope.data.MatchSearch.PlayerID == null
            && $scope.data.MatchSearch.MinAge == null
            && $scope.data.MatchSearch.Lat == null
            && $scope.data.MatchSearch.MatchTypeID == null
            && $scope.data.MatchSearch.FieldID == null
            && $scope.data.MatchSearch.ChallengeTypeID == null
            && $scope.data.MatchSearch.Public == null
            && $scope.data.MatchSearch.IsHotMatch == null) {
            $scope.toggleFilters();
         }
         else {
            $scope.search();
         }
      };

      $scope.search = function () {
         $scope.tagView = true;
         $scope.base.isProcessing = 1;
         $scope.searchResults = null;

         var _hour = moment($scope.auxStartTime , $rootScope.$settings.timeFormat).get('hour');
         var _minute = moment($scope.auxStartTime , $rootScope.$settings.timeFormat).get('minute');

         if (!$scope.data.MatchSearch) $scope.data.MatchSearch = {};

         $scope.data.MatchSearch.StartDateTime = moment($scope.auxStartDate, $rootScope.$settings.dateFormat.toUpperCase() + ' ' + $rootScope.$settings.timeFormat).set({ 'hour': _hour, 'minute': _minute }).toDate();

         _hour = moment($scope.auxEndTime, $rootScope.$settings.timeFormat).get('hour');
         _minute = moment($scope.auxEndTime, $rootScope.$settings.timeFormat).get('minute');

         $scope.data.MatchSearch.EndDateTime= moment($scope.auxEndDate, $rootScope.$settings.dateFormat.toUpperCase() + ' ' + $rootScope.$settings.timeFormat).set({ 'hour': _hour, 'minute': _minute }).toDate();


         ws_MatchSearch.get($scope.data.MatchSearch).then(function (response) {
            $scope.searchResults = response.data;
            $scope.base.isProcessing = 0;
         }, function (error) {
            $scope.tagView = false;
            alertService.clear();
            errorService.controlExc(error);
            $scope.base.isProcessing = 0;
         });
      };

      $scope.init();
   }]);