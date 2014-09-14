/*global angular */
angular.module('authorizationEventHandler', [])
  .run(['$rootScope', '$location', 'authService', 'settingsService', function ($rootScope, $location, authService, settingsService) {
    'use strict';
    console.log("adding event handler");
    $rootScope.$on('$routeChangeStart', function (event, next, current) {
      if (!authService.is_logged_in() && !next.allow_anonymous) {
        $location.path(settingsService.login_route);
      }
    });
  }]);