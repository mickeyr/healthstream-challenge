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