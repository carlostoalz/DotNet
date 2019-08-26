'use strict';

var directives = angular.module('App.Daserva.Directives', []);
//solo numeros
directives.directive('number', function () {
    return {
        require: 'ngModel',
        restrict: 'A',
        link: function (scope, element, attrs, ctrl) {
            ctrl.$parsers.push(function (input) {
                if (input === undefined) return '';
                var inputNumber = input.toString().replace(/[^0-9]$/g, '');
                if (inputNumber !== input) {
                    ctrl.$setViewValue(inputNumber);
                    ctrl.$render();
                }
                return inputNumber;
            });
        }
    };
});

//solo letras
directives.directive('letter', function () {
    return {
        require: 'ngModel',
        restrict: 'A',
        link: function (scope, element, attrs, ctrl) {
            ctrl.$parsers.push(function (input) {
                if (input === undefined) return '';
                var inputNumber = input.toString().replace(/[0-9]$/g, '');
                if (inputNumber !== input) {
                    ctrl.$setViewValue(inputNumber);
                    ctrl.$render();
                }
                return inputNumber;
            });
        }
    };
});

//para el run
directives.directive('run', function () {
    return {
        require: 'ngModel',
        restrict: 'A',
        link: function (scope, element, attrs, ctrl) {
            ctrl.$parsers.push(function (input) {
                if (input === undefined) return '';
                if (/^([0-9])-([0-9]){5,}/.test(input)) {
                    return input;
                } else {
                    ctrl.$setViewValue(input);
                    ctrl.$render();
                    return '';
                }                
            });
        }
    };
});