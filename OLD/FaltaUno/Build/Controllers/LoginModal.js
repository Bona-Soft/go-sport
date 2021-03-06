/******************************************************
                     MODAL LOGIN
******************************************************/
/*----- Modal Controller -----*/
// Please note that $uibModalInstance represents a modal window (instance) dependency.
// It is not the same as the $uibModal service used above.

angular.module('Authentication').controller('loginCtrl', ["$scope", "$uibModalInstance", "ws_Login", "alertService", "authenticationService", "errorService", "SSEService",
   function ($scope, $uibModalInstance, ws_Login, alertService, authenticationService, errorService, SSEService) {
      alertService.clear();

      $scope.init = function () {
         $scope.user = {
            username: '',
            password: '',
            isLogged: false,
            user: {},
         }
      }

      $scope.ok = function () {
         alertService.clear();
         authenticationService.login($scope.user).then(
            function (user) {
               SSEService.start();
               $uibModalInstance.close(user);

            },
            function (error) {
               if (!errorService.controlExc(error)) {
                  alertService.addHttpAlerts(error, $scope);
               }
            }
         );
      };

      $scope.cancel = function () {
         $uibModalInstance.dismiss('cancel');
      };

      $scope.FBLogin = function () {
         authenticationService.fbLogin();
      };

      $scope.gmail = {
         username: "",
         email: ""
      };

      $scope.ongoogleLogin = function () {
         var params = {
            clientid: '480438999449-ghc90fi67v2ia2oaupkst3e3k1h0h0sg.apps.googleusercontent.com',
            cookiepolicy: 'single_host_origin',
            callback: function (result) {
               console.log(result);
               if (result['status']['signed_in']) {
                  //comment
                  console.log('LOGUEADO');
               }
            },
            approvalprompt: 'force',
            scope: 
               'https://www.googleapis.com/auth/plus.login ' +
               'https://www.googleapis.com/auth/calendar ' +
               'https://www.googleapis.com/auth/calendar.readonly ' +
               'https://www.googleapis.com/auth/contacts ' +
               'https://www.googleapis.com/auth/contacts.readonly ' +
               'https://www.googleapis.com/auth/userinfo.profile ' +
               'https://www.googleapis.com/auth/userinfo.email ' +
               'https://www.googleapis.com/auth/user.addresses.read ' +
               'https://www.googleapis.com/auth/user.birthday.read ' +
               'https://www.googleapis.com/auth/user.emails.read ' +
               'https://www.googleapis.com/auth/user.phonenumbers.read'
         };

         gapi.auth.signIn(params);
      };

      $scope.init();
   }]);