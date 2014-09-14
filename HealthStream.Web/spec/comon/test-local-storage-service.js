/*global describe, beforeEach, module, spyOn, it, inject, expect */
describe('local storage service', function () {
  'use strict';
  var store = {};
  var _localStorageService;

  beforeEach(module("healthStreamAuth"));
  
  beforeEach(inject(function (_localStorageService_) {
    _localStorageService = _localStorageService_;
    
    spyOn(localStorage, 'getItem').andCallFake(function (key) {
      return store[key];
    });
    
    spyOn(localStorage, 'setItem').andCallFake(function (key, value) {
      store[key] = value;
    });
    
    spyOn(localStorage, 'removeItem').andCallFake(function (key) {
      delete store[key];
    });
    
    spyOn(localStorage, 'clear').andCallFake(function () {
      store = {};
    });
  }));
  
  
  it('should have a set method to save a JSON encoded item to the local storage', function () {
    var key = "testKey";
    var expectedValue = "value";

    _localStorageService.set(key, expectedValue);
    expect(JSON.parse(store[key])).toEqual(expectedValue);
       
  });
  
  it('should have a get method to retrieve and JSON decode an item from storage', function () {
    var key = "testKey";
    var expectedValue = "expectedValue";
    store[key] = JSON.stringify(expectedValue);
    var actual = _localStorageService.get(key);
    expect(actual).toEqual(expectedValue);
  });
  
  it('should have a remove method to remove an item from storage', function () {
    var key = "testKey";
    var expectedValue = "expectedValue";
    store[key] = JSON.stringify(expectedValue);
    _localStorageService.remove(key);
    expect(store[key]).toBe(undefined);
  });
  
  it('should have a clear method to remove all items from storage', function () {
    var key = "testKey";
    var expectedValue = "expectedValue";
    store[key] = JSON.stringify(expectedValue);
    _localStorageService.clear();
    expect(store).toEqual({});
  });
});