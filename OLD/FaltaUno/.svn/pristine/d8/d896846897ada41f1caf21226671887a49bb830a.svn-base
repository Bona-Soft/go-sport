app.controller('Home', ['$scope', '$http', '$controller', '$uibModal',
   'settings', 'alertService',
   'ws_MatchSearch', 'ws_UserNews',
   function ($scope, $http, $controller, $uibModal,
      settings, alertService,
      ws_MatchSearch, ws_UserNews) {
      $controller('BaseController', { $scope: $scope });
      $scope.base.isLoading = 1;

      {
         $scope.infoSlickConfig = {
            enabled: true,
            autoplay: false,
            draggable: true,
            slidesToShow: 4,
            slidesToScroll: 4,
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
                     slidesToShow: 3,
                     slidesToScroll: 3
                  }
               },
               {
                  breakpoint: 810,
                  settings: {
                     slidesToShow: 2,
                     slidesToScroll: 2
                  }
               },
               {
                  breakpoint: 768,
                  settings: {
                     slidesToShow: 3,
                     slidesToScroll: 3
                  }
               },
               {
                  breakpoint: 600,
                  settings: {
                     slidesToShow: 2,
                     slidesToScroll: 2
                  }
               },
               {
                  breakpoint: 500,
                  settings: {
                     slidesToShow: 1,
                     slidesToScroll: 1
                  }
               }
               // You can unslick at a given breakpoint now by adding:
               // settings: "unslick"
               // instead of a settings object
            ]
         };

         $scope.getUserNews = function () {
            $scope.base.isProcessing = 1;
            ws_UserNews.get().then(function (response) {
               $scope.data.userNews = response.data;
               $scope.base.isProcessing = 0;
            },
               function (error) {
                  $scope.base.isProcessing = 0;
               });
         };
      }

      var _init = function () {
         $scope.getUserNews();

         async.parallel([
            _getAvailableMatches
         ],
            function (err, results) {
               $scope.base.isLoading = 0;
            });
      };

      var _getAvailableMatches = function (callback) {
         var _search = {};

         return ws_MatchSearch.get(_search).then(function (response) {
            if (callback)
               callback();
         }, function (error) {
            if (callback)
               callback(error);
         });
      };

      var _getTopPlayers = function () {
      };

      _init();
   }]);