app.controller('TeamRanking', ["$scope", "$controller", "alertService", "errorService",
   function ($scope, $controller, alertService, errorService) {
      $controller('BaseController', { $scope: $scope });
   }]);