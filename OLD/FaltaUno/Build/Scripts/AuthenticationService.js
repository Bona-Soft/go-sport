window.fbAsyncInit = function () {
   FB.init({
      appId: '367480407016923',
      autoLogAppEvents: true,
      xfbml: true,
      version: 'v2.10'
   });
   FB.AppEvents.logPageView();
};

(function (d, s, id) {
   var js, fjs = d.getElementsByTagName(s)[0];
   if (d.getElementById(id)) { return; }
   js = d.createElement(s); js.id = id;
   js.src = "//connect.facebook.net/en_US/sdk.js";
   fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));

(function (root, factory) {
   "use strict";

   /*global define*/
   if (typeof define === 'function' && define.amd) {
      define(['angular', 'authenticationService'], factory);  // AMD
   } else if (typeof module === 'object' && module.exports) {
      module.exports = factory(require('angular'), require('authenticationService')); // Node
   } else {
      factory(root.angular);					// Browser
   }
}(this, function (angular) {
   "use strict";

   angular.module('myb.ngAuthenticationService', [])

      .provider('mybAuthenticationServiceConfig', function () {
         var _config = {
            wsVirtualPath: '',
            ws_Login: null,
            ws_Logout: null,
            ws_Register: null,
            ws_VerifyUser: null,
            userService: null
         };

         return {
            $get: function () {
               return {
                  setDefaults: function (options) {
                     // order of precedence: element options, theme, defaults.
                     _config = angular.extend(_config, options);
                  },
                  config: _config
               };
            }
         };
      })

      // 2. set a constant
      .constant('MODULE_VERSION', '1.0.0')

      .factory('ws_Login', ['$http', 'mybAuthenticationServiceConfig', function ($http, mybAuthenticationServiceConfig) {
         return {
            get: function (oData) {
               if (oData === null || !angular.isDefined(oData)) {
                  return $http.get(mybAuthenticationServiceConfig.config.wsVirtualPath + '/Login.aspx');
               }
               else {
                  return $http.get(mybAuthenticationServiceConfig.config.wsVirtualPath + '/Login.aspx?', { params: oData });
               }
            }
         }
      }])

      .factory('ws_Logout', ['$http', 'mybAuthenticationServiceConfig', function ($http, mybAuthenticationServiceConfig) {
         return {
            post: function () {
               return $http.post(mybAuthenticationServiceConfig.config.wsVirtualPath + '/Logout.aspx');
            }
         }
      }])

      .factory('ws_Register', ['$http', 'mybAuthenticationServiceConfig', function ($http, mybAuthenticationServiceConfig) {
         return {
            post: function (oData) {
               return $http.post(mybAuthenticationServiceConfig.config.wsVirtualPath + '/Register.aspx', oData);
            }
         }
      }])

      .factory('ws_VerifyUser', ['$http', 'mybAuthenticationServiceConfig', function ($http, mybAuthenticationServiceConfig) {
         return {
            get: function (oData) {
               return $http.get(mybAuthenticationServiceConfig.config.wsVirtualPath + '/VerifyUser.aspx?', { params: oData });
            }
         }
      }])

      .factory('authenticationService', ['$q', '$http', '$timeout', 'mybAuthenticationServiceConfig', 'ws_Login', 'ws_Logout', 'ws_Register', 'ws_VerifyUser',
         function ($q, $http, $timeout, mybAuthenticationServiceConfig, ws_Login, ws_Logout, ws_Register, ws_VerifyUser) {
            var _options = mybAuthenticationServiceConfig.config;
            var _user = null;

            if (_options.ws_Login === null)
               _options.ws_Login = ws_Login;

            if (_options.ws_Logout === null)
               _options.ws_Logout = ws_Logout;

            if (_options.ws_Register === null)
               _options.ws_Register = ws_Register;

            if (_options.ws_VerifyUser === null)
               _options.ws_VerifyUser = ws_VerifyUser;

            var isLoggedDeferred = null;

            var _isLogged = function (forceWebService) {            

               if (forceWebService === true) _user = null;

               if (isLoggedDeferred == null || isLoggedDeferred.promise.$$state.status != 0) {
                  isLoggedDeferred = $q.defer();
               }
               else {
                  return isLoggedDeferred.promise;
               }

                  
               // check and see if we have retrieved the 'user' data from the server. if we have, reuse it by immediately resolving
               if (_user !== null) {
                  isLoggedDeferred.resolve(_user);
                  return isLoggedDeferred.promise;
               }
               else if (_options.ws_Login !== null) {
                  _options.ws_Login.get().then(
                     //success
                     function (response) {
                        if (response.data.User.UserId !== null) {
                           _user = response.data.User;
                           if (_options.userService !== null) {
                              _options.userService.authenticate(_user);
                           }
                           isLoggedDeferred.resolve(_user);
                        } else {
                           _user = null;
                           if (_options.userService !== null) {
                              _options.userService.authenticate(_user);
                           }
                           isLoggedDeferred.resolve(_user);
                        }
                     },
                     //errors
                     function (error) {
                        _user = null;
                        if (_options.userService !== null) {
                           _options.userService.authenticate(_user);
                        }
                        isLoggedDeferred.reject(error);
                     }
                  );
               }
               else {
                  isLoggedDeferred.reject("ws_Login not satisfied.");
               }
               return isLoggedDeferred.promise;
            }

            return {
               user: function () {
                  return _user;
               },
               login: function (oData) {
                  var deferred = $q.defer();

                  if (oData === null || !angular.isDefined(oData)) {
                     deferred.reject("Parameters not supplied");
                  }
                  else if (_options.ws_Login !== null) {
                     _options.ws_Login.get(oData).then(
                        //success
                        function (response) {
                           _user = response.data.User;
                           if (_options.userService !== null) {
                              _options.userService.authenticate(_user);
                           }
                           deferred.resolve(_user);
                        },
                        //errors
                        function (error) {
                           _user = null;
                           if (_options.userService !== null) {
                              _options.userService.authenticate(_user);
                           }
                           deferred.reject(error);
                        }
                     );
                  }
                  else {
                     deferred.reject("ws_Login not satisfied.");
                  }
                  return deferred.promise;
               },
               fbLogin: function () {
                  FB.login(function (response) {
                     if (response.authResponse) {
                        console.log('Welcome!  Fetching your information.... ');
                        FB.api('/me', function (response) {
                           console.log(response);
                           console.log('Good to see you, ' + response.name + '.');
                        });
                     } else {
                        console.log('User cancelled login or did not fully authorize.');
                     }
                  });
               },
               logout: function () {
                  var deferred = $q.defer();

                  if (_options.ws_Logout !== null) {
                     _options.ws_Logout.post().then(
                        //success
                        function (response) {
                           _user = null;
                           if (_options.userService !== null) {
                              _options.userService.authenticate(_user);
                           }
                           deferred.resolve(response);
                        },
                        //errors
                        function (error) {
                           deferred.reject(error);
                        }
                     );
                  }
                  else {
                     deferred.reject("ws_Logout not satisfied.");
                  }
                  return deferred.promise;
               },
               isLogged: _isLogged,
               register: function (oData) {
                  var deferred = $q.defer();

                  if (oData === null || !angular.isDefined(oData)) {
                     deferred.reject("Parameters not supplied");
                  }
                  if (_options.ws_Register !== null) {
                     return ws_Register.post(oData);
                  }
                  else {
                     deferred.reject("ws_Register not satisfied.");
                  }
                  return deferred.promise;
               },
               verifyUser: function (oData) {
                  var deferred = $q.defer();

                  if (oData === null || !angular.isDefined(oData)) {
                     deferred.reject("Parameters not supplied");
                  }

                  if (_options.ws_VerifyUser !== null) {
                     return ws_VerifyUser.get(oData);
                  }
                  else {
                     deferred.reject("ws_VerifyUser not satisfied.");
                  }
                  return deferred.promise;
               }
            };
         }
      ]);
}));