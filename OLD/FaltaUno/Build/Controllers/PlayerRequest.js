app.controller('PlayerRequest', ["$scope", "$controller", "$filter", "$state",
   "alertService", "errorService", "requestStateService",
   "ws_UserMatchPlayerRequests", "ws_Match",
   function ($scope, $controller, $filter, $state, alertService, errorService, requestStateService, ws_UserMatchPlayerRequests, ws_Match) {
      $controller('BaseController', { $scope: $scope });

      $scope.requestStateService = requestStateService;

      var _init = function () {

         _getMatchPlayerRequests();        
      };

      var _getMatchPlayerRequests = function () {

         ws_UserMatchPlayerRequests.get().then(
            function (response) {
               $scope.myMatchRequest = response.data;
               $scope.base.isLoading = 0;
            },
            function (error) {
               alertService.clear();
               if (!errorService.controlExc(error)) {
                  alertService.addHttpAlerts(error, $scope);
               }
               $scope.base.isLoading = 0;
            });
      };

      //REQUEST STATE BUTTOMS
      {
         $scope.openMenu = function ($mdMenu, ev) {
            originatorEv = ev;
            $mdMenu.open(ev);
         };

         $scope.positiveClick = function (option, item) {
            _stateClick(option, item);
         };

         $scope.tentativeClick = function (option, item) {
            _stateClick(option, item);

         };

         $scope.negativeClick = function (option, item) {
            /*
            $mdDialog.show(
               $mdDialog.alert()
                  .title('You clicked!')
                  .textContent('You clicked the menu item at index ' + index)
                  .ok('Nice')
                  .targetEvent(originatorEv)
            );
            originatorEv = null;*/

            _stateClick(option, item);
         };

         var _stateClick = function (option, item) {
            if ($scope.base.isProcessing > 0) {
               return;
            }
            $scope.base.isProcessing = 1;

            if (item.MatchPlayerRequestState) {
               item.MatchPlayerRequestState.RequestStateID = option.id;
            } else {
               item.MatchPlayerRequestState = {
                  RequestStateID: option.id
               };
            }
            
            requestStateService.sendMatchPlayerRequest(item, item.Match).then(
               function (resp) {
                  alertService.clear();
                  var arr = $filter('filter')(resp.data, { MatchPlayerRequestID: item.MatchPlayerRequestID });
                  if (arr.length > 0) {
                     var pr = item.Match.PlayersRequest;
                     angular.copy(arr[0], item);
                     var list = $filter('filter')(pr, { MatchPlayerRequestID: item.MatchPlayerRequestID });
                     if (list.length > 0) {
                        angular.copy(item, list[0]);
                     }
                     item.Match.PlayersRequest = pr;
                  }
                     
                  $scope.base.isProcessing = 0;
               },
               function (error) {
                  alertService.clear();
                  if (!errorService.controlExc(error)) {
                     alertService.addHttpAlerts(error, $scope);
                  }
                  $scope.base.isProcessing = 0;
               });
         };
      }

      //REQUEST SEARCH
      {
         var _recfilterListPlayersRequest = function (mpr, arrText) {
            if (!arrText) {
               return false;
            }
            var _result = false;

            if (mpr.PlayerSender && mpr.PlayerSender.User && mpr.PlayerSender.User.Name)
               _result = mpr.PlayerSender.User.Name.toLowerCase().indexOf(arrText[0].toLowerCase()) !== -1;

            if (!_result
               && mpr.PlayerSender
               && mpr.PlayerSender.User
               && mpr.PlayerSender.User.LastName) {
               _result = mpr.PlayerSender.User.LastName.toLowerCase().indexOf(arrText[0].toLowerCase()) !== -1;
            }

            if (_result && arrText[1]) {
               arrText.splice(0, 1);
               return _recfilterListPlayersRequest(mpr, arrText);
            }


            
            return _result;
         };

         var _filterListPlayersRequestByMatchName = function (mpr, arrText) {
            if (!arrText) {
               return false;
            }
            
            for (var i = 0; i < arrText.length; i++) {
               var _result = mpr.Match.Name.toLowerCase().indexOf(arrText[i].toLowerCase());
               if (_result < 0)
                  return false;
            }
            return true;
            
         }

         var _filterListPlayersRequest = function (mpr) {
            if ($scope.matchPlayerRequestSearchText && $scope.matchPlayerRequestSearchText.trim() != "") {
               var _searchText = $scope.matchPlayerRequestSearchText.split(" ");
               var _found = _recfilterListPlayersRequest(mpr, _searchText);
               if (!_found) {
                 return  _filterListPlayersRequestByMatchName(mpr, _searchText);
               }
               return _found;
            }
            return true;
         };

         $scope.listPlayersRequest = function () {

            if ($scope.myMatchRequest)
               return $filter('filter')($scope.myMatchRequest, _filterListPlayersRequest);
            return [];
         };
      }

      $scope.viewMatch = function (match) {
         $scope.selectedMatch = match;
      };

      $scope.goMatchView = function (match) {
         $state.go("Match/View", { MatchID: match.MatchID });
      };

      $scope.goPlayerView = function (player) {
         $state.go("Player/View", { PlayerID: player.PlayerID });
      };

      $scope.existsMatchSubstitutes = function () {
         if ($scope.selectedMatch) {
            var subtitutes = $filter('filter')($scope.selectedMatch.PlayersRequest, { MatchPlayerRequestState: { Value: "confirmed_substitute" } }, true);
            return subtitutes && subtitutes.length > 0;
         }
         return false;
      };

      _init();
   }
]);