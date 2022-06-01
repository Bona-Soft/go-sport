
//Half documentation http://jdewit.github.io/bootstrap-timepicker/

(function (root, factory) {
   "use strict";

   /*global define*/
   if (typeof define === 'function' && define.amd) {
      define(['angular', 'dateTimePicker'], factory);  // AMD
   } else if (typeof module === 'object' && module.exports) {
      module.exports = factory(require('angular'), require('dateTimePicker')); // Node
   } else {
      factory(root.angular);					// Browser
   }
}(this, function (angular) {
   "use strict";
   angular.module('myb.dateTimePicker', [])
   
	.directive('mybTimepicker', function ($filter) {
	 var _options = {
		template: 'dropdown', 
		maxHours: 24,
		snapToStep: false,
		minuteStep: 15,
		showSeconds: false,
		secondStep: 15,
		defaultTime: 'current',
		showMeridian: true,
		showInputs: true,
		disableFocus: false,
		disableMousewheel: false,
		modalBackdrop: false,
		appendWidgetTo: 'body',
		explicitMode: false,
		icons: {
		   up: 'fa fa-angle-up', //'glyphicon glyphicon-chevron-up',
		   down: 'fa fa-angle-down'//'glyphicon glyphicon-chevron-down'
		}
	 };

	 return {
		restrict: 'AC',
		require: 'ngModel',
		link: function (scope, elem, attrs, ngModel) {
		   var _timepickerOptions = angular.extend(_options, scope.$eval(attrs.mybTimepicker));
		
		   elem.timepicker(_timepickerOptions);
		}
	 }

	})


	// Datepicker Default
	// ******************
	// Include Plugin: /Base/Assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js
	// Include Style: /Base/Assets/global/plugins/bootstrap-datepicker/css/bootstrap - datepicker3.min.css
	// Include: 'Base/assets/global/plugins/jquery-inputmask/jquery.inputmask.bundle.min.js',
	.directive('mybDatepicker', function ($filter) {
	 var _options =
		{
		   format: 'dd/mm/yyyy',
		   defaultDate: null,
		   //TODO: Language
		   defaultLanguage: 'EN',
		   ISOString: false
		}

	 return {
		restrict: 'AC',
		require: 'ngModel',
		link: function (scope, elem, attrs, ngModel) {
         var _datepickerOptions = angular.extend(_options, scope.$eval(attrs.mybDatepicker));
         ngModel.$formatters.push(formatter);
		   ngModel.$parsers.push(parser);

		   if (jQuery().datepicker) {
			  elem.datepicker({
				 format: _datepickerOptions.format,
				 rtl: false,
				 orientation: "left",
              autoclose: true
			  });

			  if (scope[attrs.ngModel]) {
				 formatter(scope[attrs.ngModel])
			  }
			  else if (_datepickerOptions.defaultDate == 'today') {
				 formatter(new Date());

				 //TODO: ver valor seteado de today
			  }
		   }

		   function parser(value) {
			  var date;

			  try {
				 date = moment(value, _datepickerOptions.format.toUpperCase()).toDate();
			  }
			  catch (e) {
				 return value;
			  }

			  if (_datepickerOptions.ISOString) {
				 return date.toISOString();
			  }
			  return date;
		   }

		   function formatter(value) {
			  var tmpDate;

			  if (value == null || value == 'undefined')
				 return '';

			  if (value instanceof Date) {
				 tmpDate = value;
			  }
			  else {
              try {
                 tmpDate = new Date(value);
              }
              catch{
                 return;
              }
			  }
			  tmpDate.setHours(0);
			  tmpDate.setMinutes(0);
			  tmpDate.setSeconds(0);
			  tmpDate.setMilliseconds(0);
			  elem.datepicker("setDate", tmpDate);

			  return elem.val();
			  //Formatea el valor
			  //return $filter('date')(tmpDate, _datepickerOptions.format);
		   }
		}
	 };
	})

	// FormatDate Default
	// ******************
	.directive('cdFormatdateDefault', function ($filter) {
	 return {
		restrict: 'A',
		require: 'ngModel',
		link: function (scope, element, attrs, ngModel) {
		   ngModel.$formatters.push(formatter);
		   ngModel.$parsers.push(parser);

		   function parser(value) {
			  var arrDate = value.split("/");
			  try {
				 date = new Date(arrDate[2], arrDate[1] - 1, arrDate[0]);
			  }
			  catch (e) {
				 ngModel.$setValidity('cdFormatdateDefault', false);
				 return value;
			  }
			  ngModel.$setValidity('cdFormatdateDefault', true);

			  if (attrs.cdFormatdateDefault == "ISOString") {
				 return date.toISOString();
			  }
			  return date;
		   }

		   function formatter(value) {
			  return $filter('date')(value, "dd/MM/yyyy");
		   }
		}
	 };
	})

	// InputMaskDate Default
	// ******************
	// Include Plugin: /Base/Assets/global/plugins/jquery-inputmask/jquery.inputmask.bundle.min.js
	.directive('cdInputmaskdateDefault', function () {
	 return {
		restrict: 'AC',
		link: function (scope, elem, attrs) {
		   if (jQuery().inputmask) {
			  elem.inputmask("d/m/y", { placeholder: "dd/mm/aaaa" });
		   }
		}
	 };
	})
}));