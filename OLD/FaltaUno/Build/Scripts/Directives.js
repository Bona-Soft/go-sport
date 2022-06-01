/****************************
Global Directives
*****************************/
(function (root, factory) {
   "use strict";

   /*global define*/
   if (typeof define === 'function' && define.amd) {
      define(['angular', 'defaultDirectives'], factory);  // AMD
   } else if (typeof module === 'object' && module.exports) {
      module.exports = factory(require('angular'), require('defaultDirectives')); // Node
   } else {
      factory(root.angular);					// Browser
   }
}(this, function (angular) {
   "use strict";
   angular.module('myb.defaultDirectives', [])
      // Route State Load Spinner(used on page or content load)
      .directive('ngSpinnerBar', ['$rootScope',
         function ($rootScope) {
            return {
               link: function (scope, element, attrs) {
                  // by defult hide the spinner bar
                  element.addClass('hide'); // hide spinner bar by default

                  // display the spinner bar whenever the route changes(the content part started loading)
                  $rootScope.$on('$stateChangeStart', function () {
                     element.removeClass('hide'); // show spinner bar
                  });

                  // hide the spinner bar on rounte change success(after the content loaded)
                  $rootScope.$on('$stateChangeSuccess', function () {
                     element.addClass('hide'); // hide spinner bar
                     $('body').removeClass('page-on-load'); // remove page loading indicator
                     //Layout.setSidebarMenuActiveLink('match'); // activate selected link in the sidebar menu

                     // auto scrpll to page top
                     setTimeout(function () {
                        //App.scrollTop(); // scroll to the top on content load //esto es scroll to top de metronic
                     }, $rootScope.settings.layoutDef.pageAutoScrollOnLoad);
                  });

                  // handle errors
                  $rootScope.$on('$stateNotFound', function () {
                     element.addClass('hide'); // hide spinner bar
                  });

                  // handle errors
                  $rootScope.$on('$stateChangeError', function () {
                     element.addClass('hide'); // hide spinner bar
                  });
               }
            };
         }
      ])

      //TODO: seguir tocando
      .directive('mybAffixer', ['$window', '$rootScope', '$timeout', function ($window, $rootScope, $timeout) {
         return {
            restrict: 'A',
            link: function ($scope, $element, attr) {
               var win = angular.element($window);
               var topOffset = attr["mybAffixerTop"] && attr["mybAffixerTop"].length > 0
                  ? attr["mybAffixerTop"]
                  : $element[0].offsetTop;
               var topOffsetClass = attr["mybAffixer"] && attr["mybAffixer"].length > 0
                  ? attr["mybAffixer"]
                  : "affixer";
               var options = attr["mybAffixerOptions"];
               var mtbAffixerOptionsAffixOnSmaller = false;

               if (!attr["mtbAffixerOptionsAffixOnSmaller"]) {
                  mtbAffixerOptionsAffixOnSmaller = attr["mtbAffixerOptionsAffixOnSmaller"] != false;
               }

               var emptyDiv = document.createElement('div');
               emptyDiv.id = "affixerClone";
               emptyDiv.style.width = $element[0].offsetWidth + "px";
               emptyDiv.style.height = ($element[0].offsetHeight - $element[0].offsetTop) + "px";


               var winIsBiggerThanElement = true;
               if (mtbAffixerOptionsAffixOnSmaller == false) {
                  winIsBiggerThanElement = ($element[0].offsetHeight - $element[0].offsetTop) < win.innerHeight();
               }

               var affixElement = function () {
                  if ($window.pageYOffset > topOffset && winIsBiggerThanElement) {
                     $element.parent().append(emptyDiv);
                     $element.css('position', 'fixed');
                     $element.addClass(topOffsetClass);
                  } else {
                     $element.parent().find("#affixerClone").remove();
                     $element.css('position', '');
                     $element.removeClass(topOffsetClass);
                  }
               };

               var parentContainer = $element.parent();

               var affixResizeElement = function () {
                  var paddingLeft = $(parentContainer[0]).css('padding-left') ? $(parentContainer[0]).css('padding-left').replace("px","") : 0;
                  var paddingRight = $(parentContainer[0]).css('padding-right') ? $(parentContainer[0]).css('padding-right').replace("px", "") : 0;
                  $($element[0]).css("width", (parentContainer[0].offsetWidth - paddingLeft - paddingRight) + "px");
               };

               $timeout(affixResizeElement, 50);

               $rootScope.$on('$stateChangeStart', function () {
                  win.unbind('scroll', affixElement);
               });
               win.bind('scroll', affixElement);
               win.bind('resize', function () {
                  $timeout(affixResizeElement, 50);
               });
            }
         };
      }])

      // Handle window onresize
      .directive('onResize', function ($window) {
         return {
            link: function (scope) {
               function onResize(e) {
                  // Namespacing events with name of directive + event to avoid collisions
                  scope.$broadcast('onResize::resize'); //broadcast a los childscopes el evento 'onResize::resize' para poder escucharlo en otras directivas
                  handleDropdownToggle();
                  //showDropdownSportsNames();
               }

               function handleDropdownToggle() {
                  if (document.documentElement.clientWidth >= 768) {
                     if ($('.navbar-collapse').length > 0) {
                        if ($('.navbar-collapse').hasClass('in')) {
                           $('.navbar-collapse').removeClass('in');
                        }
                     }
                  }
               }

               /*
               function showDropdownSportsNames() {
                  console.log(document.documentElement.clientWidth);
                  if (document.getElementById('dropdown-sports-title')) {
                     if (document.documentElement.clientWidth < 768) {
                        document.getElementById('dropdown-sports-title').style.display = 'inline-block';
                     } else {
                        document.getElementById('dropdown-sports-title').style.display = 'none';
                     }
                  }
               }
               */
               function cleanUp() {
                  angular.element($window).off('resize', onResize);
               }
               angular.element($window).on('resize', onResize);
               scope.$on('$destroy', cleanUp);
               /*
               $(document).ready(function () {
                  onResize(event);
               });
               */
            }
         }
      })

      // Handle global LINK click
      .directive('a', function () {
         return {
            restrict: 'E',
            link: function (scope, elem, attrs) {
               if (attrs.ngClick || attrs.href === '' || attrs.href === '#') {
                  elem.on('click', function (e) {
                     e.preventDefault(); // prevent link click for above criteria
                  });
               }
            }
         };
      })

      // Handle Dropdown Hover Plugin Integration
      .directive('dropdownMenuHover', function () {
         return {
            link: function (scope, elem) {
               elem.dropdownHover();
            }
         };
      })

      // ZoomItem
      // *********
      .directive('cdZoomitem', function ($rootScope, $state) {
         return {
            restrict: 'A',
            link: function (scope, element, attrs) {
               scope.$watch('$root.zoom', function (v) {
                  if (element.is("iframe")) {
                     element.contents().find('body').attr("style", "zoom:" + $rootScope.zoom);
                  }
                  else {
                     //FIX para v3.html - No cambia el zoom si esta en v3
                     if ($state.current.name != 'v3') {
                        element.attr("style", "zoom:" + $rootScope.zoom);
                     }
                  }
               });

               if (element.is("iframe")) {
                  element.load(function () {
                     element.contents().find('body').attr("style", "zoom:" + $rootScope.zoom);
                  });
               }
               else {
                  //FIX para v3.html - No cambia el zoom si esta en v3
                  if ($state.current.name != 'v3') {
                     element.attr("style", "zoom:" + $rootScope.zoom);
                  }
               }
            }
         }
      })

      // bootstrapSwitch
      // ***************
      .directive('bootstrapSwitch', [
         function () {
            return {
               restrict: 'A',
               require: '?ngModel',
               link: function (scope, element, attrs, ngModel) {
                  element.bootstrapSwitch();

                  element.on('switchChange.bootstrapSwitch', function (event, state) {
                     if (ngModel) {
                        scope.$apply(function () {
                           ngModel.$setViewValue(state);
                        });
                     }
                  });

                  scope.$watch(attrs.ngModel, function (newValue, oldValue) {
                     if (newValue) {
                        element.bootstrapSwitch('state', true, true);
                     } else {
                        element.bootstrapSwitch('state', false, true);
                     }
                  });
               }
            };
         }
      ])

      // Print
      // *********
      .directive('cdPrint', function ($rootScope, $state) {
         var printSection = document.getElementById("printSection");

         function printElement(elem) {
            // clones the element you want to print
            var domClone = elem.cloneNode(true);
            if (!printSection) {
               printSection = document.createElement("div");
               printSection.id = "printSection";
               document.body.appendChild(printSection);
            } else {
               printSection.innerHTML = "";
            }
            printSection.appendChild(domClone);
         }

         function link(scope, element, attrs) {
            element.on("click", function () {
               var elemToPrint = document.getElementById(attrs.printElementId);
               if (elemToPrint) {
                  printElement(elemToPrint);
                  window.print();
               }
            });
         }

         return {
            restrict: 'A',
            link: link
         };
      })

      // After rendering
      // ***************
      .directive('afterRender', ['$timeout', function ($timeout) {
         var def = {
            restrict: 'A',
            transclude: false,
            link: function (scope, element, attrs) {
               scope.$watch(attrs.afterRenderWatch, function (newValue, oldValue) {
                  if (newValue != oldValue) {
                     $timeout(function () { eval(attrs.afterRender); eval(attrs.afterRenderFinish); }, 0);
                  }
               });
            }
         };
         return def;
      }])

      // Datatable rendering
      // *******************
      /*.directive('datatableRender', ['$timeout', function ($timeout) {
         var def = {
            restrict: 'A',
            transclude: false,
            link: function (scope, element, attrs) {
               scope.$watch(attrs.datatableRender, function (newValue, oldValue) {
                  if (newValue != oldValue) {
                     eval(attrs.datatableDestroy)
                     eval('scope.' + attrs.datatableVar + ' = ' + 'angular.copy(scope.' + attrs.datatableRender + ')');
                     $timeout(function () { eval(attrs.datatableInit); }, 0);
                  }
               });
            }
         };
         return def;
      }])*/

      // Datatable renderingEx
      // *********************
      .directive('datatableRender', ['$timeout', function ($timeout) {
         var def = {
            restrict: 'A',
            transclude: false,
            link: function (scope, element, attrs) {
               var configuracion = {};
               var configuracionDefault = {
                  // Internationalisation. For more info refer to http://datatables.net/manual/i18n
                  "language": {
                     "aria": {
                        "sortAscending": ": activar para ordenar la columna en forma ascendente",
                        "sortDescending": ": activar para ordenar la columna en forma descendiente"
                     },
                     "emptyTable": "No hay informacion disponible en la tabla.",
                     "info": "Mostrando _START_ a _END_ de _TOTAL_ registros",
                     "infoEmpty": "No se encontraron registros",
                     "infoFiltered": "(filtrado de _MAX_ registros totales)",
                     "lengthMenu": "Mostrar _MENU_",
                     "search": "Buscar:",
                     "zeroRecords": "No se encontraron registros para la busqueda.",
                     "paginate": {
                        "previous": "Anterior",
                        "next": "Siguiente",
                        "last": "Ultima",
                        "first": "Primera"
                     }
                  },

                  "bStateSave": true, // save datatable state(pagination, sort, etc) in cookie.

                  /*"columnDefs": [{
                     "targets": [0,1],
                     "orderable": true,
                     "searchable": true
                  }],*/

                  "lengthMenu": [
                     [5, 15, 20, -1],
                     [5, 15, 20, "Todos"] // change per page values here
                  ],
                  // set the initial value
                  "pageLength": 20,
                  "pagingType": "bootstrap_full_number",
                  "order": [
                     [0, "desc"]
                  ] // set first column as a default sort by asc
               }

               if (attrs.datatableConfig) {
                  angular.extend(configuracion, configuracionDefault, scope.$eval(attrs.datatableConfig));
               }
               else {
                  configuracion = configuracionDefault;
               }

               scope.$watch(attrs.datatableRender, function (newValue, oldValue) {
                  if (newValue != oldValue) {
                     destroyDataTable();
                     eval('scope.' + attrs.datatableVar + ' = angular.copy(scope.' + attrs.datatableRender + ')');
                     $timeout(function () { initDataTable(); }, 0);
                  }
               });

               function initDataTable() {
                  if (jQuery.fn.DataTable.isDataTable('#' + attrs.id)) {
                     return;
                  }
                  jQuery('#' + attrs.id).DataTable(configuracion);
               }

               function destroyDataTable() {
                  if (jQuery.fn.DataTable.isDataTable('#' + attrs.id)) {
                     jQuery('#' + attrs.id).DataTable().data().clear();
                     jQuery('#' + attrs.id).DataTable().destroy();
                  }
               }
            }
         };
         return def;
      }])

      // HTML linebreak formatter
      .directive('htmlText', function () {
         return {
            restrict: 'E',
            template: '<div ng-bind-html-unsafe="html"></div>',
            scope: {
               text: '='
            },
            link: function (scope, elem, attrs) {
               var updateText = function () {
                  if (scope.text != null) {
                     scope.html = scope.text.replace('\n', "<br/>");
                  }
               };

               scope.$watch('text', function (newVal, oldVal) {
                  if (oldVal != newVal) {
                     updateText();
                  }
               });
            }
         };
      })

      .directive('format', ['$filter', function ($filter) {
         return {
            require: '?ngModel',
            link: function (scope, elem, attrs, ctrl) {
               if (!ctrl) return;

               ctrl.$formatters.unshift(formatter);
               ctrl.$parsers.unshift(parser);

               formatter(scope[attrs.ngModel]);

               function formatter(value) {
                  var formatNumber = $filter(attrs.format)(ctrl.$modelValue)
                  if (typeof formatNumber === "undefined" || formatNumber === null) {
                     return formatNumber;
                  }
                  return formatNumber.replace(/[\,]/g, '');
               }

               function parser(viewValue) {
                  if (angular.lowercase(attrs.format) == 'number') {
                     var plainNumber = viewValue.replace(/[\,]/g, '.');
                     plainNumber = plainNumber.replace(/[\.]/, '__');
                     plainNumber = plainNumber.replace(/[\.]/g, '');
                     plainNumber = plainNumber.replace(/[\__]+/, '.');
                     plainNumber = plainNumber.replace(/[^\d\.\-]|(?!^)-/g, '');
                     if (plainNumber !== viewValue) {
                        ctrl.$setViewValue(plainNumber);
                        ctrl.$render();
                     }
                     if (plainNumber == '')
                        return null;
                     return parseFloat(plainNumber);
                  }
                  return viewValue;
               }
            }
         };
      }])

      // HTML dynamic angular
      .directive('dynamicHtml', ['$compile', function ($compile) {
         return {
            restrict: 'A',
            replace: true,
            link: function (scope, element, attrs) {
               var template = '';
               var html;

               if (scope.$eval(attrs.dynamicHtml) && scope.$eval(attrs.dynamicHtml) != '') {
                  html = '<span>' + scope.$eval(attrs.dynamicHtml) + '</span>';
                  template = $compile(html)(scope);
               }
               element.replaceWith(template);
            }
         };
      }])

      // Capitalize an input field
      .directive('capitalize', function () {
         return {
            require: 'ngModel',
            link: function (scope, element, attrs, modelCtrl) {
               var capitalize = function (inputValue) {
                  if (inputValue == undefined) inputValue = '';
                  var capitalized = inputValue.toUpperCase();
                  if (capitalized !== inputValue) {
                     modelCtrl.$setViewValue(capitalized);
                     modelCtrl.$render();
                  }
                  return capitalized;
               }
               modelCtrl.$parsers.push(capitalize);
               capitalize(scope[attrs.ngModel]); // capitalize initial value
            }
         };
      })

      // FOCUS an Element on event
      .directive('focusOn', function ($timeout) {
         return function (scope, element, attrs) {
            scope.$on(attrs.focusOn, function (e) {
               $timeout(function () {
                  element[0].focus();
                  element[0].select();
               }, 100); //need some delay to work with ng-disabled
            });
         };
      })

      .directive('onFinishRender', function ($rootScope, $timeout) {
         return {
            restrict: 'A',
            link: function (scope, element, attr) {
               if (scope.$last) {
                  $timeout(function () {
                     $rootScope.$broadcast(attr.onFinishRender);
                  });
               }
            }
         };
      })

      .directive('cdHeaderFixedScroll', function ($rootScope, $state, $timeout) {
         return {
            restrict: 'A',
            link: function (scope, element, attrs) {
               scope.$on(attrs.cdHeaderFixedScroll, function (e) {
                  var th = element.children('thead').clone();
                  var thSoruce = element.children('thead').children('tr').children();
                  var tablehead = angular.element(document.createElement("table"));

                  function setWidthsHead() {
                     // Set the width of table
                     tablehead.attr("style", "width:" + element[0].getBoundingClientRect().width + "px!important;");
                     tablehead.css("max-width", "none");

                     // Get the width of thead source columns
                     colWidth = thSoruce.map(function () {
                        return angular.element(this)[0].getBoundingClientRect().width;
                     }).get();

                     // Set the width of thead columns
                     th.find('tr').children().each(function (i, v) {
                        angular.element(v).css("width", colWidth[i] + "px");
                     });
                  }

                  setWidthsHead();

                  tablehead.attr("id", "tableHeaderFixedScroll");
                  tablehead.attr("class", element.attr("class"));
                  tablehead.addClass("table-head-absolute");
                  tablehead.css("display", "none");

                  tablehead.append(th);
                  element.parent().children('#tableHeaderFixedScroll').remove();
                  element.parent().prepend(tablehead);

                  element.parent().scroll(function () {
                     if (element.position().top < 0) {
                        tablehead.css({ top: element.parent().scrollTop() + 'px' });
                        if (tablehead.css('display') == 'none') {
                           setWidthsHead();
                           tablehead.css({ display: 'table' });
                        }
                     }
                     else if (tablehead.css('display') == 'table') {
                        tablehead.css({ display: 'none' });
                     }
                  });
               });
            }
         };
      })

      .directive('cdFooterFixedScroll', function ($rootScope, $state, $timeout) {
         return {
            restrict: 'A',
            link: function (scope, element, attrs) {
               scope.$on(attrs.cdFooterFixedScroll, function (e) {
                  var tf = element.children('tfoot').clone();
                  var tfSoruce = element.children('tfoot').children('tr').children();
                  var tablefoot = angular.element(document.createElement("table"));

                  function setWidthsFoot() {
                     // Set the width of table
                     tablefoot.attr("style", "width:" + element[0].getBoundingClientRect().width + "px!important;");
                     tablefoot.css("max-width", "none");

                     // Get the width of thead source columns
                     colWidth = tfSoruce.map(function () {
                        return angular.element(this)[0].getBoundingClientRect().width;
                     }).get();

                     // Set the width of foot columns
                     tf.find('tr').children().each(function (i, v) {
                        angular.element(v).css("width", colWidth[i] + "px");
                     });
                  }

                  setWidthsFoot();

                  tablefoot.attr("id", "tableFooterFixedScroll");
                  tablefoot.attr("class", element.attr("class"));
                  tablefoot.addClass("table-head-absolute");
                  tablefoot.css("display", "none");

                  tablefoot.append(tf);
                  element.parent().children('#tableFooterFixedScroll').remove();
                  element.parent().append(tablefoot);

                  if (element.position().top + element.height() >= element.parent()[0].clientHeight) {
                     tablefoot.css({ bottom: '-' + element.parent().scrollTop() + 'px' });
                     tablefoot.css("display", "table");
                     console.log("cdFooterFixedScroll display");
                  }

                  element.parent().scroll(function () {
                     if (element.position().top + element.height() >= element.parent()[0].clientHeight) {
                        tablefoot.css({ bottom: '-' + element.parent().scrollTop() + 'px' });
                        if (tablefoot.css('display') == 'none') {
                           setWidthsFoot();
                           tablefoot.css({ display: 'table' });
                        }
                     }
                     else if (tablefoot.css('display') == 'table') {
                        tablefoot.css({ display: 'none' });
                     }
                  });
               });
            }
         };
      })

      .directive("scrollToTop", function () {
         var offset = 100;
         var duration = 500;

         return {
            restrict: 'E',
            transclude: true,
            replace: true,
            template: '<div class="scroll-to-top" ng-click="scrollToTop()"><i class="icon-arrow-up"></i></div>',
            controller: function ($scope) {
               $scope.scrollToTop = function () {
                  $('html, body').animate({ scrollTop: 0 }, 500);
               }
            },
            link: function (scope, element, attrs) {
               if (navigator.userAgent.match(/iPhone|iPad|iPod/i)) {  // ios supported
                  $(window).bind("touchend touchcancel touchleave", function (e) {
                     if ($(this).scrollTop() > offset) {
                        //$('.scroll-to-top').fadeIn(duration);
                        element.fadeIn(duration);
                     } else {
                        element.fadeOut(duration);
                     }
                  });
               } else {  // general
                  $(window).scroll(function () {
                     if ($(this).scrollTop() > offset) {
                        element.fadeIn(duration);
                     } else {
                        element.fadeOut(duration);
                     }
                  });
               }
            }
         }
      })

      //Material Design directives

      // Input/Floating labels
      .directive("mybMdInput", function () {
         var handleInput = function (el) {
            if (el.val() != "") {
               el.addClass('edited');
            } else {
               el.removeClass('edited');
            }
         }

         return {
            restrict: 'A',
            require: 'ngModel',
            link: function (scope, element, attrs, ngModel) {
               scope.$watch(function () {
                  return ngModel.$modelValue;
               }, function (newValue, oldValue) {
                  handleInput(element);
               }, true);
            }
         }
      })

      .directive("mybMdInputAutocomplete", function () {
         var handleInput = function (el, newval, oldval) {
            if (newval != "" && newval != null) {
               el.addClass('edited');
               el.find('input[type="search"]').addClass('edited');
            } else {
               el.removeClass('edited');
               el.find('input[type="search"]').removeClass('edited');
            }
         }

         return {
            restrict: 'A',
            link: function (scope, element, attrs) {
               //element.attr('md-selected-item-change', handleInput(element, attrs));

               //scope.$watch(function () {
               //   return element.find('[md-search-text]').attr('[md-search-text]');
               //}
               scope.$watch(attrs.mdSearchText, function (newValue, oldValue) {
                  handleInput(element, newValue, oldValue);
               }, true);
            }
         }
      })

      .directive("mybMdSelect", function () {
         var handleInput = function (el, newval, oldval) {
            if (newval != "" && newval != null && newval.$$mdSelectId > 0) {
               el.addClass('edited');
               el.find('.md-select-value').addClass('edited');
            } else {
               el.removeClass('edited');
               el.find('.md-select-value').removeClass('edited');
            }
         }

         return {
            restrict: 'A',
            require: 'ngModel',
            link: function (scope, element, attrs, ngModel) {
               scope.$watch(function () {
                  return ngModel.$modelValue;
               }, function (newValue, oldValue) {
                  handleInput(element, newValue, oldValue);
               }, true);
            }
         }
      })

      .directive("mybMdRadio", function () {
         return {
            restrict: 'A',
            link: function (scope, element, attrs, ngModel) {
               $('body').on('click', '.md-radio > label', function () {
                  var the = $(this);
                  // find the first span which is our circle/bubble
                  var el = $(this).children('span:first-child');

                  // add the bubble class (we do this so it doesnt show on page load)
                  el.addClass('inc');

                  // clone it
                  var newone = el.clone(true);

                  // add the cloned version before our original
                  el.before(newone);

                  // remove the original so that it is ready to run on next click
                  $("." + el.attr("class") + ":last", the).remove();
               });
            }
         }
      })

      .directive("mybMdCheckbox", function () {
         return {
            restrict: 'A',
            link: function (scope, element, attrs, ngModel) {
               $('body').on('click', '.md-checkbox > label', function () {
                  var the = $(this);
                  // find the first span which is our circle/bubble
                  var el = $(this).children('span:first-child');

                  // add the bubble class (we do this so it doesnt show on page load)
                  el.addClass('inc');

                  // clone it
                  var newone = el.clone(true);

                  // add the cloned version before our original
                  el.before(newone);

                  // remove the original so that it is ready to run on next click
                  $("." + el.attr("class") + ":last", the).remove();
               });
            }
         }
      })

      .directive("mybTag", function () {
         return {
            restrict: 'A',
            link: function (scope, element, attrs, ngModel) {

               element.addClass('myb-tag');

               //TODO:
               //var a = createElement('a');
               //a.addClass('btn btn-xs btn-link pull-right');

               //<a class="btn btn-xs btn-link pull-right" ng-click="data.MatchSearch.Name = null;"><i class="glyphicon glyphicon-remove"></i></a>
               //element.appendChild();

               scope.$watch(attrs["mybTag"], function (newval) {
                  if (newval != null && newval != "") {
                     element.show();
                  } else {
                     element.hide();
                  }

               });
            }
         }
      })

      // Convert to local time
      .directive('mybLocalDatetime', function ($filter) {
         return {
            restrict: 'A',
            link: function (scope, element, attrs) {

               var _fn = function (newval, oldval) {
                  var _date = newval;
                  var _format = attrs.mybLocalDatetimeFormat;

                  _date = moment(moment.utc(_date).toDate()).local().format();
                  if (!isNaN(Date.parse(_date))) {
                     if (_format)
                        _date = $filter('date')(_date, _format);
                     element.html(_date);
                  }
               };

               scope.$watch(attrs.mybLocalDatetime, _fn);
            }
         };
      });
}));

// handle group element heights
   //var handleHeight = function () {
   //    $('[data-auto-height]').each(function () {
   //        var parent = $(this);
   //        var items = $('[data-height]', parent);
   //        var height = 0;
   //        var mode = parent.attr('data-mode');
   //        var offset = parseInt(parent.attr('data-offset') ? parent.attr('data-offset') : 0);

   //        items.each(function () {
   //            if ($(this).attr('data-height') == "height") {
   //                $(this).css('height', '');
   //            } else {
   //                $(this).css('min-height', '');
   //            }

   //            var height_ = (mode == 'base-height' ? $(this).outerHeight() : $(this).outerHeight(true));
   //            if (height_ > height) {
   //                height = height_;
   //            }
   //        });

   //        height = height + offset;

   //        items.each(function () {
   //            if ($(this).attr('data-height') == "height") {
   //                $(this).css('height', height);
   //            } else {
   //                $(this).css('min-height', height);
   //            }
   //        });

   //        if (parent.attr('data-related')) {
   //            $(parent.attr('data-related')).css('height', parent.height());
   //        }
   //    });
   //}

   //handleHeight();