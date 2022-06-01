(function (root, factory) {
   "use strict";

   /*global define*/
   if (typeof define === 'function' && define.amd) {
      define(['angular', 'errorService'], factory);  // AMD module
   } else if (typeof module === 'object' && module.exports) {
      module.exports = factory(require('angular'), require('errorService'));  // CommonJS module / Node
   } else {
      factory(root.angular, root.errSer);	 // Browser global
   }
}(this, function (angular, errSer) {
   "use strict";

   angular.module('myb.ngErrorService', [])
      .info({ version: '1.0.0' })
      // 2. set a constant
      .constant('MODULE_VERSION', '1.0.0')

      .provider('mybErrorServiceConfig', function () {
         var _config = {
            showServerErrors: false, //for debug purposes
            showClientErrors: false, //for debug purposes
            defaultServerMessage: 'Hubo un error interno servidor',
            defaultClientMessage: 'Hubo un error interno cliente',
            defaultStatusCode: '',
            defaultMessage: 'No se puede acceder a la pagina'
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

      .directive('mybErrorMessage', function () {
         return {
            controller: 'ErrorServiceMessageCtrl',
            controllerAs: 'es',
            transclude: true,
            bindToController: true,
            template: '{{message}}'
         };
      })

      .directive('mybErrorStatusCode', function () {
         return {
            controller: 'ErrorServiceStatusCodeCtrl',
            controllerAs: 'el',
            transclude: true,
            bindToController: true,
            template: '{{statusCode}}'
         };
      })

      // "user" is a service that tracks the user's identity.
      // calling identity() returns a promise while it does what you need it to do
      // to look up the signed-in user's identity info. for example, it could make an
      // HTTP request to a rest endpoint which returns the user's name, roles, etc.
      // after validating an auth token in a cookie. it will only do this identity lookup
      // once, when the application first runs. you can force re-request it by calling identity(true)
      .factory('errorService', ['$http', '$location', '$timeout', 'mybErrorServiceConfig',
         function ($http, $location, $timeout, mybErrorServiceConfig) {
            var _options = mybErrorServiceConfig.config;

            var _showServerErrors = _options.showServerErrors; //for debug purposes
            var _showClientErrors = _options.showClientErrors; //for debug purposes
            var _defaultServerMessage = _options.defaultServerMessage;
            var _defaultClientMessage = _options.defaultClientMessage;
            var _defaultMessage = _options.defaultMessage;

            var _defaultStatusCode = _options.defaultStatusCode;

            var _message = '';
            var _statusCode = '';
            var _hasError = false;

            function _sendError(message, statusCode) {
               _hasError = true;
               _message = message;
               _statusCode = statusCode;
               _goError(_message, _statusCode);
            }

            function _goError(message, statusCode) {
               $location.path("/Error");
            }

            function showServerErrors() {
               return
            }

            return {
               showServerErrors: function () {
                  return _showServerErrors;
               },
               showClientErrors: function () {
                  return _showClientErrors;
               },
               defaultStatusCode: function () {
                  return _defaultStatusCode;
               },
               defaultMessage: function () {
                  if (_statusCode >= 400 && _statusCode < 500) {
                     return _defaultClientMessage;
                  } else if (_statusCode >= 500 && _statusCode < 600) {
                     return _defaultServerMessage;
                  }
                  return _defaultMessage;
               },
               controlExc: function (response) {
                  if ((response.status) && (response.status == 520)) {
                     return false;
                  }
                  if (response.status >= 500) {
                     if (_showServerErrors)
                        _sendError(response.data, response.status)
                     else
                        _sendError(_defaultServerMessage, response.status);
                  }
                  else if (response.status >= 400) {
                     if (_showClientErrors)
                        _sendError(response.data, response.status)
                     else
                        _sendError(_defaultClientMessage, response.status);
                  } else {
                     return false;
                  }
                  return true;
               },
               sendError: function (message, statusCode) {
                  _sendError(message, statusCode);
               },
               message: function () {
                  return _message;
               },
               statusCode: function () {
                  return _statusCode;
               },
               hasError: function () {
                  return _hasError;
               },
               resetMessage: function () {
                  _message = '';
               },
               resetStatusCode: function () {
                  _statusCode = '';
               },
               resetError: function () {
                  _message = '';
                  _statusCode = '';
                  _hasError: false;
               }
            };
         }
      ])

      .controller('ErrorServiceMessageCtrl', ['$scope', 'errorService', function ($scope, errorService) {
         $scope.init = function () {
            $scope.message = '';

            if (errorService.message() == '')
               $scope.message = angular.copy(errorService.defaultMessage(), $scope.message);
            else
               $scope.message = angular.copy(errorService.message(), $scope.message);
            if (errorService.message() == '' && errorService.statusCode() == '')
               errorService.resetError()
            else
               errorService.resetMessage();
         }

         $scope.init();
      }])

      .controller('ErrorServiceStatusCodeCtrl', ['$scope', 'errorService', function ($scope, errorService) {
         $scope.init = function () {
            $scope.statusCode = '';

            $scope.statusCode = angular.copy(errorService.statusCode(), $scope.statusCode);
            if (errorService.message() == '' && errorService.statusCode() == '')
               errorService.resetError()
            else
               errorService.resetStatusCode();
         }

         $scope.init();
      }]);
}));