app.controller('PlayerSearch',
      ["$scope", "$controller",
         "alertService", "errorService", "geolocationService",
         "ws_PlayerSearch", "ws_UserOwnerMatches",
   function ($scope, $controller,
         alertService, errorService, geolocationService,
      ws_PlayerSearch, ws_UserOwnerMatches) {

      $controller('BaseController', { $scope: $scope });

      //PIRVATE DEFINITIONS
      {
         
         var _init = function () {
            $scope.showFilters = true;
            $scope.searchResults = null;
            $scope.base.isLoading = 0;
            $scope.data.PlayerSearch = {};

            ws_UserOwnerMatches.get().then(function (response) {
               $scope.currentUserMatches = response.data;
            });
         };
      }
     
      //SCOPE DEFINITIONS
      {

         $scope.currentUserMatches = [];


         $scope.toggleFilters = function () {
            $scope.showFilters = !$scope.showFilters;
         };

         $scope.showNoPlayers = function () {
            return $scope.base.isLoading === 0 && ($scope.searchResults == null || !($scope.searchResults.length > 0));
         };


         $scope.setLocationGroup = function (item, searchText) {
            if (!$scope.data.PlayerSearch) $scope.data.PlayerSearch = {};
            $scope.data.PlayerSearch.HeadquarterID = null;
            $scope.data.PlayerSearch.LocationID = null;
            $scope.data.PlayerSearch.Lat = null;
            $scope.data.PlayerSearch.Lng = null;

            if (item != null) {
               //TODO: pasar magickey/placeid como parameter
               geolocationService.getLocation(item.Location.Value).then(function (resp) {
                  if (item.Headquarter) {
                     $scope.data.PlayerSearch.HeadquarterID = item.Headquarter.HeadquarterID;
                  }
                  $scope.data.PlayerSearch.LocationID = item.LocationID;
                  $scope.data.PlayerSearch.Lat = resp.Lat;
                  $scope.data.PlayerSearch.Lng = resp.Lng;
                  //$scope.setMapLocation(resp);
               });
            };
         };

         $scope.research = function () {

            if ($scope.data.PlayerSearch.Name == null
               && $scope.data.PlayerSearch.LastName == null
               && $scope.data.PlayerSearch.Alias == null
               && $scope.data.PlayerSearch.MinAge == null
               && $scope.data.PlayerSearch.Lat == null) {
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

            ws_PlayerSearch.get($scope.data.PlayerSearch).then(function (response) {
               $scope.searchResults = response.data;
               $scope.base.isProcessing = 0;
            }, function (error) {
               $scope.tagView = false;
               alertService.clear();
               errorService.controlExc(error);
               $scope.base.isProcessing = 0;
            });
         };
      }


      _init();
   }]);