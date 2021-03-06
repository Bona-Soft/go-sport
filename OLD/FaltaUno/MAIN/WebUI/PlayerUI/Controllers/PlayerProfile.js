app.controller('PlayerProfile', ["$rootScope", "$scope", "$controller", "$filter", "alertService", "errorService", "userService", "ws_Player", "SportID",
   function ($rootScope, $scope, $controller, $filter, alertService, errorService, userService, ws_Player, SportID) {
      $controller('BaseController', { $scope: $scope });

      $scope.SportID = SportID;

      var _init = function () {
         $scope.base.isLoading = 1;
         $scope.tabPosition = 1;
         $scope.playerExists = false;
         if ($scope.SportID != null && !isNaN($scope.SportID)) {
            $rootScope.changeSport($scope.SportID);
         }
         _getPlayer();

      };

      var _getPlayer = function () {
         if (userService.user()) {
            var sport = { Sport: { SportID: $rootScope.getSportSelected().SportID } };
            var players = $filter('filter')(userService.user().Players, sport);

            if (players.length > 0) {
               var data = {
                  PlayerID: players[0].PlayerID
               };

               ws_Player.get(data).then(
                  function (response) {
                     $scope.playerExists = response.data.PlayerID && response.data.PlayerID > 0;

                     if ($scope.playerExists) {
                        $scope.data.Player = response.data;
                        $scope.data.account = {
                           personalInfo: {
                              User: response.data.User,
                              auxBirthDate: $rootScope.getFrontDateTime(response.data.User.BirthDate)
                           }

                        };

                     }

                     $scope.base.isLoading = 0;
                  },
                  function (error) {
                     $scope.playerExists = false;
                     alertService.clear();
                     if (!errorService.controlExc(error)) {
                        alertService.addHttpAlerts(error, $scope);
                     }
                     $scope.base.isLoading = 0;
                  });
            } else {
               $scope.playerExists = false;
               $scope.base.isLoading = 0;
            }
         }
      };

      _init();
   }
]);

app.controller('PlayerGeneralProfile', ["$scope", "$controller", "alertService", "errorService", "userService",
   function ($scope, $controller, alertService, errorService, userService) {
      $controller('BaseController', { $scope: $scope });

   }
]);

