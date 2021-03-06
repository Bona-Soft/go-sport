app.controller('MatchPlayersEdit',
   ['$mdDialog', '$scope', '$rootScope', '$state', '$stateParams', '$controller', '$filter',
      'errorService', 'alertService', 'userService', 'requestStateService',
      'ws_PlayerFrecuently', 'ws_PlayerRecommended', 'ws_Match', 'ws_MatchPlayerRequests',
      'Match',
   function ($mdDialog, $scope, $rootScope, $state, $stateParams, $controller, $filter,
      errorService, alertService, userService, requestStateService,
      ws_PlayerFrecuently, ws_PlayerRecommended, ws_Match, ws_MatchPlayerRequests,
      Match)
   {
      $controller('BaseController', { $scope: $scope });

      $scope.requestStateService = requestStateService;

      if (Match == null || Match.MatchID == 0) {
         $state.go("Home");
         return;
      }

      var _init = function () {
         $scope.base.isLoading = 1;

         $scope.slickConfig = {
            enabled: true,
            dots: false,
            autoplay: false,
            draggable: true,
            slidesToShow: 5,
            slidesToScroll: 5,
            infinite: false,
            //centerMode: false,
            //focusOnSelect: true,
            method: {},
            event: {
               beforeChange: function (event, slick, currentSlide, nextSlide) {
               },
               afterChange: function (event, slick, currentSlide, nextSlide) {
               }
            },
            responsive: [
               {
                  breakpoint: 1072,
                  settings: {
                     slidesToShow: 4,
                     slidesToScroll: 4
                  }
               },
               {
                  breakpoint: 930,
                  settings: {
                     slidesToShow: 3,
                     slidesToScroll: 3
                  }
               },
               {
                  breakpoint: 768,
                  settings: {
                     slidesToShow: 5,
                     slidesToScroll: 5
                  }
               },
               {
                  breakpoint: 675,
                  settings: {
                     slidesToShow: 4,
                     slidesToScroll: 4
                  }
               },
               {
                  breakpoint: 570,
                  settings: {
                     slidesToShow: 3,
                     slidesToScroll: 3
                  }
               },
               {
                  breakpoint: 420,
                  settings: {
                     slidesToShow: 2,
                     slidesToScroll: 2
                  }
               }
            ]
         };
         $scope.matchPlayerRequestSearchText = "";

         async.parallel([
            _getFrecuently,
            _getRecommended,
            _loadMatch
         ],
            function (err, results) {
               $scope.base.isLoading = 0;
            });

      };

      var _getFrecuently = function (callback) {
         return ws_PlayerFrecuently.get().then(
            function (response) {
               $scope.data.frecuentlyPlayers = response.data;
               callback();
            },
            function (error) {
               alertService.clear();
               if (!errorService.controlExc(error)) {
                  alertService.addHttpAlerts(error, $scope);
               }
               callback(error);
            });
      };

      var _getRecommended = function (callback) {
         var data = {
            MatchID: Match.MatchID
         };
         return ws_PlayerRecommended.get(data).then(
            function (response) {
               $scope.data.recommendedPlayers = response.data;
               callback();
            },
            function (error) {
               alertService.clear();
               if (!errorService.controlExc(error)) {
                  alertService.addHttpAlerts(error, $scope);
               }
               callback(error);
            });
      };

      var _loadMatch = function (callback) {
         var data = {
            MatchID: Match.MatchID,
            ViewType: 3
         };

         return ws_Match.get(data).then(function (response) {
            $scope.data.Match = response.data;
            callback();
         }, function (error) {
            alertService.clear();
            if (!errorService.controlExc(error)) {
               alertService.addHttpAlerts(error, $scope);
            }
            callback(error);
         });
      };

      $scope.sendAllRequest = function () {

         if ($scope.base.isProcessing > 0) {
            return;
         }
         $scope.base.isProcessing = 1;

         var data = { object: $scope.data.Match.PlayersRequest };

         ws_MatchPlayerRequests.post(data).then(
            function (response) {
               alertService.clear();
               $scope.data.Match.PlayersRequest = response.data;

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

      $scope.existsPlayerRequest = function (player) {
         return player && ($filter('filter')($scope.data.Match.PlayersRequest, { PlayerReceiver: { PlayerID: player.PlayerID } })).length > 0;  
      };

      $scope.notExistsPlayerRequest = function (player) {
         return !$scope.existsPlayerRequest(player);
      };

      $scope.addRequest = function (player) {
         if (!player)
            return;

         if ($scope.base.isProcessing == 1)
            return;

         if ($scope.existsPlayerRequest(player)) {
            return;
         };

         $scope.base.isProcessing = 1;

         var obj = {
            Match: {
               MatchID: $scope.data.Match.MatchID
            },
            PlayerSender: $rootScope.getCurrentPlayer(),
            PlayerReceiver: player,
            Comment: {
               CommentID: -1,
               User: userService.user(),
               Text: "Todavia no enviaste la solicitud a este jugador."
            },
            MatchPlayerRequestState: {
               MatchPlayerRequestStateID: 0,
               Value: "not_sended",
               Description: "Solicitud no enviada"
            },
            RecieveDate: null,
            LastStateChangeDate: new Date()
         };

         $scope.data.Match.PlayersRequest.push(obj);
         $scope.playerSelected = null;

         $scope.base.isProcessing = 0;
      };

      $scope.showCommentText = function (item) {
         return item.Comment != null && item.Comment.Text != null && item.Comment.Text.length > 0;
      };

      _init();

      var originatorEv;

      
      //var _requestStateMenuBnts = [
      //   {
      //      id: 0,
      //      value: "not_sended",
      //      description: "Solicitud no enviada",
      //      icon: null,
      //      type: 'I'
      //   },
      //   {
      //      id: 1,
      //      value: "pending",
      //      description: "Reenviar solicitud",
      //      icon: "fa fa-trash",
      //      type: 'I'
      //   },
      //   {
      //      id: 2,
      //      value: "tentative_not",
      //      description: "No creo que pueda",
      //      icon: "fa fa-trash",
      //      type: 'T'
      //   },
      //   {
      //      id: 3,
      //      value: "tentative",
      //      description: "No se todavía",
      //      icon: "fa fa-trash",
      //      type: 'T'
      //   },
      //   {
      //      id: 4,
      //      value: "tentative_yes",
      //      description: "Creo que puedo",
      //      icon: "fa fa-trash",
      //      type: 'T'
      //   },
      //   {
      //      id: 5,
      //      value: "proposal",
      //      description: "Proponer...",
      //      icon: "fa fa-trash",
      //      type: 'T'
      //   },
      //   {
      //      id: 6,
      //      value: "confirmed",
      //      description: "Confirmar",
      //      icon: "fa fa-trash",
      //      type: 'P'
      //   },
      //   {
      //      id: 8,
      //      value: "rejected",
      //      description: "Rechazar",
      //      icon: "fa fa-trash",
      //      type: 'N'
      //   },
      //   {
      //      id: 9,
      //      value: "cancelled",
      //      description: "Cancelar",
      //      icon: "fa fa-trash",
      //      type: 'N'
      //   },
      //   {
      //      id: 12,
      //      value: "confirmed_substitute",
      //      description: "Confirmar como suplente",
      //      icon: "fa fa-trash",
      //      type: 'P'
      //   },
      //];


      //$scope.positiveStates = function (mpr) {
      //   if (mpr.MatchPlayerRequestState.Type != 'P'
      //      && mpr.MatchPlayerRequestState.Type != 'C'
      //      && mpr.MatchPlayerRequestState.Value != 'not_sended') {
      //      if (mpr.MatchPlayerRequestState.Type != 'N') {

      //         if ($scope.data.Match.MaxPlayers > $filter('filter')($scope.data.Match.PlayersRequest, { MatchPlayerRequestState: { Value: "confirmed" } }, true).length) {
      //            return $filter('filter')(_requestStateMenuBnts, { value: "confirmed" }, true);
      //         }
      //         else {
      //            return $filter('filter')(_requestStateMenuBnts, { value: "confirmed_substitute" }, true);
      //         }
      //      }
      //      else {
      //         return $filter('filter')(_requestStateMenuBnts, { value: "pending" });
      //      }

      //   }
      //   return [];   
      //};

      //$scope.tentativeStates = function (mpr) {
      //   if (mpr.MatchPlayerRequestState.Type != 'N'
      //      && mpr.MatchPlayerRequestState.Type != 'C'
      //      && mpr.MatchPlayerRequestState.Value != 'not_sended') {

      //      return $filter('filter')(_requestStateMenuBnts, {type: 'T'});
                    
      //   }
      //   return [];
      //};

      //$scope.negativeStates = function (mpr) {
      //   if (mpr.MatchPlayerRequestState.Type != 'N'
      //      && mpr.MatchPlayerRequestState.Type != 'C'
      //      && mpr.MatchPlayerRequestState.Value != 'not_sended') {

      //      if (mpr.PlayerSender.PlayerID == $rootScope.getCurrentPlayer().PlayerID) {
      //         return $filter('filter')(_requestStateMenuBnts, { value: "cancelled" });
      //      }
      //      else {
      //         return $filter('filter')(_requestStateMenuBnts, { value: "rejected" });
      //      }
      //   }
      //   return [];
      //};

      var _recfilterListPlayersRequest = function (mpr, arrText) {
         if (!arrText) {
            return false;
         }
         var _result = false;

         if (mpr.PlayerReceiver && mpr.PlayerReceiver.User && mpr.PlayerReceiver.User.Name)
            _result = mpr.PlayerReceiver.User.Name.toLowerCase().indexOf(arrText[0].toLowerCase()) !== -1;

         if (!_result
               && mpr.PlayerReceiver
            && mpr.PlayerReceiver.User
            && mpr.PlayerReceiver.User.LastName) {
            _result = mpr.PlayerReceiver.User.LastName.toLowerCase().indexOf(arrText[0].toLowerCase()) !== -1;
         } 
         
         if (_result && arrText[1]) {
            arrText.splice(0, 1);
            return _recfilterListPlayersRequest(mpr, arrText);
         }
         return _result; 
      };
      var _filterListPlayersRequest = function (mpr) {
         if ($scope.matchPlayerRequestSearchText && $scope.matchPlayerRequestSearchText.trim() != "") {
            var _searchText = $scope.matchPlayerRequestSearchText.split(" ");
            return _recfilterListPlayersRequest(mpr,_searchText);
         }
         return true;
      };

      $scope.listPlayersRequest = function () {
        
         if ($scope.data != null && $scope.data.Match != null)
            return $filter('filter')($scope.data.Match.PlayersRequest, _filterListPlayersRequest);
         return [];
      };

      $scope.openMenu = function ($mdMenu, ev) {
         originatorEv = ev;
         $mdMenu.open(ev);
      };

      //TODO: ver si se puede unificar en una funcion
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

         item.MatchPlayerRequestState.RequestStateID = option.id;

         requestStateService.sendMatchPlayerRequest(item, $scope.data.Match).then(
            function (resp) {
               alertService.clear();
               $scope.data.Match.PlayersRequest = resp.data;
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

      
      $scope.searchPlayers = function (playerSearchText) {
         return $rootScope.inputMethods.searchPlayers(playerSearchText).then(function (playerList) {
            return $filter('filter')(playerList, $scope.notExistsPlayerRequest);
         },
         function (error) {
            return [];
         });         
      };

      //var _sendRequest = function (matchPlayerRequest) {

      //   if ($scope.base.isProcessing > 0) {
      //      return;
      //   }
      //   $scope.base.isProcessing = 1;

      //   ws_MatchPlayerRequests.post(matchPlayerRequest).then(
      //      function (response) {
      //         alertService.clear();

      //         //for (var i = 0; i < $scope.data.Match.PlayersRequest.length; i++) {
      //         //   if ($scope.data.Match.PlayersRequest[i].MatchPlayerRequestID == response.data.MatchPlayerRequestID) {
      //         //      $scope.data.Match.PlayersRequest[i] = response.data;
      //         //      break;
      //         //   }
      //         //}

      //         var obj = {
      //            MatchID: $scope.data.Match.MatchID
      //         }

      //         ws_MatchPlayerRequests.get(obj).then(
      //            function (resp) {
      //               alertService.clear();
      //               $scope.data.Match.PlayersRequest = resp.data;
      //               $scope.base.isProcessing = 0;
      //         }, function (error) {
      //            alertService.clear();
      //            if (!errorService.controlExc(error)) {
      //               alertService.addHttpAlerts(error, $scope);
      //            }
      //            $scope.base.isProcessing = 0;
      //         });

               
      //      },
      //      function (error) {
      //         alertService.clear();
      //         if (!errorService.controlExc(error)) {
      //            alertService.addHttpAlerts(error, $scope);
      //         }
      //         $scope.base.isProcessing = 0;
      //      }
      //   );
      //};

   }
]);