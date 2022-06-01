app.controller('PlayerView', ["$rootScope", "$scope", "$controller", "$filter",
   "alertService", "errorService", "userService",
   "ws_Player",
   "PlayerID",
   function ($rootScope, $scope, $controller, $filter,
      alertService, errorService, userService,
      ws_Player,
      PlayerID) {
      $controller('BaseController', { $scope: $scope });

      $scope.PlayerID = PlayerID;

      var _init = function () {
         $scope.base.isLoading = 1;

         _getPlayer();

      };

      var _getPlayer = function () {
         if (userService.user()) {
            var data = {
               PlayerID: PlayerID
            };

            ws_Player.get(data).then(
               function (response) {
                  $scope.player = response.data;

                  $scope.base.isLoading = 0;
               },
               function (error) {
                  alertService.clear();
                  if (!errorService.controlExc(error)) {
                     alertService.addHttpAlerts(error, $scope);
                  }
                  $scope.base.isLoading = 0;
               });
         }
      };

      _init();
   }
]);
