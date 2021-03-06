app.module('Match', [
   "ui.router",
   "ui.bootstrap",
   "oc.lazyLoad",
   "ngSanitize",
   "ngResource"
])

   .config(['$stateProvider', '$urlRouterProvider', function ($stateProvider, $urlRouterProvider) {
      $stateProvider

         .state('Match', {
            url: "/Match/:ID",
            templateUrl: "Templates/Match.html",
            data: {
               pageTitle: 'Match',
               access: {
                  authenticationRequired: true,
                  authenticationFailState: 'Home',
                  sportSelectedRequired: true
               }
            },
            params: { 'MatchID': null },
            controller: "Match",
            resolve: {
               MatchID: ['$stateParams', function ($stateParams) {
                  if ($stateParams.MatchID == null) {
                     if ($stateParams.ID == null) {
                        return 0;
                     }
                     else {
                        return $stateParams.ID;
                     }
                  }  
                  return $stateParams.MatchID;
               }],
               deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                  return $ocLazyLoad.load({
                     name: appName,
                     insertBefore: '#ng_load_plugins_before', // load the above css files before a LINK element with this ID. Dynamic CSS files must be loaded between core and theme css files
                     files: [
                        'Assets/global/plugins/ngMap/ng-map.min.js',  
                        'Controllers/Match.js'
                     ]
                  });
               }]
            }
         })
         .state('Match/New/Players', {
            url: "/EditMatchPlayers/:MatchID",
            templateUrl: "Templates/MatchPlayersEdit.html",
            data: {
               pageTitle: 'Edit Match Players',
               access: {
                  authenticationRequired: true,
                  authenticationFailState: 'Home',
                  sportSelectedRequired: true
               }
            },
            params: {'Match': null},
            controller: "MatchPlayersEdit",
            resolve: {
               Match: ['$stateParams', function ($stateParams) {
                  if ($stateParams.Match == null)
                     return { 'MatchID': $stateParams.MatchID };
                  return $stateParams.Match;
               }],
               deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                  return $ocLazyLoad.load({
                     name: appName,
                     insertBefore: '#ng_load_plugins_before', // load the above css files before a LINK element with this ID. Dynamic CSS files must be loaded between core and theme css files
                     files: [
                        'Scripts/RequestStateService.js',
                        'Controllers/MatchPlayersEdit.js'

                     ]
                  });
               }]
            }
         })

         .state('Match/View', {
            url: "/ViewMatch/:ID",
            templateUrl: "Templates/MatchView.html",
            data: {
               pageTitle: 'View Match',
               access: {
                  authenticationRequired: true,
                  authenticationFailState: 'Home',
                  sportSelectedRequired: true
               }
            },
            params: { 'MatchID': null },
            controller: "MatchView",
            resolve: {
               MatchID: ['$stateParams', function ($stateParams) {
                  if ($stateParams.MatchID == null)
                     return $stateParams.ID;
                  return $stateParams.MatchID;
               }],
               deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                  return $ocLazyLoad.load({
                     name: appName,
                     insertBefore: '#ng_load_plugins_before', // load the above css files before a LINK element with this ID. Dynamic CSS files must be loaded between core and theme css files
                     files: [
                        'Controllers/MatchView.js'

                     ]
                  });
               }]
            }
         })

         .state('Match/List', {
            url: "/Matchs",
            templateUrl: "Templates/MatchList.html",
            data: { pageTitle: 'Match' },
            controller: "MatchList",
            resolve: {
               deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                  return $ocLazyLoad.load({
                     name: appName,
                     insertBefore: '#ng_load_plugins_before', // load the above css files before a LINK element with this ID. Dynamic CSS files must be loaded between core and theme css files
                     files: [
                        'Controllers/MatchList.js'
                     ]
                  });
               }]
            }
         })

         .state('Match/Search', {
            url: "/SearchMatch",
            templateUrl: "Templates/MatchSearch.html",
            data: { pageTitle: 'New Match' },
            controller: "MatchSearch",
            resolve: {
               deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                  return $ocLazyLoad.load({
                     name: appName,
                     insertBefore: '#ng_load_plugins_before', // load the above css files before a LINK element with this ID. Dynamic CSS files must be loaded between core and theme css files
                     files: [
                        'Controllers/MatchSearch.js'
                     ]
                  });
               }]
            }
         });
   }])

   /*-----------------------------------   MATCH WS   ---------------------------------*/

   .factory('ws_Match', ['$rootScope', '$http', 'settings', function ($rootScope, $http, settings) {
      return {
         post: function (oData) {
            if (!oData) oData = {}; oData.SportID = $rootScope.getSportSelected().SportID;
            return $http.post(settings.wsVirtualPath + '/Match.aspx', oData);
         },
         get: function (oData) {
            if (!oData) oData = {}; oData.SportID = $rootScope.getSportSelected().SportID;
            return $http.get(settings.wsVirtualPath + '/Match.aspx?', { params: oData });
         },
         put: function (oData) {
            if (!oData) oData = {}; oData.SportID = $rootScope.getSportSelected().SportID;
            return $http.put(settings.wsVirtualPath + '/Match.aspx', oData);
         }
      };
   }])
   .factory('ws_MatchSearch', ['$rootScope', '$http', 'settings', function ($rootScope, $http, settings) {
      return {
         get: function (oData) {
            if (!oData) oData = {}; oData.SportID = $rootScope.getSportSelected().SportID;
            return $http.get(settings.wsVirtualPath + '/MatchSearch.aspx', { params: oData });
         }
      };
   }])
   .factory('ws_MatchCurrentUser', ['$rootScope', '$http', 'settings', function ($rootScope, $http, settings) {
      return {
         get: function (oData) {
            if (!oData) oData = {}; oData.SportID = $rootScope.getSportSelected().SportID;
            return $http.get(settings.wsVirtualPath + '/MatchCurrentUser.aspx', { params: oData });
         }
      };
   }])
   .factory('ws_MatchPlayerRequests', ['$rootScope', '$http', 'settings', function ($rootScope, $http, settings) {
      return {
         post: function (oData) {
            if (!oData) oData = {}; oData.SportID = $rootScope.getSportSelected().SportID;
            return $http.post(settings.wsVirtualPath + '/MatchPlayerRequests.aspx', oData);
         },
         get: function (oData) {
            if (!oData) oData = {}; oData.SportID = $rootScope.getSportSelected().SportID;
            return $http.get(settings.wsVirtualPath + '/MatchPlayerRequests.aspx', { params: oData });
         }
      };
   }]);

/*----------------------------------- END MATCH WS ---------------------------------*/