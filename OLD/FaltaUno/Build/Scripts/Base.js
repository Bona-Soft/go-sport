/* HayEquipoApp App */

/* Configure ocLazyLoader(refer: https://github.com/ocombe/ocLazyLoad) */
app.config(['$ocLazyLoadProvider', '$sceDelegateProvider', function ($ocLazyLoadProvider, $sceDelegateProvider) {
   $ocLazyLoadProvider.config({
      // global configs go here
   });

   $sceDelegateProvider.resourceUrlWhitelist([
      'self',
      'http://sys.cubox.com.ar/**',
      'http://*.connectingonline.com.ar/**',
      'http://*.cylarcom.net/**',
      'http://*.cylarcom.biz/**',
      'http://*.gestionbos.com.ar/**',
   ]);
}]);

/* Setup global settings */
app.factory('settings', ['$rootScope', function ($rootScope) {
   {
      // supported languages

      //var App = function() {
      //	// IE mode
      //	var isRTL = false;
      //	var isIE8 = false;
      //	var isIE9 = false;
      //	var isIE10 = false;

      //	var resizeHandlers = [];

      //	var assetsPath = 'Assets/';

      //	var globalImgPath = 'global/img/';

      //	var globalPluginsPath = 'global/plugins/';

      //	var globalCssPath = 'global/css/';

      //	// theme layout color set

      //	var brandColors = {
      //		'blue': '#89C4F4',
      //		'red': '#F3565D',
      //		'green': '#1bbc9b',
      //		'purple': '#9b59b6',
      //		'grey': '#95a5a6',
      //		'yellow': '#F8CB00'
      //	};

      //	// runs callback functions set by App.addResponsiveHandler().
      //	var _runResizeHandlers = function () {
      //		// reinitialize other subscribed elements
      //		for (var i = 0; i < resizeHandlers.length; i++) {
      //			var each = resizeHandlers[i];
      //			each.call();
      //		}
      //	};

      //	//* END:CORE HANDLERS *//

      // handle the layout reinitialization on window resize
      //var handleOnResize = function () {
      //    var resize;
      //    if (isIE8) {
      //        var currheight;
      //        $(window).resize(function () {
      //            if (currheight == document.documentElement.clientHeight) {
      //                return; //quite event since only body resized not window.
      //            }
      //            if (resize) {
      //                clearTimeout(resize);
      //            }
      //            resize = setTimeout(function () {
      //                _runResizeHandlers();
      //            }, 50); // wait 50ms until window resize finishes.
      //            currheight = document.documentElement.clientHeight; // store last body client height
      //        });
      //    } else {
      //        $(window).resize(function () {
      //            if (resize) {
      //                clearTimeout(resize);
      //            }
      //            resize = setTimeout(function () {
      //                _runResizeHandlers();
      //            }, 50); // wait 50ms until window resize finishes.
      //        });
      //    }
      //};

      //	return {
      //		//public function to add callback a function which will be called on window resize
      //		addResizeHandler: function (func) {
      //			resizeHandlers.push(func);
      //		},
      //
      //		// check for device touch support
      //		isTouchDevice: function () {
      //			try {
      //				document.createEvent("TouchEvent");
      //				return true;
      //			} catch (e) {
      //				return false;
      //			}
      //		},
      //
      //		// get layout color code by color name
      //		getBrandColor: function (name) {
      //			if (brandColors[name]) {
      //				return brandColors[name];
      //			} else {
      //				return '';
      //			}
      //		},
      //
      //		getResponsiveBreakpoint: function (size) {
      //			// bootstrap responsive breakpoints
      //			var sizes = {
      //				'xs': 480,     // extra small
      //				'sm': 768,     // small
      //				'md': 992,     // medium
      //				'lg': 1200     // large
      //			};
      //
      //			return sizes[size] ? sizes[size] : 0;
      //		}
      //	}
      //
      //}();
   }
   var settings = {
      layoutDef: {
         pageSidebarClosed: false, // sidebar menu state
         pageContentWhite: true, // set page content layout
         pageBodySolid: false, // solid body color state
         pageAutoScrollOnLoad: 1000 // auto scroll to top on page load
      },
      assetsPath: 'Assets',
      globalPath: 'Assets/global',
      layoutPath: 'Styles/layouts',
      sportSelected: { SportID: "0", Name: "Todos", Value: "all" },
      wsVirtualPath: '',
      language: 'ES', //TODO: Cargar del usuario el default language y de cookies
      dateFormat: 'dd/MM/yyyy',
      timeFormat: 'hh:mm a',
      datetimeFormat: 'dd/MM/yyyy h:mm:ss A'
   };

   $rootScope.settings = settings;

   return settings;
}]);

