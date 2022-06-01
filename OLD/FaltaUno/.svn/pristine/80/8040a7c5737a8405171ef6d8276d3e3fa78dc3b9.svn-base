(function (root, factory) {
   "use strict";

   /*global define*/
   if (typeof define === 'function' && define.amd) {
      define(['angular', 'SSEService'], factory);  // AMD
   } else if (typeof module === 'object' && module.exports) {
      module.exports = factory(require('angular'), require('SSEService')); // Node
   } else {
      factory(root.angular, root.swal);					// Browser
   }
}(this, function (angular) {
   "use strict";

   angular.module('myb.SSEService', [])

      .provider('SSEServiceConfig', function () {
         var _servers =
            [{
               name: 'default',
               wsPath: '/SSE.aspx',
               reconnectTime: 10,
               callback: function (event) {
                  console.log(event.data);
                  //$scope.msg = JSON.parse(msg.data)
               }
            }];

         var _config = {
            servers: _servers,
            wsVirtualPath: ''
         };


         return {
            $get: function () {
               return {
                  config: _config,
                  servers: _servers
               };
            },
            config: _config,
            servers: _servers
         };
      })

      // 2. set a constant
      .constant('MODULE_VERSION', '1.0.0')

      .factory('SSEService', ['$q', '$http', '$timeout', 'SSEServiceConfig',
         function ($q, $http, $timeout, SSEServiceConfig) {
            var _options = SSEServiceConfig.config;

            var serverEventSources = [];

            var _start = function () {
               for (var i = 0; i < _options.servers.length; i++) {

                  if (!(serverEventSources[i] && serverEventSources[i].name == _options.servers[i].name)) {
                     serverEventSources[i] = {
                        name: _options.servers[i].name,
                        source: new EventSource(_options.wsVirtualPath + _options.servers[i].wsPath)
                     };
                     var _callback = _options.servers[i].callback;
                     var _reconnectTime = _options.servers[i].reconnectTime;
                     serverEventSources[i].source.onopen = function (e) {
                        // Reset reconnect frequency upon successful connection
                        reconnectFrequencySeconds = _reconnectTime;
                     };
                     serverEventSources[i].source.onmessage = function (event) {
                        _callback(JSON.parse(event.data), event);
                     };
                  }
               }
            };


            return {
               start: _start,
               config: SSEServiceConfig.config 

            };
         }
      ]);
}));