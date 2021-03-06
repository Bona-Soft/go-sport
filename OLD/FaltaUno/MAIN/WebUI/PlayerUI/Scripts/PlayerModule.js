app.module('Player', [
   "ui.router",
   "ui.bootstrap",
   "oc.lazyLoad",
   "ngSanitize",
   "ngResource"
])

   .config(['$stateProvider', '$urlRouterProvider', function ($stateProvider, $urlRouterProvider) {
      $stateProvider

         .state('Player/Profile', {
            url: "/PlayerProfile/:SID",
            templateUrl: "Templates/PlayerProfile.html",
            data: {
               pageTitle: 'Player Profile',
               access: {
                  authenticationRequired: true,
                  authenticationFailState: 'Inicio',
                  sportSelectedRequired: true,
                  parametersRequired: []
               }
            },
            params: { 'SportID': null },
            controller: "PlayerProfile",
            resolve: {
               SportID: ['$stateParams', function ($stateParams) {
                  if ($stateParams.SportID == null) {
                     if ($stateParams.SID == null) {
                        return 0;
                     }
                     else {
                        return $stateParams.SID;
                     }
                  }
                  return $stateParams.SportID;
               }],
               deps: ['$ocLazyLoad', '$injector', function ($ocLazyLoad, $injector) {
                  return $ocLazyLoad.load({
                     name: appName,
                     insertBefore: '#ng_load_plugins_before', // load the above css files before a LINK element with this ID. Dynamic CSS files must be loaded between core and theme css files
                     files: [
                        'Assets/global/plugins/bootstrap-fileinput/bootstrap-fileinput.css',
                        'Assets/global/plugins/bootstrap-fileinput/bootstrap-fileinput.js',
                        'Assets/global/plugins/angularjs/plugins/ui-uploader/uploader.js'
                     ]
                  }).then(function () {
                     app.module('Player', ["ui.uploader"]);
                     return $ocLazyLoad.load('Controllers/PlayerProfile.js');
                  });
               }]
            }
         })
         .state('Player/Ranking', {
            url: "/PlayerRanking",
            templateUrl: "Templates/PlayerRanking.html",
            data: { pageTitle: 'Player Ranking' },
            controller: "PlayerRanking",
            resolve: {
               deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                  return $ocLazyLoad.load({
                     name: appName,
                     insertBefore: '#ng_load_plugins_before', // load the above css files before a LINK element with this ID. Dynamic CSS files must be loaded between core and theme css files
                     files: [
                        'Controllers/PlayerRanking.js'
                     ]
                  });
               }]
            }
         })
         .state('Player/Request', {
            url: "/PlayerRequest",
            templateUrl: "Templates/PlayerRequest.html",
            data: { pageTitle: 'Player Request' },
            controller: "PlayerRequest",
            resolve: {
               deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                  return $ocLazyLoad.load({
                     name: appName,
                     insertBefore: '#ng_load_plugins_before', // load the above css files before a LINK element with this ID. Dynamic CSS files must be loaded between core and theme css files
                     files: [
                        'Scripts/RequestStateService.js',
                        'Controllers/PlayerRequest.js'
                     ]
                  });
               }]
            }
         })
         .state('Player/Search', {
            url: "/SearchPlayer",
            templateUrl: "Templates/PlayerSearch.html",
            data: { pageTitle: 'Search Player' },
            controller: "PlayerSearch",
            resolve: {
               deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                  return $ocLazyLoad.load({
                     name: appName,
                     insertBefore: '#ng_load_plugins_before', // load the above css files before a LINK element with this ID. Dynamic CSS files must be loaded between core and theme css files
                     files: [
                        'Controllers/PlayerSearch.js'
                     ]
                  });
               }]
            }
         })
         .state('Player/View', {
            url: "/PlayerView/:PID",
            templateUrl: "Templates/PlayerView.html",
            data: {
               pageTitle: 'Player View',
               access: {
                  authenticationFailState: 'Error',
                  parametersRequired: ['PlayerID']
               }
            },
            params: {
               'PlayerID': 0
            },
            controller: "PlayerView",
            resolve: {
               PlayerID: ['$stateParams', function ($stateParams) {
                  if ($stateParams.PlayerID == null) {
                     if ($stateParams.PID == null) {
                        return 0;
                     }
                     else {
                        return $stateParams.PID;
                     }
                  }
                  return $stateParams.PlayerID;
               }],
               deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                  return $ocLazyLoad.load({
                     name: appName,
                     insertBefore: '#ng_load_plugins_before', // load the above css files before a LINK element with this ID. Dynamic CSS files must be loaded between core and theme css files
                     files: [
                        'Controllers/PlayerView.js'
                     ]
                  });
               }]
            }
         });
   }])

   /*-----------------------------------   PLAYERS WS   ---------------------------------*/

   .factory('ws_Player', ['$rootScope', '$http', 'settings', function ($rootScope, $http, settings) {
      return {
         get: function (oData) {
            if (!oData) oData = {}; oData.SportID = $rootScope.getSportSelected().SportID;
            return $http.get(settings.wsVirtualPath + '/Player.aspx?', { params: oData });
         },
         post: function (oData) {
            if (!oData) oData = {}; oData.SportID = $rootScope.getSportSelected().SportID;
            return $http.post(settings.wsVirtualPath + '/Player.aspx', oData);
         }
      };
   }])

   .factory('ws_PlayerSearch', ['$rootScope', '$http', 'settings', function ($rootScope, $http, settings) {
      return {
         get: function (oData) {
            if (!oData) oData = {}; oData.SportID = $rootScope.getSportSelected().SportID;
            return $http.get(settings.wsVirtualPath + '/PlayerSearch.aspx?', { params: oData });
         }
      };
   }])

   .factory('ws_PlayerEnable', ['$rootScope', '$http', 'settings', function ($rootScope, $http, settings) {
      return {
         put: function (oData) {
            if (!oData) oData = {}; oData.SportID = $rootScope.getSportSelected().SportID;
            return $http.put(settings.wsVirtualPath + '/PlayerEnable.aspx', oData);
         }
      };
   }])

   .factory('ws_PlayerFrecuently', ['$rootScope', '$http', 'settings', function ($rootScope, $http, settings) {
      return {
         get: function (oData) {
            if (!oData) oData = {}; oData.SportID = $rootScope.getSportSelected().SportID;
            return $http.get(settings.wsVirtualPath + '/PlayerFrecuently.aspx?', { params: oData });
         }
      };
   }])

   .factory('ws_UserOwnerMatches', ['$rootScope', '$http', 'settings', function ($rootScope, $http, settings) {
      return {
         get: function (oData) {
            if (!oData) oData = {}; oData.SportID = $rootScope.getSportSelected().SportID;
            return $http.get(settings.wsVirtualPath + '/UserOwnerMatches.aspx', { params: oData });
         }
      };
   }])

   .factory('ws_PlayerRecommended', ['$rootScope', '$http', 'settings', function ($rootScope, $http, settings) {
      return {
         get: function (oData) {
            if (!oData) oData = {}; oData.SportID = $rootScope.getSportSelected().SportID;
            return $http.get(settings.wsVirtualPath + '/PlayerRecommended.aspx?', { params: oData });
         }
      };
   }])
   .factory('ws_UserMatchPlayerRequests', ['$rootScope', '$http', 'settings', function ($rootScope, $http, settings) {
      return {
         get: function (oData) {
            if (!oData) oData = {}; oData.SportID = $rootScope.getSportSelected().SportID;
            return $http.get(settings.wsVirtualPath + '/UserMatchPlayerRequests.aspx', oData);
         }
      };
   }])
   .factory('ws_UserPersonalInfo', ['$rootScope', '$http', 'settings', function ($rootScope, $http, settings) {
      return {
         post: function (oData) {
            if (!oData) oData = {}; oData.SportID = $rootScope.getSportSelected().SportID;
            return $http.post(settings.wsVirtualPath + '/UserPersonalInfo.aspx', oData);
         }
      };
   }])
   .factory('ws_UserPrivacy', ['$rootScope', '$http', 'settings', function ($rootScope, $http, settings) {
      return {
         get: function (oData) {
            if (!oData) oData = {}; oData.SportID = $rootScope.getSportSelected().SportID;
            return $http.get(settings.wsVirtualPath + '/UserPrivacy.aspx', oData);
         },
         put: function (oData) {
            if (!oData) oData = {}; oData.SportID = $rootScope.getSportSelected().SportID;
            return $http.put(settings.wsVirtualPath + '/UserPrivacy.aspx', oData);
         }
      };
   }])
   .factory('ws_ChangePassword', ['$rootScope', '$http', 'settings', function ($rootScope,$http, settings) {
      return {
         post: function (oData) {
            if (!oData) oData = {}; oData.SportID = $rootScope.getSportSelected().SportID;
            return $http.post(settings.wsVirtualPath + '/ChangePassword.aspx', oData);
         }
      };
   }]);

	/*----------------------------------- END PLAYERS WS ---------------------------------*/