(function (root, factory) {
   "use strict";

   /*global define*/
   if (typeof define === 'function' && define.amd) {
      define(['angular', 'validatorService'], factory);  // AMD module
   } else if (typeof module === 'object' && module.exports) {
      module.exports = factory(require('angular'), require('validatorService'));  // CommonJS module / Node
   } else {
      factory(root.angular, root.errSer);	 // Browser global
   }
}(this, function (angular, errSer) {
   "use strict";

   angular.module('myb.ngValidatorService', [])
      .info({ version: '1.0.0' })
      // 2. set a constant
      .constant('MODULE_VERSION', '1.0.0')

      .provider('mybValidatorService', function () {
         var _password = '';

         //TODO: Improvment: options.ErrorMessage. nomenclaturas correctas
         var _options = {
            requiredMessage: 'Mandatory field.',
            emailMessage: 'Invalid email format.',
            passwordMessage: 'Invalid password complexy.',
            repeatPasswordMessage: 'Passwords are not the same.',
            minlength: 'The password is too short.', //minLengthMessage
            maxlength: 'The password is too long.',
            dateMessage: 'The date format is incorrect',
            checkFirstTime: false,
            messageClass: 'help-block error validatorMessage',
            preventEventsOnInvalid: true
         };

          var _required = function (modelValue, viewValue, ele, ctrl) {
              if(ele.isFirstTime){
                  ele.isFirstTime = false;
                  return true;
              }
            return typeof (viewValue) == "undefined" || viewValue == null || viewValue.length > 0;
         },

            _emailFormat = function (modelValue, viewValue, ele, ctrl) {
               if (!modelValue)
                  return true;
               return /^([^\x00-\x20\x22\x28\x29\x2c\x2e\x3a-\x3c\x3e\x40\x5b-\x5d\x7f-\xff]+|\x22([^\x0d\x22\x5c\x80-\xff]|\x5c[\x00-\x7f])*\x22)(\x2e([^\x00-\x20\x22\x28\x29\x2c\x2e\x3a-\x3c\x3e\x40\x5b-\x5d\x7f-\xff]+|\x22([^\x0d\x22\x5c\x80-\xff]|\x5c[\x00-\x7f])*\x22))*\x40([^\x00-\x20\x22\x28\x29\x2c\x2e\x3a-\x3c\x3e\x40\x5b-\x5d\x7f-\xff]+|\x5b([^\x0d\x5b-\x5d\x80-\xff]|\x5c[\x00-\x7f])*\x5d)(\x2e([^\x00-\x20\x22\x28\x29\x2c\x2e\x3a-\x3c\x3e\x40\x5b-\x5d\x7f-\xff]+|\x5b([^\x0d\x5b-\x5d\x80-\xff]|\x5c[\x00-\x7f])*\x5d))*$/.test(modelValue);
            },

            _passwordComplexity = function (modelValue, viewValue, ele, ctrl) {
               _password = viewValue;
               if (viewValue)
                  return /^(?=.*\d)(?=.*)(?=.*[a-z])(?=.*[A-Z]).{8,}$/.test(viewValue);
               return true;
            },

            _passwordRepeated = function (modelValue, viewValue, ele, ctrl) {
               if (viewValue == _password)
                  return true;
               return false;
            },

            _minlength = null,
            _maxlength = null,

            _AddValidation = function (funcName, func) {
               _validateFunction[funcName] = func;
            },

            _dateFormat = function (modelValue, viewValue, ele, ctrl) {
               if (!modelValue)
                  return true;
               var arrDate = viewValue.split("/");
               var date;
               try {
                  date = new Date(arrDate[2], arrDate[1] - 1, arrDate[0]);
               }
               catch (e) {
                  return false;
               }

               if (isNaN(date.getTime())) {
                  return false;
               }
               return true;
            };

         var _validateFunction = {
            required: _required,
            emailFormat: _emailFormat,
            passwordComplexity: _passwordComplexity,
            passwordRepeated: _passwordRepeated,
            minlength: _minlength,
            maxlength: _maxlength,
            dateFormat: _dateFormat
         };

         var _validateForm = function (form) {
            var _form = form;
            var _valid = true;

            if (form) {
               angular.forEach(form.$$controls, function (control, controlKey) {
                  angular.forEach(control.$validators, function (validatorFunc, validatorKey) {
                     if (typeof (control.$viewValue) == "undefined") control.$viewValue = "";
                     if (!validatorFunc(undefined, control.$viewValue)) {
                        control.$setValidity(validatorKey, false);
                     }
                  });

                  if (Object.keys(control.$error).length > 0) {
                     var mybValidatorGroup = $(control.$$element).parent('[myb-validator-group]');
                     var mybValidatorErrorMessage = $(control.$$element).parent('[myb-validator-group]').children('[myb-validator-error-message]');
                     mybValidatorGroup.isFirstTime = false;
                     mybValidatorErrorMessage.isFirstTime = false;
                     _handledValidatorGroup(true, mybValidatorGroup);
                     _handledErrorMessage(control.$error, mybValidatorErrorMessage);
                     _valid = false;
                  }
               });
            }

            return _valid;
         };

         var _handledErrorMessage = function (validatorFunctions, element, onErrorHideElement) {
            var errors = '';
            if (!onErrorHideElement)
               onErrorHideElement = element.parents("[myb-validator-group]").children("[myb-validator-onErrorHide]");

            if (validatorFunctions) {
               if (validatorFunctions.required)
                  errors = _options.requiredMessage
               else {
                  if (validatorFunctions.emailFormat)
                     errors += _options.emailMessage;
                  if (validatorFunctions.dateFormat)
                     errors += _options.dateMessage;
                  if (validatorFunctions.passwordComplexity)
                     errors += _options.passwordMessage;
                  if (validatorFunctions.passwordRepeated)
                     errors += _options.repeatPasswordMessage;
                  if (validatorFunctions.minlength)
                     errors += _options.minlength;
                  if (validatorFunctions.maxlength)
                     errors += _options.maxlength;
               }
            }

            if (element.errorMessage && element.errorMessage.length > 0 && errors.length > 0) {
               errors = element.errorMessage;
            }

            if (onErrorHideElement) {
               if (errors.length > 0) {
                  onErrorHideElement.addClass('hide');
               } else {
                  onErrorHideElement.removeClass('hide');
               }
            }

            if (errors.length > 0) {
               element.removeClass('hide');
            } else {
               element.addClass('hide');
            }

            element.html(errors);
         }

         var _handledValidatorGroup = function (isInvalid, elem) {
            if (typeof (isInvalid) == "undefined" || isInvalid == null) {
               elem.removeClass('has-error');
               elem.removeClass('has-success');
               return;
            }
            elem.toggleClass('has-error', !!isInvalid);
            if (elem.isFirstTime)
               elem.isFirstTime = false
            else
               elem.toggleClass('has-success', !isInvalid);
         }

         return {
            setDefaults: function (options) {
               _options = angular.extend(_options, options);
            },
            $get: function () {
               return {
                  validateFunction: _validateFunction,
                  validateForm: _validateForm,
                  SetValidation: _AddValidation,
                  options: _options,
                  password: function () { return _password; },
                  handledErrorMessage: _handledErrorMessage,
                  handledValidatorGroup: _handledValidatorGroup
               }
            }
         }
      })

      .directive('mybValidatorOnClick', ["mybValidatorService", function (mybValidatorService) {
         return {
            restrict: 'A',
            require: '^form',
            link: {
               pre: function (scope, element, attrs) {
                  var _preventEvents;
                  var _options = mybValidatorService.options;
                  var _formName = element.parents('[ng-form]') && element.parents('[ng-form]').length
                     ? element.parents('[ng-form]').attr('name')
                     : element.parents('form').attr('name');
                  var _form = attrs["myb-validator-on-click"] && attrs["myb-validator-on-click"].length
                     ? attrs["myb-validator-on-click"]
                     : scope[_formName];

                  if (typeof (attrs["mybValidatorPreventeventsoninvalid"]) == "undefined") {
                     _preventEvents = _options.preventEventsOnInvalid;
                  } else {
                     _preventEvents = true;
                  }

                  if (_form) {
                     element.bind('click', function (event) {
                        if (!mybValidatorService.validateForm(_form)
                           && _preventEvents) {
                           event.stopImmediatePropagation();
                        }
                     });
                  }
               }
            }
         };
      }])

      .directive('mybValidatorGroup', ["mybValidatorService", function (mybValidatorService) {
         return {
            restrict: 'A',
            require: '^form',
            link: function (scope, element, attrs) {
               var _options = mybValidatorService.options;
               var _formName = element.parents('[ng-form]').length ? element.parents('[ng-form]').attr('ng-form') : element.parents('form').attr('name');
               var _controlName = element.find('.form-control').length ? element.find('.form-control').attr('name') : element.find('input').attr('name');
               element.isFirstTime = !_options.checkFirstTime;

               scope.$watch(_formName + '.' + _controlName + '.$invalid', function (newval) {
                  mybValidatorService.handledValidatorGroup(newval, element);
               });
            }
         };
      }])

      .directive('mybValidatorErrorMessage', ["mybValidatorService", function (mybValidatorService) {
         return {
            restrict: 'A',
            require: '^form',
            link: function (scope, element, attrs, form) {
               var options = mybValidatorService.options;
               var _formName = form
                  ? form.$name
                  : (element.parents('[ng-form]').length
                     ? elem.parents('[ng-form]').attr('ng-form')
                     : elem.parents('form').attr('name'));
               var _controlName = element.parents("[myb-validator-group]").children("[myb-validator]").attr("Name");
               var breakline = function (errors) { return errors.length ? "\n" : ""; };
               var onErrorHideElement = element.parents("[myb-validator-group]").children("[myb-validator-onErrorHide]");
               angular.element(element).addClass(options.messageClass);
               element.isFirstTime = !options.checkFirstTime;
               element.errorMessage = attrs["mybValidatorErrorMessage"];

               scope.$watch(_formName + '.' + _controlName + '.$error', function (newval, oldval, scope) {
                  mybValidatorService.handledErrorMessage(newval, element, onErrorHideElement);
               }, true);
            }
         };
      }])

      .directive('mybValidator', ["mybValidatorService", function (mybValidatorService) {
         return {
            restrict: 'A',
            require: '?ngModel',
            updateOn: 'blur',
            link: function (scope, ele, attrs, ctrl) {
               var _validatorConditions = attrs.mybValidator.toUpperCase();

               var required = _validatorConditions.search("REQUIRED") >= 0 || attrs.required;
               var email = _validatorConditions.search("EMAIL") >= 0 || attrs.type == "email";
               var date = _validatorConditions.search("DATE") >= 0 || attrs.type == "date";
               var password = _validatorConditions.search("PASSWORD") >= 0;
               var repeatPassword = _validatorConditions.search("REPEATPASSWORD") >= 0;
               var minlength = _validatorConditions.search("MINLENGTH") >= 0 || attrs.minlength > 0;
               var maxlength = _validatorConditions.search("MAXLENGTH") >= 0 || attrs.maxlength > 0;

                ele.isFirstTime = !mybValidatorService.options.checkFirstTime;
                if (required && mybValidatorService.validateFunction.required) ctrl.$validators.required = function (m, v) { return mybValidatorService.validateFunction.required(m, v, ele, ctrl) };
                if (email && mybValidatorService.validateFunction.emailFormat) ctrl.$validators.emailFormat = function (m, v) { return mybValidatorService.validateFunction.emailFormat(m, v, ele, ctrl) };
                if (date && mybValidatorService.validateFunction.dateFormat) ctrl.$validators.dateFormat = function (m, v) { return mybValidatorService.validateFunction.dateFormat(m, v, ele, ctrl)};
                if (minlength && mybValidatorService.validateFunction.minLength) ctrl.$validators.minlength = function (m, v) { return mybValidatorService.validateFunction.minlength(m, v, ele, ctrl) };
                if (maxlength && mybValidatorService.validateFunction.maxLength) ctrl.$validators.maxlength = function (m, v) { return mybValidatorService.validateFunction.maxlength(m, v, ele, ctrl) };
                if (password && !repeatPassword && mybValidatorService.validateFunction.passwordComplexity) ctrl.$validators.passwordComplexity = function (m, v) { return mybValidatorService.validateFunction.passwordComplexity(m, v, ele, ctrl) };
               if (repeatPassword && mybValidatorService.validateFunction.passwordRepeated) {
                   ctrl.$validators.passwordRepeated = function (m, v) { return mybValidatorService.validateFunction.passwordRepeated(m, v, ele, ctrl) };
                  scope.$watch(mybValidatorService.password, function (newval, oldval, scope) {
                     if (newval != oldval) {
                        ctrl.$error = {};
                        ctrl.$valid = true;
                        ctrl.$invalid = null;
                        ctrl.$$element.val("");
                     }
                  }, true);
               }
            }
         };
      }]);
}));