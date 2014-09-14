/*global angular */

var app = angular.module("healthStreamAuth", ["ngRoute", "authorizationEventHandler"]);

app.config(["$routeProvider", function ($routeProvider) {
  'use strict';
  
  $routeProvider
    .when("/login", {
      controller: "loginController",
      templateUrl: "/assets/js/auth/login/auth-login.html",
      allow_anonymous: true
    })
    .when("/register", {
      controller: "registerController",
      templateUrl: "/assets/js/auth/register/auth-register.html",
      allow_anonymous: true
    })
    .when("/", {
      controller: "homeController",
      templateUrl: "/assets/js/home/home.html"
    });
}]);