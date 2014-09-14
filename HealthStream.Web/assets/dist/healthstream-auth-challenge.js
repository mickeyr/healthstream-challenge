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
      
      $http.post(settingsService.base_url + 'authentication/register', register_data).success(function (response) {
        deferred.resolve(response);
      }).error(function (err, status) {
        deferred.reject(err);
      });
      
      return deferred.promise;
    };
    
    var _login = function (login_data) {
      var deferred = $q.defer();
      $http.post(settingsService.base_url + 'authentication/authenticate', login_data).success(function (response) {
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
/*global app */

app.factory('localStorageService', function () {
  'use strict';
  
  var prefix = 'ls';
  var storage = window.localStorage;
  
  var _set = function (key, value) {
    storage.setItem(key, JSON.stringify(value));
  };
  
  var _get = function (key) {
    return JSON.parse(storage.getItem(key));
  };
  
  var _remove = function (key) {
    storage.removeItem(key);
  };
  
  var _clear = function () {
    storage.clear();
  };
  
  return {
    set: _set,
    get: _get,
    remove: _remove,
    clear: _clear
  };
});
/*global app */
app.factory('settingsService', function () {
  'use strict';
  
  var _base_url = "http://localhost:3000/";
  var _login_route = "/login";
  var _homepage = "/";
  
  return {
    base_url: _base_url,
    login_route: _login_route,
    homepage: _homepage
  };
});
/*global app */
app.controller('homeController', function () {
  'use strict';
});