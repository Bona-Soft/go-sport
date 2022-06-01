app.controller('MatchView', ["$rootScope", "$scope", "$controller", "$state", "$filter",
   "alertService", "errorService", "userService",
   "ws_Match", "ws_MatchPlayerRequests", "MatchID",
   function ($rootScope, $scope, $controller, $state, $filter,
      alertService, errorService, userService,
      ws_Match, ws_MatchPlayerRequests, MatchID) {
      $controller('BaseController', { $scope: $scope });

      $scope.init = function () {
         $scope.base.isLoading = 1;

         $scope.loadMatch();

      };

      $scope.loadMatch = function (callback) {
         $scope.match = null;

         $scope.data.filters = {
            MatchID: MatchID,
            ViewType: 3
         };

         ws_Match.get($scope.data.filters).then(
            function (response) {
               $scope.Match = response.data;
               $scope.auxMatchDateTime = moment(response.data.MatchDateTime).format("MMMM Do YYYY, h:mm:ss a");
               $scope.auxMatchDate = moment(response.data.MatchDateTime).format("MMMM Do YYYY");
               $scope.auxMatchTime = moment(response.data.MatchDateTime).format("h:mm:ss a");
               $scope.editEnabled = $scope.Match.MatchPlayerOwner.User.UserID == userService.user().UserID;
               $scope.base.isLoading = 0;
            }, function (error) {
               alertService.clear();
               errorService.controlExc(error);
               $scope.base.isLoading = 0;
            });
      };

      $scope.leaveMatchEnabled = function () {
         return $scope.Match != null
            && $scope.Match.PlayersRequest != null
            && $filter('filter')($scope.Match.PlayersRequest, function (item) {
               return item.PlayerReceiver.PlayerID == $rootScope.getCurrentPlayer().PlayerID
                  && item.MatchPlayerRequestState.Type == 'P';
            }).length > 0;

      };

      $scope.leaveMatch = function () {
         if ($scope.base.isProcessing > 0 || !$scope.leaveMatchEnabled()) {
            return;
         }
         var mpr = $filter('filter')($scope.Match.PlayersRequest, function (item) {
            return item.PlayerReceiver.PlayerID == $rootScope.getCurrentPlayer().PlayerID;
         });
         if (mpr.length <= 0) {
            return;
         }
         var data = angular.copy(mpr[0]);
         data.MatchPlayerRequestState.RequestStateID = 8;

         _sendPlayerRequest(data);

      };

      $scope.joinMatch = function () {
         if ($scope.base.isProcessing > 0 || $scope.leaveMatchEnabled()) {
            return;
         }
         var mpr = $filter('filter')($scope.Match.PlayersRequest, function (item) {
            return item.PlayerReceiver.PlayerID == $rootScope.getCurrentPlayer().PlayerID;
         });
         var data;

         if (mpr.length <= 0) {
            data = {
               Match: {
                  MatchID: $scope.Match.MatchID
               },
               PlayerReceiver: $rootScope.getCurrentPlayer(),
               Comment: {
                  CommentID: -1,
                  User: userService.user()
               },
               MatchPlayerRequestState: {
                  RequestStateID: 12,
                  Value: "approval_required"
               },
               RecieveDate: null,
               LastStateChangeDate: new Date()
            };
         }
         else {
            data = angular.copy(mpr[0]);
            data.MatchPlayerRequestState.RequestStateID = 12;
            if (_isPlayerOwner()) {
               data.MatchPlayerRequestState.RequestStateID = 6;
            }
         }

         _sendPlayerRequest(data);
      };

      var _sendPlayerRequest = function (data) {
         if ($scope.base.isProcessing > 0) {
            return;
         }
         $scope.base.isProcessing = 1;

         ws_MatchPlayerRequests.post(data).then(
            function (response) {
               alertService.clear();
               for (var i = 0; i < $scope.Match.PlayersRequest.length; i++) {
                  if ($scope.Match.PlayersRequest[i].MatchPlayerRequestID == data.MatchPlayerRequestID) {
                     $scope.Match.PlayersRequest.splice(i, 1);
                     break;
                  }
               }
               $scope.Match.PlayersRequest.push(response.data);

               $scope.base.isProcessing = 0;
            },
            function (error) {
               alertService.clear();
               if (!errorService.controlExc(error)) {
                  alertService.addHttpAlerts(error, $scope);
               }
               $scope.base.isProcessing = 0;
            }
         );
      };

      var _isPlayerOwner = function () {
         return $scope.Match != null
            && $scope.Match.MatchPlayerOwner != null
            && $scope.Match.MatchPlayerOwner.PlayerID == $rootScope.getCurrentPlayer().PlayerID;
      };

      $scope.editMatch = function () {
         $state.go("Match", { MatchID: $scope.Match.MatchID });
      };

      $scope.editMatchPlayerRequest = function () {
         $state.go("Match/New/Players", { Match: $scope.Match });
      };

      $scope.init();
   }]);