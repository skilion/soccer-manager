# Soccer Manager REST API

## Compilation

To compile the project you can use either [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) (recommended) or [Visual Studio Code](https://code.visualstudio.com/) with the [C# extension](https://code.visualstudio.com/Docs/languages/csharp).

## API Reference

For a reference of the API endpoints, run the project in Debug mode and open `/swagger` for the [Swagger UI](https://swagger.io/) or `/swagger/v1/swagger.json` for the [OpenAPI](https://www.openapis.org/) specification.

For convenience a pre-generated version is included [here](api/README.md).

## Deployment

Edit the file `SoccerManager/appsettings.json` to set your unique [JWT](https://jwt.io/) sign key.

To deploy the service you can follow these [instructions](https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/?view=aspnetcore-6.0).

Note that by default the service is configured to use [SQLite](https://sqlite.org/index.html). Therefore the host environment needs persistent writable storage attached.
