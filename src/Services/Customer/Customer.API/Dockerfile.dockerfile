#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src

COPY "restaurant-app-microservices.sln" "restaurant-app-microservices.sln"

COPY "BuildingBlocks/WebHostCustomization/WebHostCustomization/WebHostCustomization.csproj" "BuildingBlocks/WebHostCustomization/WebHostCustomization/WebHostCustomization.csproj"

COPY "Services/Customer/Customer.API/Customer.API.csproj"                                   "Services/Customer/Customer.API/Customer.API.csproj"
COPY "Services/Customer/Customer.FunctionalTests/Customer.FunctionalTests.csproj"           "Services/Customer/Customer.FunctionalTests/Customer.FunctionalTests.csproj"
COPY "Services/Customer/Customer.UnitTest/Customer.UnitTest.csproj"                         "Services/Customer/Customer.UnitTest/Customer.UnitTest.csproj"

COPY "Services/Kitchen/Kitchen.API/Kitchen.API.csproj"                                      "Services/Kitchen/Kitchen.API/Kitchen.API.csproj"
COPY "Services/Kitchen/Kitchen.FunctionalTests/Kitchen.FunctionalTests.csproj"           "Services/Kitchen/Kitchen.FunctionalTests/Kitchen.FunctionalTests.csproj"
COPY "Services/Kitchen/Kitchen.UnitTest/Kitchen.UnitTest.csproj"                         "Services/Kitchen/Kitchen.UnitTest/Kitchen.UnitTest.csproj"

COPY "Services/Ordering/Ordering.API/Ordering.API.csproj"                                   "Services/Ordering/Ordering.API/Ordering.API.csproj"
COPY "Services/Ordering/Ordering.FunctionalTests/Ordering.FunctionalTests.csproj"           "Services/Ordering/Ordering.FunctionalTests/Ordering.FunctionalTests.csproj"
COPY "Services/Ordering/Ordering.UnitTest/Ordering.UnitTest.csproj"                         "Services/Ordering/Ordering.UnitTest/Ordering.UnitTest.csproj"

COPY "Services/Restaurant/Restaurant.API/Restaurant.API.csproj"                             "Services/Restaurant/Restaurant.API/Restaurant.API.csproj"
COPY "Services/Restaurant/Restaurant.FunctionalTests/Restaurant.FunctionalTests.csproj"           "Services/Restaurant/Restaurant.FunctionalTests/Restaurant.FunctionalTests.csproj"
COPY "Services/Restaurant/Restaurant.UnitTest/Restaurant.UnitTest.csproj"                         "Services/Restaurant/Restaurant.UnitTest/Restaurant.UnitTest.csproj"

COPY "docker-compose.dcproj" "docker-compose.dcproj"


RUN dotnet restore "restaurant-app-microservices.sln"

COPY . .
WORKDIR "/src/Services/Customer/Customer.API"
RUN dotnet build "Customer.API.csproj" -c Release -o /app/build

FROM build as unittest
WORKDIR /src/Services/Customer/Customer.UnitTest

FROM build as functionaltest
WORKDIR /src/Services/Customer/Customer.FunctionalTests

FROM build AS publish
RUN dotnet publish "Customer.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Customer.API.dll"]