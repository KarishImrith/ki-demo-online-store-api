# Sprint Planning
## Compulsary
* Create dotnet core 8 application
	* [DONE] Create OnlineStore.App
  * [DONE] Remove redundant code & cleanup
	* [DONE] Setup stylecop
	* [DONE] Setup healthchecks
	* [DONE] Setup logging using serilog
  * [DONE] Setup swagger
* Setup Authentication with google
  * [DONE] Add security via identity core
  * [DONE] Update swagger with identity authentication
	* [DONE] Wire up google authentication
* Create the database
    * [DONE] Create models
	* [DONE] Setup automatic migration on startup
	* [!] Setup seed data
* Implement logic
	* [DONE] Create OnlineStore.Logic
  * [DONE] Setup middleware & logic to get userId from httpcontextaccessor
  * [DONE] Setup mediatr
	* [DONE] Product Concern: Create mediator handlers as per planning
	* [DONE] Create OnlineStore.Logic.UnitTests
	* [DONE] Product Concern: Create unit tests
	* Setup OData
	* Product Concern: Create controller
	* ShoppingCart Concern: Create mediator handlers as per planning & unit tests
	* ShoppingCart Concern: Create controller
* Create file storage libraries
	* Create Demo.Libraries.FileStorage.Abstractions library as per planning
	* Create Demo.Libraries.FileStorage.Local library as per planning
* Implement logic
    * ProductAttachment Concern: Create mediator handlers as per planning & unit tests
	* ProductAttachment Concern: Create controller
* Add validation using FluentValidation
* Add additional swagger documentation for the controllers with supported response types
* Add a global exception handler and ensure correct http status codes are being returned
* Add audit for all api operations
* Add api rate limiting
* Add dockerfile
	* MS Sql Server
	* OnlineStore.App
* Add additional healthchecks
* Add documentation on how the application can scale
	* Move to blob storage/minio and avoid running files through the api when being downloaded
	* Add redis distributed caching
	* Add image/request caching
	* Scale out the application using docker swarm or kubernetes or Azure Container Apps/simila
	* Run infrastructure on cloud so that we can scale up
	* Setup distributed databases with sql server so that we can share the load amongst additional instances
	* There are other options as well
* Add Start Here documentation so that a developer can quickly get up and debugging
* Add technology breakdown

## Optional
* Add authorization for Product & ProductAttachment
* Add PurchaseOrder concern
* Add in memory caching
* Add image & request caching
