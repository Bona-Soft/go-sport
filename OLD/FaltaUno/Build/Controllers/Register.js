angular.module('Authentication').controller('Register', ["$scope", "$controller", "authenticationService", "alertService", "errorService", 
   function ($scope, $controller, authenticationService, alertService, errorService) {
      $controller('BaseController', { $scope: $scope });

       var _init = function () {
           $scope.data = {
               name: null,
               lastName: null,
               mail: null,
               password: null,
               repeatPassword: null,
               sport: null
           };
           $scope.base.isLoading = 0;
       };

      $scope.success = false;

      $scope.register = function () {
         if ($scope.base.isProcessing == 1)
            return;
         $scope.base.isProcessing = 1;
         var oData = $scope.data;
         $scope.messages = [];
         authenticationService.register(oData).then(
            function () {
               //success
               alertService.clear();
               alertService.add('success', 'El e-mail se registró correctamente.\r\nEn unos instantes recibirá un correo electrónico para validar su usuario.');
               $scope.base.isProcessing = 0;
               $scope.success = true;
            }, function (error) {
               //errors
               if (!errorService.controlExc(error)) {
                  alertService.clear();
                  alertService.addHttpAlerts(error, $scope);
               }
               $scope.base.isProcessing = 0;
               $scope.success = false;
            }
         );
      }

      _init();
   }]);