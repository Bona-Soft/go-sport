app.controller('MatchList', ["$scope", "$controller", "$state", "alertService", "errorService", "userService", "ws_MatchCurrentUser",
   function ($scope, $controller, $state, alertService, errorService, userService, ws_MatchCurrentUser) {
      $controller('BaseController', { $scope: $scope });

      var _init = function () {
         $scope.base.isLoading = 1;
         _loadMatches();
      };

      var _loadMatches = function () {
         $scope.matchList = null;
         var data = {
            MatchRequestType: 'P',
	         Closed: false,
	         PageNumber: 1,
            PageSize: 10, 
	         AscOrder: false
         };

         ws_MatchCurrentUser.get(data).then(
            function (response) {
               $scope.matchList = response.data;
               $scope.base.isLoading = 0;
            }, function (error) {
               alertService.clear();
               errorService.controlExc(error);
               $scope.base.isLoading = 0;
            });
      };

      $scope.viewMatch = function (match) {
         $state.go('Match/View', { MatchID: match.MatchID });
      };

      $scope.showNoMatch = function () {
         return $scope.base.isLoading === 0 && ($scope.matchList == null || !($scope.matchList.length > 0));
      };

      _init();
   }]);