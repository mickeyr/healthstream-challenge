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
  
# Running the application from Visual Studio
1. Deploy the HealthStream.Sql project to the database of your choice.  I'm using 
   localdb for development.  Be sure to update the Web.config file in the 
   HealthStream.Api project with connection string of your database.
2. Install npm, grunt, and bower
3. In the HealthStream.Web project, run npm install and grunt
4. Set HealthStream.Api and HealthStream.Web as startup projects
5. Press F5 and visit the URL for the HealthStream.Web project.