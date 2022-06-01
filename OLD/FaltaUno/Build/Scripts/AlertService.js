(function (root, factory) {
   "use strict";

   /*global define*/
   if (typeof define === 'function' && define.amd) {
      define(['angular', 'alertService'], factory);  // AMD
   } else if (typeof module === 'object' && module.exports) {
      module.exports = factory(require('angular'), require('alertService')); // Node
   } else {
      factory(root.angular);					// Browser
   }
}(this, function (angular) {
   "use strict";

   angular
      .module('myb.AlertService', [])

      .constant('MODULE_VERSION', '1.0.0')

      .provider('alertServiceConfig', function () {
         var _config = {};

         return {
            setDefaults: function (options) {
               // order of precedence: element options, theme, defaults.
               _config = angular.extend(_config, options);
            },
            $get: function () {
               return {
                  config: _config
               };
            }
         };
      })

      .factory('alertService', ['$rootScope', 'alertServiceConfig', function ($rootScope, alertServiceConfig) {
         //$rootScope.alerts = [];
         var _scope = null;

         function _setScope(scope) {
            if (scope) {
               _scope = scope;
            }
            else {
               _scope = $rootScope;
            }
            if (!_scope.alerts) {
               _scope.alerts = [];
            }
         }

         function getAlertType(HttpResponse) {
            if (HttpResponse) {
               if (HttpResponse.status) {
                  switch (HttpResponse.status.toString().substring(0, 1)) {
                     case '1': //Informational
                        return "info";
                     case '2': //Success
                        return "success";
                     case '3': //Redirection
                        return "info";
                     case '4': //Client Error
                        return "warning";
                     case '5': //Server Error
                        return "danger";
                     default:
                        return "danger";
                  }
               }
            }
         }

         function addHttpAlerts(HttpResponse, scope) {
            var type = getAlertType(HttpResponse);
            var arrMsg = HttpResponse.data;
            _setScope(scope);

            if (arrMsg.length > 0) {
               for (var i = 0; i < arrMsg.length; i++) {
                  var obj = {
                     'type': type,
                     'msg': arrMsg[i].Message,
                     close: function (alert) {
                        closeAlert(alert);
                     }
                  }
                  _scope.alerts.push(obj);
               }
            }
         }

         function add(type, msg, scope) {
            _setScope(scope);

            var obj = {
               'type': type,
               'msg': msg,
               close: function (alert) {
                  alertService.closeAlert(alert);
               }
            }
            _scope.alerts.push(obj);
         }

         function closeAlert(alert) {
            if (_scope)
               closeAlertIdx(_scope.alerts.indexOf(alert));
         }

         function closeAlertIdx(index) {
            if (_scope)
               _scope.alerts.splice(index, 1);
         }

         //Note: call this function at the begining of the controller or if you use states put it on "$stateChangeSuccess" event.
         function clear() {
            if (_scope)
               _scope.alerts.length = 0;
         }

         return {
            add: add,
            addHttpAlerts: addHttpAlerts,
            closeAlertIdx: closeAlertIdx,
            closeAlertIdx: closeAlertIdx,
            clear: clear
         }
      }]);

   //TODO: hacer directiva clear alert service
}));