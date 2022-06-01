(function (root, factory) {
    "use strict";

    /*global define*/
    if (typeof define === 'function' && define.amd) {
        define(['angular', 'userService'], factory);  // AMD
    } else if (typeof module === 'object' && module.exports) {
        module.exports = factory(require('angular'), require('userService')); // Node
    } else {
        factory(root.angular, root.swal);					// Browser
    }
}(this, function (angular, swal) {
    "use strict";

    angular.module('myb.ngUserService', [])

       // 2. set a constant
       .constant('MODULE_VERSION', '1.0.0')

       // "user" is a service that tracks the user's identity.
       // calling identity() returns a promise while it does what you need it to do
       // to look up the signed-in user's identity info. for example, it could make an
       // HTTP request to a rest endpoint which returns the user's name, roles, etc.
       // after validating an auth token in a cookie. it will only do this identity lookup
       // once, when the application first runs. you can force re-request it by calling identity(true)
       .factory('userService', ['$q', '$http', '$timeout', 'ws_Login',
          function ($q, $http, $timeout, ws_Login) {
             var _user = undefined,
                _authenticated = false;

             return {
                user: function () {
                   return _user;
                },
                isUserResolved: function () {
                   return angular.isDefined(_user);
                },
                isAuthenticated: function () {
                   return _authenticated;
                },
                isInRole: function (role) {
                   if (!_authenticated || !_user.roles) return false;

                   return _user.roles.indexOf(role) != -1;
                },
                isInAnyRole: function (roles) {
                   if (!_authenticated || !_user.roles) return false;

                   for (var i = 0; i < roles.length; i++) {
                      if (this.isInRole(roles[i])) return true;
                   }

                   return false;
                },
                authenticate: function (user) {
                   _user = user;
                   _authenticated = user != null;

                   // for this demo, we'll store the 'user' in localStorage. For you, it could be a cookie, sessionStorage, whatever
                   /*
                   if (user) localStorage.setItem("demo.identity", angular.toJson(user));
                   else localStorage.removeItem("demo.identity");
               */
                }
             };
          }
       ])

       /* //TODO: Revisar si se implementa sistema de permisos y como.*/

        .factory('authorization', ['$rootScope', '$state', 'userService',
            function ($rootScope, $state, userService) {
                return {
                    authorize: function () {
                       var isAuthenticated = userService.isAuthenticated();

                       if ($rootScope.toState.data.roles
                          && $rootScope.toState.data.roles.length > 0
                          && !userService.isInAnyRole($rootScope.toState.data.roles)) {
                          if (isAuthenticated) {
                             // user is signed in but not
                             // authorized for desired state
                             //$state.go('accessdenied');
                             return -1; //no tiene permiso
                          } else {
                             // user is not authenticated. Stow
                             // the state they wanted before you
                             // send them to the sign-in state, so
                             // you can return them when you're done
                             $rootScope.returnToState = $rootScope.toState;
                             $rootScope.returnToStateParams = $rootScope.toStateParams;

                             // now, send them to the signin state
                             // so they can log in
                             //$state.go('signin');
                             return -2 //no esta logueado
                          }
                       }

                       return 1;
                    }
                };
            }
        ]);
}));