/* Setup Rounting For All Pages */
app.config(['$stateProvider', '$controllerProvider', '$compileProvider', '$filterProvider', '$provide', '$urlRouterProvider', '$locationProvider', '$animateProvider', 'mybValidatorServiceProvider', 'SSEServiceConfigProvider',
   function ($stateProvider, $controllerProvider, $compileProvider, $filterProvider, $provide, $urlRouterProvider, $locationProvider, $animateProvider, mybValidatorServiceProvider, SSEServiceConfigProvider) {
      app.controller = $controllerProvider.register;
      app.directive = $compileProvider.directive;
      app.component = $compileProvider.component;
      app.animation = $animateProvider.register;
      app.filter = $filterProvider.register;
      app.factory = $provide.factory;
      app.service = $provide.service;
      app.constant = $provide.constant;
      app.value = $provide.value;
      app.decorator = $provide.decorator;

      // the known route, with missing '/' - let's create alias
      $urlRouterProvider.when('', '/');
      // Redirect any unmatched url
      $urlRouterProvider.otherwise("Error");
      $locationProvider.hashPrefix('');

      mybValidatorServiceProvider.setDefaults({
         requiredMessage: 'Campo obligatorio.',
         emailMessage: 'Direccion de correo electronico invalida.',
         passwordMessage: 'La contraseña es demasiado debil.',
         repeatPasswordMessage: 'Las contraseñas no coinciden.',
         minlength: 'El campo es demasiado corto.',
         maxlength: 'El campo es demasiado largo.'
      });

      SSEServiceConfigProvider.config.servers[0] = {
         name: 'KeepAlive',
         wsPath: '/KeepAlive.aspx',
         callback: function (data,event) {
            console.log(data);
            console.log(event);
            //$scope.msg = JSON.parse(msg.data)
         }
      };

      $stateProvider
         // Home
         .state('Home', {
            url: "/",
            templateUrl: "Templates/Home.html",
            data: {
               pageTitle: 'Inicio',
               access: {
                  authenticationRequired: false,
                  authenticationFailState: 'Inicio'
               }
            },
            controller: "Home",
            resolve: {
               deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                  return $ocLazyLoad.load({
                     name: appName,
                     insertBefore: '#ng_load_plugins_before', // load the above css files before a LINK element with this ID. Dynamic CSS files must be loaded between core and theme css files
                     files: [
                        'Controllers/Home.js'
                     ]
                  });
               }]
            }
         })

         // Home Usuario
         .state('Inicio', {
            url: "/Inicio",
            templateUrl: "Templates/Inicio.html",
            data: {
               pageTitle: 'Inicio',
               access: {
                  authenticationRequired: true,
                  authenticationFailState: 'Home'
               }
            },
            controller: "Home",
            resolve: {
               deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                  return $ocLazyLoad.load({
                     name: appName,
                     insertBefore: '#ng_load_plugins_before', // load the above css files before a LINK element with this ID. Dynamic CSS files must be loaded between core and theme css files
                     files: [
                        'Controllers/Home.js'
                     ]
                  });
               }]
            }
         })

         // Select sport
         .state('SelectSport', {
            url: "/SelectSport",
            templateUrl: "Templates/SelectSport.html",
            data: {
               pageTitle: 'Select Sport'
            },
            controller: "SelectSport",
            params: { 'previousStateName': null },
            resolve: {
               deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                  return $ocLazyLoad.load({
                     name: appName,
                     insertBefore: '#ng_load_plugins_before', // load the above css files before a LINK element with this ID. Dynamic CSS files must be loaded between core and theme css files
                     files: [
                        'Controllers/SelectSport.js'
                     ]
                  });
               }]
            }
         })

         //Error State
         .state('Error', {
            url: "/Error",
            templateUrl: "Templates/Error.html",
            data: { pageTitle: 'Error' }
         })

         //Service unavailable
         .state('ServiceUnavailable', {
            url: "/ServiceUnavailable",
            templateUrl: "Templates/ServiceUnavailable.html",
            data: { pageTitle: 'Error' }
         });
   }]);

