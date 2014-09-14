/*global app, console */

app.controller('registerController', [
  '$location', 'settingsService', 'authService', function ($location, settingsService, authService) {
    'use strict';
    var self = this;
    
    this.register_data = {
      username: '',
      password: '',
      confirm_password: '',
      email_address: ''
    };
    
    this.message = '';
    
    this.register = function () {
      authService.register(this.register_data).then(
        function (response) {
          $location.path(settingsService.login_route);
        },
        function (error) {
          self.message = error.error_description;
        }
      );
    };
  }
]);