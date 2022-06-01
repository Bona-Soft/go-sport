app.controller('SportSelect', ['$scope', '$rootScope', '$state', '$stateParams', '$controller', 'alertService',
   function ($scope, $rootScope, $state, $stateParams, $controller, alertService) {
      //$controller('BaseController', { $scope: $scope });

      $scope.init = function () {
      }

      $scope.changeSport = function (sport) {
         $rootScope.settings.sportSelected = sport;
         if ($stateParams.previousStateName == 'undefined' || $stateParams.previousStateName == null)
            $stateParams.previousStateName = 'Home';
         $state.go($stateParams.previousStateName);
      }
      $scope.init();
   }]);