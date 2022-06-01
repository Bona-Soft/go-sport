app.controller('SelectSport', ['$scope', '$rootScope', '$state', '$stateParams', '$controller', 'alertService','ws_UserActiveSport','cookieService',
   function ($scope, $rootScope, $state, $stateParams, $controller, alertService, ws_UserActiveSport, cookieService) {
      //$controller('BaseController', { $scope: $scope });

      var _init = function () {
      };

      //TODO: hay 2 funciones changeSport, la de este controller y la del header deberian llamar al mismo.
      $scope.changeSport = function (sport) {
         $rootScope.settings.sportSelected = sport;
         if ($stateParams.previousStateName == 'undefined' || $stateParams.previousStateName == null)
            $stateParams.previousStateName = 'Home';
         $state.go($stateParams.previousStateName);

         ws_UserActiveSport.post(sport);
         cookieService.setJsonCookie("SportSelected", sport);
      };

      _init();
   }]);

   