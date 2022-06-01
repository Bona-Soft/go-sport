angular.module('Authentication').controller('VerifyUser', ['$scope', '$controller', '$stateParams', 'authenticationService', 'errorService', 'SweetAlert',
    function ($scope, $controller, $stateParams, authenticationService, errorService, SweetAlert) {
        //$controller('BaseController', { $scope: $scope });
        $scope.isLoading = 1;

        $scope.init = function () {
            $scope.data = {
                email: $stateParams.email,
                code: $stateParams.code
            };
            $scope.verifyUser();
        }

        $scope.verifyUser = function () {
            var oData = $scope.data;

            authenticationService.verifyUser(oData).then(
                //success
                function (response) {
                    //TODOFE: mostrar informe datos validados.
                    $scope.data.result = console.log(response.data);
                    $scope.isLoading = 0;
                },
                //errors
                function (error) {
                    errorService.controlExc(error);
                    if (error.data[0].ErrorCode === 1) {
                        errorService.sendError('No existe un usuario con este codigo.');
                    }
                    else if (error.data[0].ErrorCode === 2) {
                        errorService.sendError("El usuario ya fue verificado anteriormente.");
                    }
                    else if (error.data[0].ErrorCode === 3) {
                        errorService.sendError("Codigo de verificacion inválido.");
                    }
                    $scope.isLoading = 0;
                }
            );
        }

        $scope.init();
    }]);