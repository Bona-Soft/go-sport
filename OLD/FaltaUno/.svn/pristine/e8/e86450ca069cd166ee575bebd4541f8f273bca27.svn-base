/****************************
Global Angular Helpers Services
*****************************/

(function (root, factory) {
   "use strict";

   /*global define*/
   if (typeof define === 'function' && define.amd) {
      define(['angular', 'ngHelpers'], factory);  // AMD
   } else if (typeof module === 'object' && module.exports) {
      module.exports = factory(require('angular'), require('ngHelpers')); // Node
   } else {
      factory(root.angular);					// Browser
   }
}(this, function (angular) {
   "use strict";
   angular.module('myb.ngHelpers', [])

      .constant('MODULE_VERSION', '1.0.0');

      /* TODO: bajar Jasmine and Karma y averiguar mas del tema */

      /*
      .describe('httpMockRequest', function () {
         beforeEach(inject(function (_$controller_, _$httpBackend_) {
            $controller = _$controller_;
            $httpBackend = _$httpBackend_;
            $httpBackend.expectGET('/99db823a-1a1f-4892-acbb-2315f1ae6e93').respond(200, { data: 'expected response' });
         }))

         it('should set response variable', function () {
            var $scope = {};
            var controller = $controller('myCtrl', { $scope: $scope });
            $scope.getResponse();
            $httpBackend.flush();
            expect($scope.response).toEqual('expected response');
         });
      })

      .factory('mockService', ['$q', '$httpBackend', function ($q, $httpBackend) {

         var _fakeHttpCall = function (isSuccessful, successObj, failObj) {
            if (typeof (successObj) == "undefined")
               successObj = "Successfully resolved the fake $http call";
            if (typeof (failObj) == "undefined")
               failObj = "Oh no! Something went terribly wrong in you fake $http call";

            var deferred = $q.defer()

            if (isSuccessful === true) {
               deferred.resolve(successObj)
            }
            else {
               deferred.reject(failObj)
            }

            return deferred.promise
         }

      
         //var _isSuccessful = true;
         //var _respondObj = null;
         //var _successObj = "Successfully resolved the fake $http call";
         //var _failObj = "Oh no! Something went terribly wrong in you fake $http call";

         //$httpBackend.whenGET('/99db823a-1a1f-4892-acbb-2315f1ae6e93').respond(_respondObj);
         //$httpBackend.whenPOST('/99db823a-1a1f-4892-acbb-2315f1ae6e93').respond(function (method, url, data) {
         //   var phone = angular.fromJson(data);
         //   phones.push(phone);
         //   return [200, phone, {}];
         //});

         //$httpBackend.whenGET(/^\/templates\//).passThrough(); // Requests for templates are handled by the real server
        
         //var _get = function (isSuccessful, successObj, failObj) {

         //}
         return {
            fakeHttpCall: _fakeHttpCall
         }
      }])

      //TODO: Cambiar el settings.wsVirtualPath por una variable en el servicio
      .factory('ws_Mock', ['$http', 'settings', function ($http, settings) {
         return {
            post: function (oData) {
               return $http.post(settings.wsVirtualPath + '/99db823a-1a1f-4892-acbb-2315f1ae6e93', oData);
            },
            get: function (oData) {
               return $http.get(settings.wsVirtualPath + '/99db823a-1a1f-4892-acbb-2315f1ae6e93?', { params: oData });
            }
         }
      }]);
      */
}));