/* Init global settings and run the app */
app.run(["$rootScope", "settings", "$http", "$injector", "$state", "$location", "$filter",
   "mybErrorServiceConfig", "userService", "authorization", "mybAuthenticationServiceConfig",
   "authenticationService", "alertService", "$templateCache", "cookieService", "inputMethods", "SSEService",
   "ws_Sport", "ws_Fields", "ws_ChallengeTypes", "ws_MatchTypes", "ws_UserActiveSport",
   function ($rootScope, settings, $http, $injector, $state, $location, $filter,
      mybErrorServiceConfig, userService, authorization, mybAuthenticationServiceConfig, 
      authenticationService, alertService, $templateCache, cookieService, inputMethods, SSEService, 
      ws_Sport, ws_Fields, ws_ChallengeTypes, ws_MatchTypes, ws_UserActiveSport) {


      if ($rootScope.isFirstLoad) {
         $rootScope.mainLoading = 1;
      }

      //DEFINITIONS
      {
         var _userLogin = function (callback) {
            authenticationService.isLogged().then(function (response) {
               callback();
               SSEService.start();
            }, function (error) {
               callback(error);
            });

         };

         var _setLenguage = function (callback) {
            if (userService != null
               && userService.user() != null
               && availableLenguages.includes(userService.user().DefaultLanguage)) {
               $rootScope.$settings.language = userService.user().DefaultLanguage;
            }
            else if (availableLenguages.includes(cookieService.getCookie("BaseAppSessionLanguage"))) {
               $rootScope.$settings.language = document.BaseAppSessionLanguage;
            }
            callback();
         };

         var _setActiveSport = function (callback) {
            if (userService != null && userService.user() != null) {
               if (userService.user().ActiveSport != null) {
                  $rootScope.settings.sportSelected = userService.user().ActiveSport;
               }
               else if (cookieService.getJsonCookie("SportSelected") != null) {
                  $rootScope.settings.sportSelected = cookieService.getJsonCookie("SportSelected");
               }
               else if (userService.user().DefaulSport != null) {
                  $rootScope.settings.sportSelected = userService.user().ActiveSport;
               }
            } else if (cookieService.getJsonCookie("SportSelected") != null) {
               $rootScope.settings.sportSelected = cookieService.getJsonCookie("SportSelected");
            }
            callback();
         };

         $rootScope.getChallengeType = function (id) {
            return $rootScope.challengeTypes.filter(function (challengeType) {
               return challengeType.ChallengeTypeID == id;
            })[0];
         };

         $rootScope.getField= function (id) {
            return $rootScope.fields.filter(function (field) {
               return field.FieldID == id;
            })[0];
         };

         $rootScope.getMatchType = function (id) {
            return $rootScope.matchTypes.filter(function (matchType) {
               return matchType.MatchTypeID == id;
            })[0];
         };

         $rootScope.getCurrentUserAvatarURL = function () {
            if (userService != null && userService.user() != null && userService.user().Avatar != null) {
               return "UploadedFiles/Avatars/" + userService.user().UserID + "/Thumbnails/" + userService.user().Avatar;
            }
            return settings.layoutPath + "/default/img/avatar.png";
         };

         $rootScope.getUserAvatarURL = function (user) {
            if (user != null && user.Avatar != null) {
               return "UploadedFiles/Avatars/" + user.UserID + "/Thumbnails/" + user.Avatar;
            }
            return settings.layoutPath + "/default/img/avatar.png";
         };

         $rootScope.getCurrentUserFullAvatarURL = function () {
            if (userService != null && userService.user() != null && userService.user().Avatar != null) {
               return "UploadedFiles/Avatars/" + userService.user().UserID + "/" + userService.user().Avatar;
            }
            return settings.layoutPath + "/default/img/avatar.png";
         };

         $rootScope.getUserFullAvatarURL = function (user) {
            if (user != null && user.Avatar != null) {
               return "UploadedFiles/Avatars/" + user.UserID + "/" + user.Avatar;
            }
            return settings.layoutPath + "/default/img/avatar.png";
         };

         $rootScope.getSportSelected = function () {
            return settings.sportSelected;
         };

         $rootScope.changeSport = function (sport) {
            if (!isNaN(sport)){
               sport = $rootScope.getSport(sport);
            }
            if (sport != null) {
               $rootScope.settings.sportSelected = sport;
               if (userService.user()) {
                  userService.user().ActiveSport = sport;
                  ws_UserActiveSport.post(sport);
               }
               cookieService.setJsonCookie("SportSelected", sport);
               $rootScope.$state.reload();
            }       
         };

         $rootScope.getSport = function (SportID) {
            var sports = $filter('filter')($rootScope.sports, { "SportID": SportID });
            var sport = null;
            if (sports.length == 1) {
               sport = sports[0];
            }
            return sport;
         };

         $rootScope.getCurrentPlayer = function () {
            var players = $filter('filter')(userService.user().Players, { "Sport": { "SportID": $rootScope.getSportSelected().SportID } });
  
            if (players.length == 1) {
               return players[0];
            }
            return null;
         };

         $rootScope.getFrontDateTime = function (date, time) {
            var _date = date || '1970-01-01Z00:00:00:000';
            var _time = time || '00:00';
            var _hour = '00';
            var _minute = '00';
            

            try {
               _hour = moment(_time, 'hh:mm A').get('hour');
               _minute = moment(_time, 'hh:mm A').get('minute');

               _date = moment(_date, 'DD/MM/YYYY h:mm:ss A').set({ 'hour': _hour, 'minute': _minute }).toDate();
            } catch
            {
               return null;
            }

            return _date;
         };

      }


      //DATA INITIALIZATION
      {
         $rootScope.currentPath = $location.path();
         $rootScope.$state = $state; // state to be accessed from view
         $rootScope.$settings = settings; // settings to be accessed from view
         $rootScope.lastLocation = [];
         $rootScope.noScroll = false;
         $rootScope.isFirstLoad = true;
         $rootScope.mainLoading = 0;
         $rootScope.stateIsLooping = false;
         $rootScope.inputMethods = inputMethods;

         var availableLenguages = ['ES', 'EN'];

         //Preloaded for if ws_Sport fail
         $rootScope.sports = [
            { SportID: "1", Name: "Futbol", Value: "futbol" },
            { SportID: "2", Name: "Basket", Value: "basket" },
            { SportID: "3", Name: "Volley", Value: "volley" },
            { SportID: "4", Name: "Tenis", Value: "tenis" }
         ];
   
      }


      //WS LOADING INITIAL INFORMATION
      {
         ws_Sport.get().then(
            function (response) {
               $rootScope.sports = response.data;
            },
            function (error) {
               return;
            }
         );
         ws_Fields.get().then(
            function (response) {
               $rootScope.fields = response.data;
            },
            function (error) {
               return;
            }
         );
         ws_ChallengeTypes.get().then(
            function (response) {
               $rootScope.challengeTypes = response.data;
            },
            function (error) {
               return;
            }
         );
         ws_MatchTypes.get().then(
            function (response) {
               $rootScope.matchTypes = response.data;
            },
            function (error) {
               return;
            }
         );
      }


      //CONFIGURATION
      {
         //todo: pasar a la config arriba
         mybAuthenticationServiceConfig.setDefaults({
            wsVirtualPath: settings.wsVirtualPath,
            //ws_Login: null, //override this with you custom webservice
            //ws_Logout: null, //override this with you custom webservice
            //ws_Register: null, //override this with you custom webservice
            //ws_VerifyUser: null, //override this with you custom webservice
            userService: userService
         });

         mybErrorServiceConfig.setDefaults({
            showServerErrors: false, //for debug purposes
            showClientErrors: false, //for debug purposes
            defaultServerMessage: 'Hubo un error interno del servidor.',
            defaultClientMessage: 'Hubo un error en el cliente.',
            defaultStatusCode: '',
            defaultMessage: 'No es posible ver el contenido de esta página.'
         });

         SSEService.config.servers[0] = {
            name: 'KeepAlive',
            wsPath: '/KeepAlive.ashx',
            callback: function (data, event) {
               var news = userService.user().Base.News > 0 ? userService.user().Base.News : 0;
               userService.user().Base.News = news + data.news;
            }
         };


         $templateCache.put('Templates/ServiceUnavailable.html'
            , '<div><h4>Servicio no disponible</h4></div>');

         $rootScope.$on("$locationChangeStart", function (event, next, current) {
            //Reseteo la variable noScroll
            $rootScope.noScroll = false;
         });

         $rootScope.$on("$locationChangeSuccess", function (event, next, current) {
            var path = $location.url();
            $rootScope.currentPath = $location.path();

            alertService.clear();
            if (path == "/Error")
               return;

            if ($rootScope.lastLocation.length == 0) {
               $rootScope.lastLocation.push(path);
            }
            else if ($rootScope.lastLocation[$rootScope.lastLocation.length - 1] != path) {
               $rootScope.lastLocation.push(path);
               if ($rootScope.lastLocation.length > 20) {
                  $rootScope.lastLocation.shift();
               }
            }
         });

         $rootScope.$on('$stateChangeStart',
            function (event, toState, toStateParams, fromState, fromParams) {
               $rootScope.toState = toState;
               $rootScope.toStateParams = toStateParams;
               var stateDataAccess = null;
               var authenticationRequired = null;
               var sportSelectedRequired = false;
               var parametersRequired = [];
               var toStateName = toState.name;

               if ($rootScope.stateIsLooping) {
                  $rootScope.stateIsLooping = false;
                  return;
               }

               if (toState.data && toState.data.access) {
                  stateDataAccess = toState.data.access;
                  if (stateDataAccess.authenticationRequired != null) {
                     authenticationRequired = stateDataAccess.authenticationRequired;
                  }
                  if (stateDataAccess.sportSelectedRequired) {
                     sportSelectedRequired = stateDataAccess.sportSelectedRequired;
                  }
                  if (stateDataAccess.parametersRequired && stateDataAccess.parametersRequired > 0) {
                     parametersRequired = stateDataAccess.parametersRequired;
                  }
               };

               
               var checkSportSelected = function () {
                  if (sportSelectedRequired && $rootScope.settings.sportSelected.Value == 'all'){
                     toStateParams.previousStateName = toState.name;
                     toStateName = 'SelectSport';
                     return false;
                  }
                  return true;
               };
               
               var parametersProvided = function () {
                  for (var i = 0; i < parametersRequired.length; i++)
                  {
                     if (toState.resolve[parametersRequired[i]] == null
                        || toState.resolve[parametersRequired[i]][toState.resolve[parametersRequired[i]].length - 1] == null
                        || toState.resolve[parametersRequired[i]][toState.resolve[parametersRequired[i]].length - 1](toStateParams) == ""
                        || toState.resolve[parametersRequired[i]][toState.resolve[parametersRequired[i]].length - 1](toStateParams) == null) {
                        return false;
                     }
                  }
                  return true;
               };

               var checkParametersRequired = function () {
                  if (parametersRequired.length > 0 && !parametersProvided()) {
                     toStateParams.previousStateName = toState.name;
                     toStateName = stateDataAccess.authenticationFailState;  
                  }
               };

               var checkAuthorization = function () {
                  if (authenticationRequired != null
                     && ((authenticationRequired && !userService.isAuthenticated())
                        || (!authenticationRequired && userService.isAuthenticated()))) {
                     if (stateDataAccess != null) {
                        $state.go(stateDataAccess.authenticationFailState, toStateParams, { reload: stateDataAccess.authenticationFailState });
                     } else {
                        $rootScope.stateIsLooping = true;
                        if (checkSportSelected()) {
                           checkParametersRequired();
                        }
                        
                        $state.go(toStateName, toStateParams);
                     }
                  } else {
                     $rootScope.stateIsLooping = true;
                     if (checkSportSelected()) {
                        checkParametersRequired();
                     }
                     $state.go(toStateName, toStateParams, { reload: toState.name });
                  }
               };

               event.preventDefault();

               if ($rootScope.isFirstLoad || (authenticationRequired && !userService.isAuthenticated())) {
                  $rootScope.isFirstLoad = false;

                  authenticationService.isLogged().then(
                     function (result) {
                        checkAuthorization();
                        SSEService.start();
                        $rootScope.mainLoading = 0;
                     },
                     function (error) {
                        checkAuthorization();
                        $rootScope.mainLoading = 0;
                     }
                  );
               } else {
                  checkAuthorization();
                  $rootScope.mainLoading = 0;
               }

               if (userService.isUserResolved())
                  authorization.authorize();
            });

         $rootScope.$on('$stateChangeSuccess', function () {
            alertService.clear();
            $(window).resize();
         });

         $rootScope.$on("$stateChangeError", function (event, toState, toParams, fromState, fromParams, error) {
            $state.go("ServiceUnavailable");
            $(window).resize();
         });
      }


      async.series([
         _userLogin,
         _setLenguage,
         _setActiveSport
      ],
         function (err, results) {
            if (err) {
               console.log(err);
            }
         });
      
      //TODO: hacer un $q deferred entre sports, login etc. Esperar a q todo los http terminen
      //var promises = [];
      //$scope.cats.forEach(function (cat) {
      //   promises.push(cat.$update());
      //});

      //$q.all(promises).then(function () {
      //   // do something after all http requests
      //});
   }]);