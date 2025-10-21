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
| **Infrastructure.Persistence** | EF Core + PostgreSQL DbContext, entity configurations, repositories |
| **API** | ASP.NET Core Web API + global exception middleware |
| **Tests** | Unit & integration tests (Domain, Application, Persistence, API) using **xUnit**, **FluentAssertions**, **Moq** |


## Tech Stack

- **.NET 8.0**
- **ASP.NET Core Web API**
- **Entity Framework Core (Npgsql)**
- **PostgreSQL**
- **MediatR + FluentValidation**
- **Docker Compose**