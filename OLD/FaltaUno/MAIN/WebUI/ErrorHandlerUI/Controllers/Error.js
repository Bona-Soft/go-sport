angular.module('HayEquipoApp').controller('Error', ['$scope', '$http', '$controller', '$uibModal', '$state', 'errorService',
   function ($scope, $http, $controller, $uibModal, $state, errorService) {
      $controller('BaseController', { $scope: $scope });

      $scope.init = function () {
         if (!errorService.hasError()) {
            $state.go("Home");
         }
      }

      $scope.init();
   }]);

angular.module('HayEquipoApp').controller('confirmModalInstance', function ($scope, $uibModalInstance, data) {
   $scope.init = function () {
      $scope.titulo = data.titulo;
      $scope.texto = data.texto;
   }

   $scope.ok = function () {
      $uibModalInstance.close();
   }

   $scope.cancel = function () {
      $uibModalInstance.dismiss('cancel');
   };

   $scope.init();
});