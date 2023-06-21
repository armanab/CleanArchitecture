# CleanArchitecture
Clean Architecture Solution Template for .NET 6 Web Api


<br/>

This is a solution template for creating Web Api ASP.NET Core following the principles of Clean Architecture.

## Technologies

* ASP.NET Core 6
* [Entity Framework Core 5](https://docs.microsoft.com/en-us/ef/core/)
* [ABluePredicateBuilder](https://www.nuget.org/packages/ABluePredicateBuilder/1.0.1)
* [MediatR](https://github.com/jbogard/MediatR)
* [AutoMapper](https://automapper.org/)
* [FluentValidation](https://fluentvalidation.net/)
* [NUnit](https://nunit.org/)
* [Docker](https://www.docker.com/)

## Getting Started

1. Install the latest [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
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

## Features
- [x] Event sourcing
- [x] CQRS with MediatR Library
- [x] Entity Framework Core - Code First
- [x] MediatR Pipeline Logging & Validation
- [x] Swagger UI
- [x] Response Wrappers
- [x] Healthchecks
- [x] Pagination
- [x] In-Memory Caching
- [ ] Redis Caching
- [x] In-Memory Database
- [x] Microsoft Identity with JWT Authentication
- [x] Role based Authorization
- [x] Identity Seeding
- [x] Database Seeding
- [x] Custom Exception Handling Middlewares
- [x] Fluent Validation
- [x] Automapper
- [x] SMTP / Mailkit / Sendgrid Email Service
- [x] Complete User Management Module (Register / Generate Token / Refresh token /Forgot Password / Confirmation Mail)
- [x] User Auditing

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
### What is ABluePredicateBuilder
it is library  that like ODATA and you can filter or sort on any entity with paging .

With this library, you can write httpGet api to put any filter you want on it and output it in the collection of pagination.

The filters and the face of each are in the form of strings . for example filterString : 
**"CourseId=d1773f09-f17c-4ed0-4e56-08d94509941d¤1µFirstName=Arman¤5µ"**
This string means that the CourseId is equal to d1773f09-f17c-4ed0-4e56-08d94509941d and the firstName Like Arman.



1-The first thing must be the name of the entity property(case sensitive) and next " = " .

2-" ¤ " for seprate VALUE From OprationEnum (after ¤ oprationEnum is placed)


3-" µ " for seprate each filter section .


OperationEnum in the library has the following values:

Equal = 1, NotEqual = 2, Contain = 3, NotContain = 4, Like = 5, NotLike = 6, StartsWith = 7, NotStartsWith = 8, EndsWith = 9, NotEndsWith = 10,

GreaterThan = 11, GreaterThanOrEqual = 12, LessThan = 13, LessThanOrEqual = 14, IsNull = 15, IsNotNull = 16, None = 17, HasFlag = 18, HasnotFlag = 19,

### Give a Star

My primary focus in this project is on quality. Creating a good quality product involves a lot of analysis, research and work. It takes a lot of time. If you like this project, learned something or you are using it in your applications, please give it a star :star:.  This is the best motivation for me to continue this work. Thanks!

### Share It

There are very few really good examples of this type of application. If you think this repository makes a difference and is worth it, please share it with your friends and on social networks. I will be extremely grateful.


## Support

If you are having problems, please let us know by [raising a new issue](https://github.com/armanab/CleanArchitecture/issues/new/choose).

## License

This project is licensed with the [MIT license](LICENSE).
