app.module('Team', [
   "ui.router",
   "ui.bootstrap",
   "oc.lazyLoad",
   "ngSanitize",
   "ngResource"
])

   .config(['$stateProvider', '$urlRouterProvider', function ($stateProvider, $urlRouterProvider) {
      $stateProvider

         //Pedidos Finalizar POPUP v3
         .state('Team/Profile', {
            url: "/TeamProfile/:sportID/:teamID",
            templateUrl: "Templates/TeamProfile.html",
            data: { pageTitle: 'Team Profile' },
            controller: "TeamProfile",
            resolve: {
               sportID: ['$stateParams', function ($stateParams) {
                  return $stateParams.sportID;
               }],
               teamID: ['$stateParams', function ($stateParams) {
                  return $stateParams.playerID;
               }],
               deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                  return $ocLazyLoad.load({
                     name: appName,
                     insertBefore: '#ng_load_plugins_before', // load the above css files before a LINK element with this ID. Dynamic CSS files must be loaded between core and theme css files
                     files: [
                        'Controllers/TeamProfile.js'
                     ]
                  });
               }]
            }
         })
         .state('Team/Ranking', {
            url: "/TeamRanking",
            templateUrl: "Templates/TeamRanking.html",
            data: { pageTitle: 'Team Raking' },
            controller: "TeamRanking",
            resolve: {
               deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                  return $ocLazyLoad.load({
                     name: appName,
                     insertBefore: '#ng_load_plugins_before', // load the above css files before a LINK element with this ID. Dynamic CSS files must be loaded between core and theme css files
                     files: [
                        'Controllers/TeamRanking.js'
                     ]
                  });
               }]
            }
         })
         .state('Team/Search', {
            url: "/SearchTeam",
            templateUrl: "Templates/TeamSearch.html",
            data: { pageTitle: 'Search Team' },
            controller: "TeamSearch",
            resolve: {
               deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                  return $ocLazyLoad.load({
                     name: appName,
                     insertBefore: '#ng_load_plugins_before', // load the above css files before a LINK element with this ID. Dynamic CSS files must be loaded between core and theme css files
                     files: [
                        'Controllers/TeamSearch.js'
                     ]
                  });
               }]
            }
         });
   }])

   /*-----------------------------------   PLAYERS WS   ---------------------------------*/

   .factory('wsTeam', function ($http) {
      return {
         get: function (oData) {
            return $http.get('WebServices/PlayerProfile.aspx?', { params: oData });
         }
      }
   });

	/*----------------------------------- END PLAYERS WS ---------------------------------*/