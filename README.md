# CarPark API

A simple **Car Park Management API** built with **.NET 8**, following **Clean Architecture** principles.  
Implements allocation, exit, and capacity tracking logic for a parking lot - backed by **PostgreSQL** via **EF Core**.


##  Overview

The system allows to:
- Allocate vehicles to the **first available space**  
- Retrieve the **number of available and occupied spaces**  
- Handle **vehicle exit** and compute parking **charges** based on type and duration  
- Apply **extra £1 fee per every 5 minutes**


## Architecture

Project follows **Clean Architecture** (Domain -> Application -> Infrastructure -> API):

| Layer | Description |
|-------|--------------|
| **Domain** | Entities, Enums, Pricing policy ('IPricingPolicy') |
| **Application** | Use-cases ('AllocateVehicle', 'ExitVehicle', 'GetCapacity') implemented with **MediatR** |
| **Contracts** | DTOs (Requests / Responses) exposed by the API; serve as the boundary between API and Application |
| **Infrastructure.Persistence** | EF Core + PostgreSQL DbContext, entity configurations, repositories |
| **Infrastructure** | Configuration, dependency injection, and cross-cutting integrations |
| **API** | ASP.NET Core Web API layer — controllers, dependency registration, middleware, Swagger |
| **Tests** | Unit & integration tests (Domain, Application, Persistence, API) using **xUnit**, **FluentAssertions**, **Moq** |

## Environment Variables

This project uses environment variables to configure database credentials, ports, and API settings.

All environment variables are defined in a local '.env' file.  
For security reasons, this file is **not committed** to the repository.

An example configuration file **'.env.example'** is included in the project root.  

## Tech Stack

- **.NET 8.0**
- **ASP.NET Core Web API**
- **Entity Framework Core (Npgsql)**
- **PostgreSQL**
- **MediatR + FluentValidation**
- **Docker Compose**