app.controller('Debug', ["$scope", '$rootScope', "$state", "$controller","$filter",
   "userService", "alertService", "errorService", "mybValidatorService", 'geolocationService',
   'ws_Log', 'ws_Jobs',
   function ($scope, $rootScope, $state, $controller, $filter,
      userService, alertService, errorService, mybValidatorService, geolocationService,
      ws_Log, ws_Jobs) {
      $controller('BaseController', { $scope: $scope });
      
      //SCOPE DEFINITIONS
      {
         $scope.loadLog = function () {
            $scope.base.isProcessing = 1;
            var data = {
               capacity: $scope.data.DebugOptions.capacity,
               level: $scope.data.DebugOptions.level
            };
            return ws_Log.get(data).then(function (response) {
               $scope.log = response.data;
               $scope.base.isProcessing = 0;
               $scope.base.isLoading = 0;
            }, function (error) {
               alertService.clear();
               if (!errorService.controlExc(error)) {
                  alertService.addHttpAlerts(error, $scope);
               }
               $scope.base.isProcessing = 0;
               $scope.base.isLoading = 0;
            });
         };

         $scope.singleRunAll = function () {
            $scope.base.isProcessing = 1;
            return ws_Jobs.post().then(function (response) {
               $scope.base.isProcessing = 0;
            }, function (error) {
               alertService.clear();
               if (!errorService.controlExc(error)) {
                  alertService.addHttpAlerts(error, $scope);
               }
               $scope.base.isProcessing = 0;
            });
         };

         $scope.loadJobs = function () {
            $scope.base.isProcessing = 1;
            return ws_Jobs.get().then(function (response) {
               $scope.jobs = response.data;
               $scope.base.isProcessing = 0;
               $scope.base.isLoading = 0;
            }, function (error) {
               alertService.clear();
               if (!errorService.controlExc(error)) {
                  alertService.addHttpAlerts(error, $scope);
               }
               $scope.base.isProcessing = 0;
               $scope.base.isLoading = 0;
            });
         };
         $scope.init = function () {
            $scope.base.isLoading = 1;

            $scope.log = [];

            $scope.logLevels =
               [{ value: "Unique", default: 0, number: 0 },
               { value: "None", default: 0, number: 1 },
               { value: "Core", default: 0, number: 2},
               { value: "Debug", default: 1, number: 3 },
               { value: "Info", default: 0, number: 4},
               { value: "Warn", default: 0, number: 5},
               { value: "Error", default: 0, number: 6},
               { value: "Fatal", default: 0, number: 7},
               { value: "All", default: 0, number: 8}];

            $scope.data.DebugOptions = {
               capacity: 1000,
               level: "Debug"
            };

            $scope.loadLog();
            $scope.loadJobs();
         };
      }

      $scope.init();


   }]);