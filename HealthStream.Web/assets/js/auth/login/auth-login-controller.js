/*global app, console */

app.controller('loginController', [
  '$location', 'settingsService', 'authService', function ($location, settingsService, authService) {
    'use strict';
    var self = this;
    
    this.login_data = {
      username: '',
      password: '',
      remember_me: false
    };
    
    this.message = '';
    
    this.login = function () {
      authService.login(this.login_data).then(
        function (response) {
          $location.path(settingsService.homepage);
        },
        function (error) {
          console.log(error);
          self.message = error;
        }
      );
    };
  }
]);