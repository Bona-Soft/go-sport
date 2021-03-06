(function (root, factory) {
   "use strict";

   /*global define*/
   if (typeof define === 'function' && define.amd) {
      define(['angular', 'requestStateService'], factory);  // AMD
   } else if (typeof module === 'object' && module.exports) {
      module.exports = factory(require('angular'), require('requestStateService')); // Node
   } else {
      factory(root.angular, root.swal);					// Browser
   }
}(this, function (angular, swal) {
   "use strict";

   angular.module('myb.ngRequestStateService', [])

      // 2. set a constant
      .constant('MODULE_VERSION', '1.0.0')

      .factory('requestStateService', ['$rootScope', '$filter', 'alertService', 'ws_MatchPlayerRequests', 
         function ($rootScope, $filter, alertService, ws_MatchPlayerRequests) {

            var _requestStateMenuBnts = [
               {
                  id: 0,
                  value: "not_sended",
                  description: "Solicitud no enviada",
                  icon: null,
                  type: 'I'
               },
               {
                  id: 1,
                  value: "pending",
                  description: "Reenviar solicitud",
                  icon: "fa fa-trash",
                  type: 'I'
               },
               {
                  id: 2,
                  value: "tentative_not",
                  description: "No creo que pueda",
                  icon: "fa fa-trash",
                  type: 'T'
               },
               {
                  id: 3,
                  value: "tentative",
                  description: "No se todavía",
                  icon: "fa fa-trash",
                  type: 'T'
               },
               {
                  id: 4,
                  value: "tentative_yes",
                  description: "Creo que puedo",
                  icon: "fa fa-trash",
                  type: 'T'
               },
               {
                  id: 5,
                  value: "proposal",
                  description: "Proponer...",
                  icon: "fa fa-trash",
                  type: 'T'
               },
               {
                  id: 6,
                  value: "confirmed",
                  description: "Confirmar",
                  icon: "fa fa-trash",
                  type: 'P'
               },
               {
                  id: 8,
                  value: "rejected",
                  description: "Rechazar",
                  icon: "fa fa-trash",
                  type: 'N'
               },
               {
                  id: 9,
                  value: "cancelled",
                  description: "Cancelar",
                  icon: "fa fa-trash",
                  type: 'N'
               },
               {
                  id: 12,
                  value: "confirmed_substitute",
                  description: "Confirmar como suplente",
                  icon: "fa fa-trash",
                  type: 'P'
               },
            ];


            var _positiveStates = function (mpr, match) {
               if (mpr.MatchPlayerRequestState 
                     && mpr.MatchPlayerRequestState.Type != 'P'
                     && mpr.MatchPlayerRequestState.Type != 'C'
                     && mpr.MatchPlayerRequestState.Value != 'not_sended') {

                  if (mpr.MatchPlayerRequestState.Type != 'N') {

                     if (match.PlayersRequest
                        && match.MaxPlayers > $filter('filter')(match.PlayersRequest, { MatchPlayerRequestState: { Value: "confirmed" } }, true).length) {
                        return $filter('filter')(_requestStateMenuBnts, { value: "confirmed" }, true);
                     }
                     else {
                        return $filter('filter')(_requestStateMenuBnts, { value: "confirmed_substitute" }, true);
                     }
                  }
                  else {
                     return $filter('filter')(_requestStateMenuBnts, { value: "pending" });
                  }

               }
               return [];
            };

            var _tentativeStates = function (mpr) {
               if (mpr.MatchPlayerRequestState 
                  && mpr.MatchPlayerRequestState.Type != 'N'
                  && mpr.MatchPlayerRequestState.Type != 'C'
                  && mpr.MatchPlayerRequestState.Value != 'not_sended') {

                  return $filter('filter')(_requestStateMenuBnts, { type: 'T' });

               }
               return [];
            };

            var _negativeStates = function (mpr) {
               if (mpr.MatchPlayerRequestState 
                  && mpr.MatchPlayerRequestState.Type != 'N'
                  && mpr.MatchPlayerRequestState.Type != 'C'
                  && mpr.MatchPlayerRequestState.Value != 'not_sended') {

                  if (mpr.PlayerSender.PlayerID == $rootScope.getCurrentPlayer().PlayerID) {
                     return $filter('filter')(_requestStateMenuBnts, { value: "cancelled" });
                  }
                  else {
                     return $filter('filter')(_requestStateMenuBnts, { value: "rejected" });
                  }
               }
               return [];
            };

            var _sendMatchPlayerRequest = function (matchPlayerRequest, match, callback) {
               return ws_MatchPlayerRequests.post(matchPlayerRequest).then(
                  function (response) {
                     alertService.clear();

                     var obj = {
                        MatchID: match.MatchID
                     }

                     return ws_MatchPlayerRequests.get(obj);
                  },
                  function (error) {
                     return error;
                  }
               );
            };

            return {
               positiveStates: _positiveStates,
               tentativeStates: _tentativeStates,
               negativeStates: _negativeStates,
               sendMatchPlayerRequest: _sendMatchPlayerRequest
            };
         }
      ]);
}));