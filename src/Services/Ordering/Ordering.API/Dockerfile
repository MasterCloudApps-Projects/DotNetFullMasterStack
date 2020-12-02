#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src

COPY "restaurant-app-microservices.sln" "restaurant-app-microservices.sln"

COPY "BuildingBlocks/WebHostCustomization/WebHostCustomization/WebHostCustomization.csproj" "BuildingBlocks/WebHostCustomization/WebHostCustomization/WebHostCustomization.csproj"

COPY "Services/Ordering/Ordering.API/Ordering.API.csproj"                                   "Services/Ordering/Ordering.API/Ordering.API.csproj"
COPY "Services/Ordering/Ordering.FunctionalTests/Ordering.FunctionalTests.csproj"           "Services/Ordering/Ordering.FunctionalTests/Ordering.FunctionalTests.csproj"
COPY "Services/Ordering/Ordering.UnitTest/Ordering.UnitTest.csproj"                         "Services/Ordering/Ordering.UnitTest/Ordering.UnitTest.csproj"


COPY "docker-compose.dcproj" "docker-compose.dcproj"


RUN dotnet restore "Services/Ordering/Ordering.API/Ordering.API.csproj"

COPY . .
WORKDIR "/src/Services/Ordering/Ordering.API"
RUN dotnet build "Ordering.API.csproj" -c Release -o /app/build

FROM build as unittest
WORKDIR /src/Services/Ordering/Ordering.UnitTest

FROM build as functionaltest
WORKDIR /src/Services/Ordering/Ordering.FunctionalTests

FROM build AS publish
RUN dotnet publish "Ordering.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ordering.API.dll"]