# Brewery Code Challenge

## Running the aplication
Install [.NET Core 2.2](https://dotnet.microsoft.com/download/dotnet-core/2.2). Then, open the terminal or command prompt at the API root path (```/PragmaBrewery.API/```) and run the following commands, in sequence:
```
dotnet restore
dotnet run
```
Navigate to ```http://localhost:5000/beers/containers-temperatures``` to check if the API is working. If you see a HTTPS security error, just add an exception to see the results.

To test all endpoints, you need to use a software such as [Postman](https://www.getpostman.com/). If you use Postman be sure to go to ```Settings``` and disable the options ```SSL certificate verification``` in ```General``` tab, ```Global Proxy Configuration``` and ```Use System Proxy``` in ```Proxy``` tab.

## Frameworks and Libraries
- [ASP.NET Core 2.2](https://docs.microsoft.com/pt-br/aspnet/core/?view=aspnetcore-2.2)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/) 
- [Entity Framework In-Memory Provider](https://docs.microsoft.com/en-us/ef/core/miscellaneous/testing/in-memory)
- [xUnit](https://xunit.net)
- [Fluent Assertions](https://fluentassertions.com/documentation)

## Questions and answers/assumptions
- How are beers transported?
  There is one container for each type of beer, so each type is transported in its specific container.
- Is it necessary to add beers and their containers?
  In the first moment is not necessary, because the beers and the containers are specific. In the future CRUDs would be interesting.

## Upcoming improvements
- Implement exception handling with Middleware
- Add domain validations
- Use Autofac to manage dependencies
- Implement Mapping with IEntityTypeConfiguration in DbContext
