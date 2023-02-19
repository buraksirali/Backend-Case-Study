# Backend Case Study
This is a case study project I have written for a company's admission.

# Required SDKs and Build Information
- .Net 6.0 SDK will be required to download and build this project.
- After installing .net 6.0 and running the project, dotnet restore --no-cache should be done for required nuget packages to install.
- For the project to properly work, a PostgreSQL database connectionstring should be written into appsettings.json
- Then migrations should be done to the database. "dotnet ef database update" command should suffice.

# Tests
- Postman collection is included in the repository's root folder.
- Swagger UI can also be used to test endpoints.

# API Endpoints
- POST requests to;
  * ip:port/Share/buy
  * ip:port/Share/sell
- endpoints are available. (Default hosting is https://localhost:7046)
- Both endpoints require 3 parameters which are like below;
{ 
  ShareSymbol: string,
  userId: integer,
  Volume: integer,
}
- Each time service is built, example data are put into the database to test with. Information about inserted data is included in an Excel Sheet in repository's root folder.

# Tech-stack
- ASP.Net 6.0
- NSwag 13.18.2
- Swagger OpenAPI 3.0.3
- EntityFrameworkCore 7.0.3
- Npgsql.EntityFrameworkCore.PostgreSQL 7.0.3

If there are any questions you have, please reach me at buraksirali.dev@gmail.com
