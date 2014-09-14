/*global app */
app.factory('settingsService', function () {
  'use strict';
  
  var _base_url = "http://localhost:3000";
  var _login_route = "/login";
  var _homepage_route = "/";
  
  return {
    base_url: _base_url,
    login_route: _login_route,
    homepage_route: _homepage_route
  };
});