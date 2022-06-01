angular.module('Authentication', [
    "ui.router",
    "ui.bootstrap",
    "oc.lazyLoad",
    "ngSanitize",
    "ngResource"
]);

angular.module('Authentication').config(['$stateProvider', '$urlRouterProvider', function ($stateProvider, $urlRouterProvider) {
    $stateProvider

        //VerifyUser
        .state('VerifyUser', {
            url: "/VerifyUser/:email/:code",
            templateUrl: "Templates/VerifyUser.html",
            data: { pageTitle: 'Verificacion de correo electronico' },
            controller: "VerifyUser",
            resolve: {
                deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        name: 'HayEquipoApp', //TODOFE: verificar si se puede cambiar a BaseAPP o algo asi.
                        insertBefore: '#ng_load_plugins_before', // load the above css files before a LINK element with this ID. Dynamic CSS files must be loaded between core and theme css files
                        files: [
                            'Controllers/VerifyUser.js'
                        ]
                    });
                }]
            }
        })

        // Register
        .state('Register', {
            url: "/Register",
            templateUrl: "Templates/Register.html",
            data: { pageTitle: 'Register' },
            controller: "Register",
            resolve: {
                deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        name: 'HayEquipoApp',
                        insertBefore: '#ng_load_plugins_before', // load the above css files before a LINK element with this ID. Dynamic CSS files must be loaded between core and theme css files
                        files: [
                            'Controllers/Register.js',
                        ]
                    });
                }]
            }
        })
}]);