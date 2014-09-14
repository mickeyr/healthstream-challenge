/*global describe, beforeEach, afterEach, module, it, inject, expect */
describe('auth service', function () {
  'use strict';
  var authService, httpBackend, settingsService;
  var success_login_data = {
      username: 'test',
      password: 'test',
      remember_me: false
    };
  
  var invalid_login_data = {
      username: 'invalid',
      password: 'invalid',
      remember_me: false
    };
  
  var token = "test token";
  
  beforeEach(module("healthStreamAuth"));
  beforeEach(inject(function ($httpBackend, _authService_, _settingsService_) {
    httpBackend = $httpBackend;
    authService = _authService_;
    settingsService = _settingsService_;
    
    httpBackend.whenPOST(settingsService.base_url + '/authentication/authenticate', success_login_data)
      .respond(200, token);
    httpBackend.whenPOST(settingsService.base_url + '/authentication/authenticate', invalid_login_data)
      .respond(401, "Forbidden");
  }));
  
  afterEach(function () {
    httpBackend.verifyNoOutstandingExpectation();
    httpBackend.verifyNoOutstandingRequest();
  });
  
  it('should pass the token received on successful login', function () {
    var receivedResponse;
    console.log(httpBackend);
    console.log(settingsService.base_url);
    
    
    authService.login(success_login_data).then(function (response) {
      receivedResponse = response;
    }, function (error) {
      receivedResponse = error;
    });
    
    httpBackend.flush();
    expect(receivedResponse).toEqual(token);
  });
  
  it('should respond with "Invalid username or password." on invalid login', function () {
    var receivedResponse;
    console.log(httpBackend);
    console.log(settingsService.base_url);
    
    
    authService.login(invalid_login_data).then(function (response) {
      receivedResponse = response;
    }, function (error) {
      receivedResponse = error;
    });
    
    httpBackend.flush();
    expect(receivedResponse).toEqual("Invalid username or password.");
  });
  
});