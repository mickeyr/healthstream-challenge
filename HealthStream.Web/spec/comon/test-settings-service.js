/*global describe, beforeEach, module, spyOn, it, inject, expect */
describe('settings service', function () {
  'use strict';
  var store = {};
  var settingsService;

  beforeEach(module("healthStreamAuth"));
  
  beforeEach(inject(function (_settingsService_) {
    settingsService = _settingsService_;
  }));
  
  
  it('should contain the base url', function () {
    expect(settingsService.base_url).not.toBe(undefined);
  });
  
  it('should contain the login route', function () {
    expect(settingsService.login_route).not.toBe(undefined);
  });
  
  it('should contain the homepage route', function() {
    expect(settingsService.homepage_route).not.toBe(undefined);
  });
});