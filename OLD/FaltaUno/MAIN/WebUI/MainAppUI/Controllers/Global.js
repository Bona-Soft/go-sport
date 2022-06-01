/* Setup App Main Controller */
app.controller('AppController', ['$scope', '$rootScope', '$http', '$location', '$state', '$document', '$uibModal', '$mdSidenav', '$filter','$timeout',
   'settings', 'userService', 'authenticationService', 'alertService', 
   function ($scope, $rootScope, $http, $location, $state, $document, $uibModal, $mdSidenav, $filter, $timeout,
      settings, userService, authenticationService, alertService) {
      $scope.$on('$viewContentLoaded', function () {
         //App.initComponents(); // init core components
         //Layout.init(); //  Init entire layout(header, footer, sidebar, etc) on page load if the partials included in server side instead of loading with ng-include directive
      });

      $scope.toggleLeft = buildToggler('left');
      $scope.toggleRight = buildToggler('right');

      function buildToggler(componentId) {
         return function () {
            $mdSidenav(componentId).toggle();
         };
      }

      $scope.searchPlayers = function (playerSearchText) {
         return $rootScope.inputMethods.searchPlayers(playerSearchText).then(function (playerList) {
            return $filter('filter')(playerList, $scope.notExistsPlayerRequest);
         },
            function (error) {
               return [];
            });
      };

      $scope.selectPlayer = function (player) {
         if (player != null) {
            var _player = angular.copy(player);
            $state.go("Player/View", { PlayerID: _player.PlayerID });
            
         }
      };

      $scope.getCurrentUserNotificationNumber = function () {
         if (userService.user())
            return userService.user().Base.News;
         return 0;
      };

      $scope.mainSlickConfig = {
         enabled: true,
         autoplay: true,
         autoplaySpeed: 4000,
         draggable: true,
         variableWidth: true,
         infinite: true,
         centerMode: true,
         slidesToShow: 1,
         slidesToScroll: 1,
         method: {},
         event: {
            beforeChange: function (event, slick, currentSlide, nextSlide) {
            },
            afterChange: function (event, slick, currentSlide, nextSlide) {
            },
            init: function (event, slick) {
               slick.slickGoTo(0); // slide to correct index when init
            }
         }
      };

      $scope.user = null;
      $scope.userService = userService;

      $scope.newsImages = [
         'Assets/img/' + settings.sportSelected.Value + '/futbol_news1.jpg',
         'Assets/img/' + settings.sportSelected.Value + '/futbol_news2.jpg',
         'Assets/img/' + settings.sportSelected.Value + '/futbol_news3.jpg',
         'Assets/img/' + settings.sportSelected.Value + '/futbol_news4.jpg',
         'Assets/img/' + settings.sportSelected.Value + '/futbol_news5.jpg'
      ]

      $scope.getNewsImages = function (index) {
         console.log(index);
         console.log($scope.newsImages[index]);
         return $scope.newsImages[index];
      }

      $scope.dameImagenes = function () {
         console.log($scope.newsImages);
      }

      $scope.init = function () {
      }

      $scope.noScroll = function () {
         return $rootScope.noScroll;
      }

      $scope.urlBack = function () {
         $location.path();
      }

      $scope.showSportsNames = function () {
         return document.documentElement.clientWidth < 768;
      }

      $scope.login = function () {
         $scope.openLogInModal();
      }

      $scope.openLogInModal = function () {
         var parentElem = angular.element($document[0].querySelector('.modal-parent')) ? angular.element($document[0].querySelector('.modal-parent')) : undefined;
         var modalInstance = $uibModal.open({
            animation: false,
            templateUrl: 'Templates/LoginModal.html',
            controller: 'loginCtrl',
            size: 'sm',
            backdrop: 'static',
            appendTo: parentElem,
            resolve: {
               deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                  return $ocLazyLoad.load({
                     name: 'HayEquipoApp',
                     insertBefore: '#ng_load_plugins_before', // load the above css files before a LINK element with this ID. Dynamic CSS files must be loaded between core and theme css files
                     files: [
                        'Controllers/LoginModal.js'
                     ]
                  });
               }]
            }
         });

         modalInstance.result.then(
            function (user) {
               //Succeded
               $scope.user = user;
               $state.go('Inicio');

            }, function () {
               //Dismissed
               alertService.clear();
            }
         );
      };

      $scope.Logout = function () {
         authenticationService.logout().then(
            function () {
               $state.go('Home');
            },
            function (error) {
               alertService.add('danger', error.data);
            }
         );
      };
      /*
         $scope.isLogged = function (callback) {
                authenticationService.isLogged().then(
                  function (result) {
                      $scope.user = result;
                      return callback();
                  }
              );
         }
      */
      $scope.goRegister = function () {
         if (userService.isAuthenticated()) {
            return;
         }
         $state.go('Register');
      };

      $scope.init();
   }]);

/***
Layout Partials.
By default the partials are loaded through AngularJS ng-include directive. In case they loaded in server side(e.g: PHP include function) then below partial
initialization can be disabled and Layout.init() should be called on page load complete as explained above.
***/

