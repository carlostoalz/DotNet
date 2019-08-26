'use strict';

var AppDaservaLogin = angular.module('App.Daserva.Login', ['ngRoute','App.Daserva.Directives']).config(function ($routeProvider) {
    $routeProvider
        .when("/", {
            controller: "loginController",
            templateUrl: "/Views/Login/Templates/Login.html"
        });
});