# DotNetFullMasterStack
Todas las partes del máster implementadas con tecnologías -Net y afines

##  Build Status
| Image | Status |
| ------------- | ------------- |
| Customer API | [![Build Status](https://dev.azure.com/fjvelaaylon/DotNetFullMasterStack/_apis/build/status/microservices/customer-api?branchName=master)](https://dev.azure.com/fjvelaaylon/DotNetFullMasterStack/_build/latest?definitionId=20&branchName=master) |
| Kitchen API | [![Build Status](https://dev.azure.com/fjvelaaylon/DotNetFullMasterStack/_apis/build/status/microservices/kitchen-api?branchName=master)](https://dev.azure.com/fjvelaaylon/DotNetFullMasterStack/_build/latest?definitionId=23&branchName=master) |
| Ordering API | [![Build Status](https://dev.azure.com/fjvelaaylon/DotNetFullMasterStack/_apis/build/status/microservices/ordering-api?branchName=master)](https://dev.azure.com/fjvelaaylon/DotNetFullMasterStack/_build/latest?definitionId=24&branchName=master) |
| Resturant API | [![Build Status](https://dev.azure.com/fjvelaaylon/DotNetFullMasterStack/_apis/build/status/microservices/restaurant-api?branchName=master)](https://dev.azure.com/fjvelaaylon/DotNetFullMasterStack/_build/latest?definitionId=25&branchName=master) |

# Table of Contents
1. [Tye](#Tye)
   - [Steps](###Steps)
   - [References](###References)


## Tye
Tye is a developer tool that makes developing, testing, and deploying microservices and distributed applications easier. Project Tye includes a local orchestrator to make developing microservices easier and the ability to deploy microservices to Kubernetes with minimal configuration.

### Steps
1. dotnet tool install -g Microsoft.Tye --version 0.4.0-alpha.20371.1
2. tye run
   1. Open dashboard http://localhost:8000/
3. tye init (it adds a yaml file in order to setup services)
   1. Add a new service like 'redis"
   2. Open dashboard http://localhost:8000/
4. tye deploy 

### References 
- https://devblogs.microsoft.com/aspnet/introducing-project-tye/
- https://github.com/dotnet/tye/
