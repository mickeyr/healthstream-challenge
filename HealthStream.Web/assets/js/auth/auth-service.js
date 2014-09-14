/*global app*/
app.factory('authService', [
  '$http', '$q', 'settingsService', 'localStorageService', function ($http, $q, settingsService, localStorageService) {
    'use strict';
    
    var _authentication_key = 'authorization';
    var _authentication = localStorageService.get(_authentication_key);
    
    if (typeof (_authentication) === 'undefined' || _authentication === null) {
      _authentication = {
        is_authenticated: false,
        username: '',
        token: ''
      };
    }
    
    var _register = function (register_data) {
      var deferred = $q.defer();
      
      $http.post(settingsService.base_url + '/authentication/register', register_data).success(function (response) {
        deferred.resolve(response);
      }).error(function (err, status) {
        deferred.reject(err);
      });
      
      return deferred.promise;
    };
    
    var _login = function (login_data) {
      var deferred = $q.defer();
      $http.post(settingsService.base_url + '/authentication/authenticate', login_data).success(function (response) {
        _authentication.username = login_data.username;
        _authentication.is_authenticated = true;
        _authentication.token = response;
        localStorageService.set(_authentication_key, _authentication);
        
        deferred.resolve(response);
      }).error(function (err, status) {
        var message = "";
        if(status === 401) {
          message = "Invalid username or password.";
        } else {
          message = err;
        }

        deferred.reject(message);
      });
      
      return deferred.promise;
    };
    
    var _is_logged_in = function () {
      return _authentication.is_authenticated;
    };
    
    return {
      register: _register,
      login: _login,
      is_logged_in: _is_logged_in
    };
  }
]);