app.controller('PlayerAccountProfile', ["$scope", "$controller",
   "uiUploader", "alertService", "errorService", "userService","authenticationService",
   "ws_UserPersonalInfo", "ws_UserPrivacy", "ws_ChangePassword",
   function ($scope, $controller,
      uiUploader, alertService, errorService, userService, authenticationService,
      ws_UserPersonalInfo, ws_UserPrivacy, ws_ChangePassword)
   {
      $controller('BaseController', { $scope: $scope });
      $scope.base.isLoading = 1;

      //Private Methods
      {
         var _loadPrivacy = function () {
            ws_UserPrivacy.get().then(
               function (response) {
                  $scope.data.account.privacy = response.data;
                  $scope.base.isLoading = 0;
               },
               function (error) {
                  $scope.base.isLoading = 0;
               });

         };

         //TODO: Pasar a directiva.. revisar si hace falta
         var _initUploaderEvent = function () {
            var element = document.getElementById('fileAvatar');
            element.addEventListener('change', function (e) {
               var files = e.target.files;
               $scope.cleanFiles();
               uiUploader.addFiles(files);
               $scope.files = uiUploader.getFiles();
               $scope.$apply();
            });
         };

         var _initPersonalInformation = function () {

         };

         var _init = function () {

            $scope.data.account = {
               privacy: {
                  MobileNumber: 'N',
                  BirthDate: 'N'
               }
            };
            $scope.tabHistoryPosition = 1;
            $scope.tabAccountSettings = 1;
            $scope.files = [];

            _initUploaderEvent();
            _initPersonalInformation();
            _loadPrivacy();

         };
      }


      //Scope Methods
      {
         $scope.changeAccountPassword = function () {
            if ($scope.base.isProcessing == 1) return;
            $scope.base.isProcessing = 1;

            ws_ChangePassword.post($scope.data.account.changePassword).then(
               function (response) {
                  alertService.clear();
                  alertService.addHttpAlerts(response, $scope);
                  alertService.add('success', 'La informacion se guardo correctamente'); //TODO: cambiar todos los hardcode de respuestas correctas x alguna forma de la base x traducciones o resouce strings
                  $scope.base.isProcessing = 0;
               },
               function (error) {
                  alertService.clear();
                  if (!errorService.controlExc(error)) {
                     alertService.addHttpAlerts(error, $scope);
                  }
                  $scope.base.isProcessing = 0;
               });
         };

         $scope.saveAccountPersonalInfo = function () {
            alertService.clear();
            if ($scope.base.isProcessing == 1)
               return;
            $scope.base.isProcessing = 1;

            ws_UserPersonalInfo.post($scope.$parent.data.account.personalInfo.User).then(function (response) {
               alertService.clear();
               var userAux = response.data;
               $scope.$parent.data.account.personalInfo.User = userAux;
               alertService.addHttpAlerts(response, $scope);
               userService.authenticate(userAux);
               alertService.add('success', 'La informacion se guardo correctamente');
               $scope.base.isProcessing = 0;
            },
               function (error) {
                  alertService.clear();
                  if (!errorService.controlExc(error)) {
                     alertService.addHttpAlerts(error, $scope);
                  }
                  $scope.base.isProcessing = 0;
               });
         };

         $scope.saveAccountPrivacy = function () {
            alertService.clear();
            if ($scope.base.isProcessing == 1)
               return;
            $scope.base.isProcessing = 1;

            ws_UserPrivacy.put($scope.data.account.privacy).then(function (response) {
               alertService.clear();
               alertService.addHttpAlerts(response, $scope);
               alertService.add('success', 'La informacion se guardo correctamente');
               $scope.data.account.privacy = response.data;
               $scope.base.isProcessing = 0;
            },
               function (error) {
                  alertService.clear();
                  if (!errorService.controlExc(error)) {
                     alertService.addHttpAlerts(error, $scope);
                  }
                  $scope.base.isProcessing = 0;
               });
         };


         $scope.removeFile = function (file) {
            uiUploader.removeFile(file);
         };
         $scope.cleanFiles = function () {
            uiUploader.removeAll();
         };
         $scope.uploadFile = function () {
            alertService.clear();

            if ($scope.files.length > 0) {
               $scope.base.isProcessing = 1;

               uiUploader.startUpload({
                  url: 'UserAvatarUpload.aspx',
                  concurrency: 1,
                  onProgress: function (file) {
                     $scope.$apply();
                  },
                  onCompleted: function (file, response, status) {
                     if (response) {
                        userService.user().Avatar = JSON.parse(response).Avatar;
                     }
                     $scope.cleanFiles();
                     alertService.clear();
                     alertService.add('success', 'El imagen se cargó correctamente.');
                     $scope.base.isProcessing = 0;
                  },
                  onError: function (error) {
                     alertService.clear();
                     if (!errorService.controlExc(error)) {
                        alertService.addHttpAlerts(error, $scope);
                     }
                     $scope.cleanFiles();
                     $scope.base.isProcessing = 0;
                  }
               });
            } else {
               //console.log("no hay file cargado");
            }
         };

         $scope.noFileLoaded = function () {
            return $scope.files.length == 0;
         };

         $scope.cancel = function () {
            $scope.cleanFiles();
            $scope.tabPosition = 1;
         };

      }

      _init();


   }]);



app.controller('PlayerSportProfile', ["$scope", "$controller", "alertService", "errorService", "userService", "ws_Player", "ws_PlayerEnable", 
   function ($scope, $controller, alertService, errorService, userService, ws_Player, ws_PlayerEnable) {
      $controller('BaseController', { $scope: $scope });     
 
      $scope.enablePlayer = function () {
         if ($scope.base.isProcessing == 1) {
            return;
         }
         $scope.base.isProcessing = 1;

         if ($scope.$parent.data.Player.PlayerID > 0) {
            var data = {
               PlayerID: $scope.$parent.data.Player.PlayerID,
               Active: $scope.$parent.data.Player.Active
            };

            ws_PlayerEnable.put(data).then(
               function (response) {
                  alertService.clear();
                  
                  $scope.base.isProcessing = 0;
               },
               function (error) {
                  alertService.clear();
                  if (!errorService.controlExc(error)) {
                     alertService.addHttpAlerts(error, $scope.$parent);
                  }
                  $scope.$parent.data.Player.Active = !$scope.$parent.data.Player.Active;
                  $scope.base.isProcessing = 0;
               });
         } else {
            ws_Player.post().then(
               function (response) {
                  alertService.clear();
                  $scope.$parent.data.Player = response.data;
                  userService.user().Players.push($scope.$parent.data.Player);
                  $scope.base.isProcessing = 0;
               },
               function (error) {
                  alertService.clear();
                  if (!errorService.controlExc(error)) {
                     alertService.addHttpAlerts(error, $scope.$parent);
                  }
                  $scope.base.isProcessing = 0;
               });
         }
        
      };

      
   }]);