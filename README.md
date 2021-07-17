# CleanArchitecture
Clean Architecture Solution Template for .NET 5 Web Api


<br/>

This is a solution template for creating Web Api ASP.NET Core following the principles of Clean Architecture.

## Technologies

* ASP.NET Core 5
* [Entity Framework Core 5](https://docs.microsoft.com/en-us/ef/core/)
* [ABluePredicateBuilder](https://www.nuget.org/packages/ABluePredicateBuilder/1.0.1)
* [MediatR](https://github.com/jbogard/MediatR)
* [AutoMapper](https://automapper.org/)
* [FluentValidation](https://fluentvalidation.net/)
* [NUnit](https://nunit.org/)
* [Docker](https://www.docker.com/)

## Getting Started

1. Install the latest [.NET 5 SDK](https://dotnet.microsoft.com/download/dotnet/5.0)
2. Clone the source code.

8. Navigate to `src/Apps/WebApi` and run  (ASP.NET Core Web API)


### Database Configuration

The template is configured to use an in-memory database by default. This ensures that all users will be able to run the solution without needing to set up additional infrastructure (e.g. SQL Server).

If you would like to use SQL Server, you will need to update **WebApi/appsettings.json** as follows:

```json
  "UseInMemoryDatabase": false,
```

Verify that the **DefaultConnection** connection string within **appsettings.json** points to a valid SQL Server instance. 

When you run the application the database will be automatically created (if necessary) and the latest migrations will be applied.

### Database Migrations

To use `dotnet-ef` for your migrations please add the following flags to your command (values assume you are executing from repository root)

* `--project src/Infrastructure` (optional if in this folder)
* `--startup-project src/WebUI`
* `--output-dir Persistence/Migrations`

For example, to add a new migration from the root folder:

 `dotnet ef migrations add "SampleMigration" --project src\Infrastructure --startup-project src\WebUI --output-dir Persistence\Migrations`

## Overview

### Domain

This will contain all entities, enums, exceptions, interfaces, types and logic specific to the domain layer.

### Application

This layer contains all application logic. It is dependent on the domain layer, but has no dependencies on any other layer or project. This layer defines interfaces that are implemented by outside layers. For example, if the application need to access a notification service, a new interface would be added to application and an implementation would be created within infrastructure.

### Infrastructure

This layer contains classes for accessing external resources such as file systems, web services, smtp, and so on. These classes should be based on interfaces defined within the application layer.

### WebApi

This layer is a web api application based on ASP.NET 5.0.x. This layer depends on both the Application and Infrastructure layers, however, the dependency on Infrastructure is only to support dependency injection. Therefore only Startup.cs should reference Infrastructure.

#### Prerequisites
* Download and Install [Docker Desktop](https://www.docker.com/products/docker-desktop)


Open CLI in the project folder and run the below comment. 

```powershell
PS CleanArchitecture> docker-compose up
```
`docker-compose.yml` pull and run the ElasticSearch and Kibana images.

If you are running first time Windows 10 [WSL 2 (Windows Subsystem for Linux)](https://docs.microsoft.com/en-us/windows/wsl/install-win10) Linux Container for Docker, You will probably get the following error from the docker.

`Error:` max virtual memory areas vm.max_map_count [65530] is too low, increase to at least [262144]

`Solution:` Open the Linux WSL 2 terminal `sudo sysctl -w vm.max_map_count=262144` and change the virtual memory for Linux.

## Support

If you are having problems, please let us know by [raising a new issue](https://github.com/armanab/CleanArchitecture/issues/new/choose).

## License

This project is licensed with the [MIT license](LICENSE).