/* Setup Layout Part - Header */
app.controller('HeaderController', ['$scope', '$rootScope', '$http', '$window', '$state', 'alertService', 'ws_UserActiveSport', 'cookieService',
   function ($scope, $rootScope, $http, $window, $state, alertService, ws_UserActiveSport, cookieService) {
   $scope.$on('$includeContentLoaded', function () {
      //$rootScope.settings.Layout.initHeader(); // init header
   });

      $scope.init = function () {
         $rootScope.zoom = 1;
         $scope.sucursalesItems = null;
         $scope.usuario;
         $scope.sportAll = { SportID: '0', Name: 'Todos', Value: 'all' };

         $scope.zoomOptions = [
            { name: "200%", value: 2 },
            { name: "150%", value: 1.5 },
            { name: "100%", value: 1 },
            { name: "85%", value: .85 },
            { name: "70%", value: .7 },
            { name: "50%", value: .5 }
         ];

      };

   $scope.enviarPantalla = function () {
      var html = getPageHTML();

      $http.post('Base/WebServices/EnviarPantalla.aspx', html).then(
         //success
         function (response) {
            alertService.Alert(response, "La pantalla ha sido enviada a soporte");
         },
         //error
         function (error) {
            alertService.Alert(error);
         }
      );
   }

   $scope.print = function () {
      $window.print();
   }

   $scope.init();
}]);

/* Setup Layout Part - Sidebar */
app.controller('SidebarController', ['$scope', '$rootScope', '$http', 'filterFilter', '$sce', function ($scope, $rootScope, $http, $filterFilter, $sce) {
   /*$scope.$on('$includeContentLoaded', function () {
       Layout.initSidebar(); // init sidebar
   });

   $scope.init = function () {
       $scope.menuItems = null;

       $rootScope.$on('Sidebar_UpdateMenu', function (event, args) {
           $scope.updateMenu();
       });
   }

   $scope.updateMenu = function () {
       $http.get('Base/WebServices/MenuUsuario.aspx').then(
           //success
           function (response) {
               $scope.menuItems = response.data;
           },
           //error
           function (error) {
               //TODO
           }
       );
   }

   $scope.getURL = function (item) {
       if (item.ModuloEntrada == null) {
           return '';
       }

       if (parseFloat(item.BOSversion) >= 4) {
           if (item.ModuloEntrada.length >= '../v4/index.aspx#'.length) {
               if (item.ModuloEntrada.slice(0, '../v4/index.aspx#'.length) == '../v4/index.aspx#') {
                   return item.ModuloEntrada.replace('../v4/index.aspx#', '');
               }
           }

           if (!item.ModuloEntrada.startsWith("/")) {
               item.ModuloEntrada = "/" + item.ModuloEntrada;
           }

           return '#' + item.ModuloEntrada;
       }

       if (parseFloat(item.BOSversion) >= 3) {
           var sURL = '#/v3.html?URL=';
           if (item.WAEDllFilename != null && item.WAEDllFilename != '') {
               sURL += '/' + item.WAEDllFilename;
           }

           if (item.ModuloEntrada != null && item.ModuloEntrada != '') {
               if (item.ModuloEntrada.indexOf('http') >= 0) {
                   sURL += item.ModuloEntrada;
               }
               else {
                   sURL += '/' + item.ModuloEntrada;
               }
           }

           if (item.Id != null && item.Id != '') {
               sURL += '?Id=' + item.Id;
           }
           $sce.trustAsResourceUrl(sURL);
           return sURL + '%23' + Math.random();
       }

       //VERSION DE 0 A 2
       return item.ModuloEntrada;
   }

   $scope.goURL = function (item) {
       url = $scope.getURL(item);
       window.location.href = url;
   }

   $scope.init();
   $scope.updateMenu();
   */
}]);

/* Setup Layout Part - Quick Sidebar */
app.controller('QuickSidebarController', ['$scope', function ($scope) {
   $scope.$on('$includeContentLoaded', function () {
      setTimeout(function () {
         QuickSidebar.init(); // init quick sidebar
      }, 2000)
   });
}]);

/* Setup Layout Part - Theme Panel */
app.controller('ThemePanelController', ['$scope', function ($scope) {
   $scope.$on('$includeContentLoaded', function () {
      Demo.init(); // init theme panel
   });
}]);

/* Setup Layout Part - Footer */
app.controller('FooterController', ['$scope', function ($scope) {
   $scope.$on('$includeContentLoaded', function () {
      Layout.initFooter(); // init footer
   });
}]);

/* Base Controller */
app.controller('BaseController', ['$scope', function ($scope) {
   $scope.data = {};
   $scope.base = {
      isProcessing: 0,
      isLoading: 1
   };
   $scope.stringData = '';

   $scope.startEdit = function () {
      $scope.stringData = JSON.stringify($scope.data);
   };

   $scope.checkChanges = function () {
      var strData = JSON.stringify($scope.data);
      return (strData !== $scope.stringData);
   };
}]);