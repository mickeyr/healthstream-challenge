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