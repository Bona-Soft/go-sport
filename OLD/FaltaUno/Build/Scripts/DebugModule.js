app.module('Debug', [
   "ui.router",
   "ui.bootstrap",
   "oc.lazyLoad",
   "ngSanitize",
   "ngResource"
])

   .config(['$stateProvider', '$urlRouterProvider', function ($stateProvider, $urlRouterProvider) {
      $stateProvider

         .state('Debug', {
            url: "/Debug",
            templateUrl: "Templates/Debug.html",
            data: {
               pageTitle: 'Debug',
               access: {
                  authenticationFailState: 'Home',
                  sportSelectedRequired: false
               }
            },
            controller: "Debug",
            resolve: {
               deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                  return $ocLazyLoad.load({
                     name: appName,
                     insertBefore: '#ng_load_plugins_before', // load the above css files before a LINK element with this ID. Dynamic CSS files must be loaded between core and theme css files
                     files: [
                        'Controllers/Debug.js'
                     ]
                  });
               }]
            }
         });
   }])

   /*-----------------------------------   MATCH WS   ---------------------------------*/

   .factory('ws_Log', ['$http', 'settings', function ($http, settings) {
      return {
         get: function (oData) {
            return $http.get(settings.wsVirtualPath + '/Log.aspx?', { params: oData });
         }
      };
   }])
   .factory('ws_Jobs', ['$http', 'settings', function ($http, settings) {
      return {
         post: function (oData) {
            return $http.post(settings.wsVirtualPath + '/Jobs.aspx', oData);
         },
         get: function (oData) {
            return $http.get(settings.wsVirtualPath + '/Jobs.aspx?', { params: oData });
         }
      };
   }]);

/*----------------------------------- END MATCH WS ---------------------------------*/