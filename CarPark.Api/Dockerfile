# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CarPark.Api/CarPark.Api.csproj", "CarPark.Api/"]
COPY ["CarPark.Application/CarPark.Application.csproj", "CarPark.Application/"]
COPY ["CarPark.Domain/CarPark.Domain.csproj", "CarPark.Domain/"]
COPY ["CarPark.Contracts/CarPark.Contracts.csproj", "CarPark.Contracts/"]
COPY ["CarPark.Infrastructure.Persistence/CarPark.Infrastructure.Persistence.csproj", "CarPark.Infrastructure.Persistence/"]
COPY ["CarPark.Infrastructure/CarPark.Infrastructure.csproj", "CarPark.Infrastructure/"]
RUN dotnet restore "./CarPark.Api/CarPark.Api.csproj"
COPY . .
WORKDIR "/src/CarPark.Api"
RUN dotnet build "./CarPark.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CarPark.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CarPark.Api.dll"]