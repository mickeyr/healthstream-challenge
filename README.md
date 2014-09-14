# Authentication System Challenge

This sytem demonstrates a basic three-teir architecture for an authentication system.
The tiers are laid out as follows:

### Data Tier
* HealthStream.Sql
  * Contains the database structure
* HealthStream.Data
  * Data access assembly
  
### Service Layer
* HealthStream.Services
  * Contains the application service libraries
* HealthStream.Api
  * OWIN WebAPI web services for accessing the services remotely
  
### Presentation
* HealthStream.Web
  * Single page AngularJS app utilizing the WebAPI provided above.

## Demo
Live demo can be found [here](http://hs.mickeyr.com).

## Running the application from Visual Studio
1. Deploy the HealthStream.Sql project to the database of your choice.  I'm using 
   localdb for development.  Be sure to update the Web.config file in the 
   HealthStream.Api project with connection string of your database.
2. Install npm, grunt, and bower
3. In the HealthStream.Web project, run npm install and grunt
4. Set HealthStream.Api and HealthStream.Web as startup projects
5. Press F5 and visit the URL for the HealthStream.Web project.

## Feature Roadmap
- Basic authentication system
  - [x] Implement registration
  - [x] Implement basic user authentication
  - [ ] Implement account lockout on 5 incorrect login attempts.  
        - User must reset password to re-enable account.
  - [ ] Implement password reset functionality
- Token Authentication
  - [ ] Regenerate random token on login and lock to the IP address given
  - [ ] Implement OWIN middleware to validate token on each needed request 
  - [ ] Implement AngularJS interceptor to add token to each ajax call
		
    
    
    
   
