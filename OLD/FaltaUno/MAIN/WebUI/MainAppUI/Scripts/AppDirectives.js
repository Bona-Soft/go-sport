/****************************
App Directives
*****************************/
(function (root, factory) {
   "use strict";

   /*global define*/
   if (typeof define === 'function' && define.amd) {
      define(['angular', 'appDirectives'], factory);  // AMD
   } else if (typeof module === 'object' && module.exports) {
      module.exports = factory(require('angular'), require('appDirectives')); // Node
   } else {
      factory(root.angular);					// Browser
   }
}(this, function (angular) {
   "use strict";
   angular.module('myb.appDirectives', [])

      .directive("sportClass", ['$rootScope', function ($rootScope) {
         return {
            restrict: 'A',
            link: function (scope, element, attrs) {
               scope.$watch('settings.sportSelected.Value', function (newval, oldval, scope) {
                  element.removeClass(oldval);
                  element.addClass(newval);
               }, true);
            }
         }
      }]);

   
}));