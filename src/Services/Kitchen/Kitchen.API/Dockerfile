#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src

COPY "restaurant-app-microservices.sln" "restaurant-app-microservices.sln"

COPY "BuildingBlocks/WebHostCustomization/WebHostCustomization/WebHostCustomization.csproj" "BuildingBlocks/WebHostCustomization/WebHostCustomization/WebHostCustomization.csproj"

COPY "Services/Kitchen/Kitchen.API/Kitchen.API.csproj"                                      "Services/Kitchen/Kitchen.API/Kitchen.API.csproj"
COPY "Services/Kitchen/Kitchen.FunctionalTests/Kitchen.FunctionalTests.csproj"				"Services/Kitchen/Kitchen.FunctionalTests/Kitchen.FunctionalTests.csproj"
COPY "Services/Kitchen/Kitchen.UnitTest/Kitchen.UnitTest.csproj"							"Services/Kitchen/Kitchen.UnitTest/Kitchen.UnitTest.csproj"


COPY "docker-compose.dcproj" "docker-compose.dcproj"

RUN dotnet restore "Services/Kitchen/Kitchen.API/Kitchen.API.csproj"

COPY . .
WORKDIR "/src/Services/Kitchen/Kitchen.API"
RUN dotnet build "Kitchen.API.csproj" -c Release -o /app/build

FROM build as unittest
WORKDIR /src/Services/Kitchen/Kitchen.UnitTest

FROM build as functionaltest
WORKDIR /src/Services/Kitchen/Kitchen.FunctionalTests

FROM build AS publish
RUN dotnet publish "Kitchen.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kitchen.API.dll"]