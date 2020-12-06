# DotNetFullMasterStack
El máster tiene como objetivo el adquirir conocimientos que permitan desarrollar y desplegar aplicaciones de internet utilizando las últimas tendencias del sector. 

- [Introducción](#introducción)
- [Herramientas necesarias](#herramientas-necesarias)
- [ASP.NET](#aspnet)
  * [ASP.NET Web Apps](#aspnet-web-apps)
  * [ASP.NET Web APIs](#aspnet-web-apis)
- [Testing](#testing)
- [Persistencia - EntityFramework (EF)](#persistencia---entityframework--ef-)
- [Feature toggle - Esquio](#feature-toggle---esquio)
- [ARM (Azure resource manager)](#arm--azure-resource-manager-)
- [Azure functions](#azure-functions)
- [Microservicios](#microservicios)
- [Tye](#tye)
- [Container instances (ACI)](#container-instances--aci-)
- [AKS](#aks)
- [Bridge to kubernetes](#bridge-to-kubernetes)
- [Integración y despliegue continuo](#integraci-n-y-despliegue-continuo)

# Introducción
En el recorrido del master se han utilizado tecnologías como java, node, AWS,…. El objetivo trabajo fin de máster es el analizar e implementar varias partes del master con tecnologías .net y afines.

A no ser que se indique lo contrario, todas las librerías indicadas en la memoria son open source y son soportadas por [.net Foundation.](https://dotnetfoundation.org/)

Se han desarrollado cuatro microservicios: customer, kitchen, ordering y restaurant utilizando [ASP.NET Web APIs](https://dotnet.microsoft.com/apps/aspnet/apis). En todos se han incluido unit test y test funcionales con [xUnit](https://xunit.net/), [Moq](https://github.com/Moq/moq4/wiki/Quickstart) y [Coverlet](https://github.com/coverlet-coverage/coverlet). Con [entity framework](https://docs.microsoft.com/en-us/ef/) estos microservicios leen y almacenaman información en un servidor de base de datos [SQL server](https://www.microsoft.com/sql-server)

Los micros se han integrado con el stack [Spring Cloud Netflix](https://spring.io/projects/spring-cloud-netflix) haciendo uso de la libreria [Steeltoe](https://steeltoe.io/), por último se ha desarrollado una aplicación web basada en [blazor](https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor)  

Para el desarrollo y depuración de la aplicación se ha utilizado vistual studio, también existe la posibilidad de ejecutar los proyectos desde línea de comandos.

Para ejecutar el microservicio Customer.API desde la línea de comandos API (desde src):
  ```
  dotnet run -p Services/Customer/Customer.API
  ```
  En la URL http://localhost:5000 se puede acceder a la aplicacion

Ejecución toda la solución con docker dompose (desde src/)
  ```
  docker-compose -f docker-compose.yml -f docker-compose.override.yml up –build
  ```
  Las URLs de la aplicacion son las siguientes: 
  - UI: http://localhost:8080/
  - admin-service: http://localhost:8889/wallboard

Ejecución suite tests (desde src/):
```
docker-compose -f docker-compose-tests.yml -f docker-compose-tests.override.yml up –build
```
# Herramientas necesarias
1. .NET Core 3.1 https://dotnet.microsoft.com/download
2. Azure CLI  https://docs.microsoft.com/en-us/cli/azure/install-azure-cli
3. Azure Functions Core Tools https://docs.microsoft.com/en-us/azure/azure-functions/functions-run-local
4. Helm 3 https://github.com/helm/helm/releases
5. Docker desktop https://www.docker.com/products/docker-desktop
6. Visual code or visual studio 2019
    - https://code.visualstudio.com/download
    - https://visualstudio.microsoft.com/vs/community/ 
7. Type 0.4.0-alpha.20371.1 (dotnet tool install -g Microsoft.Tye --version 0.4.0-alpha.20371.1)

# ASP.NET
ASP.NET es la extensión del framework .NET con herramientas y librerías  para construir aplicaciones y servicios web con .NET y C# para cualquier plataforma (Linux, Windows, macOS…) 

## ASP.NET Web Apps
Permite el desarrollo de aplicaciones web basadas en HTML5, CSS y javascript. Estas aplicaciones son escalables, rápidas y seguras 
## ASP.NET Web APIs
Con ASP.Net Web APIs se pueden desarrollar APIs de una manera sencilla  para diferentes tipos de clientes con un mismo lenguaje. 

# Testing
Una cuestión importante a la hora de desarrollar una aplicación es incluir los test necesarios a fin de asegurarnos que la aplicación funciona correctamente. Dentro del ecosistema .NET podemos utilizar las siguientes librerías xUnit, moq y coverlet.
- [xUnit](https://xunit.net/)
- [Moq](https://github.com/Moq/moq4/wiki/Quickstart) 
- [Coverlet](https://github.com/coverlet-coverage/coverlet) 
# Persistencia - Entity Framework (EF)
[Entity Framework](https://docs.microsoft.com/en-us/ef/) es un framework disponible para acceder a diferentes motores de bases de datos (sql y no sql): mysql, postgresql, mongo, firebird… 

# Feature toggle - Esquio
La librería [esquio](https://github.com/Xabaril/Esquio) Ha sido desarrollada por varios desarrolladores españoles y permite utilizar feature toggle en una aplicación .NET de manera rápida y sencilla así como en otros servicios como Azure devops.

# ARM (Azure resource manager)
[Azure resource manager (ARM)](https://docs.microsoft.com/en-us/azure/azure-resource-manager/) es la solución utilizada por Microsoft para implementar infraestructura como código para las soluciones que utilizan la plataforma Azure. Una plantilla ARM es un JSON (javascript object anotation) que define la infraestructura y configuración necesitaría para crear la infraestructura necesaria para ejecutar una o varias aplicaciones. 

# Azure functions
[Azure functions](https://azure.microsoft.com/en-us/services/functions) permite ejecutar código .net, node, java, power shell o phyton a partir de un evento: Blob storage, Azure cosmos DB, Http, RabbitMQ o Azure queue storage. Más información, [aqui](./docs/azure-functions.md#azure-functions)

Con Azure Functions podemos cubrir diferentes escenarios tales como procesar archivos cuando se almacenan, ejecutar tareas programadas, analizar datos o procesar información sin necesidad de tener un proceso arrancado consumiendo diferentes recursos.
# Microservicios
A día de hoy una de las arquitecturas más utilizadas a la hora de desarrollar aplicaciones en la nube es la arquitectura de microservicios. Se puede definir un microservicio  como un servicio pequeño y autónomo que interactúa  con otros microservicios. 
Uno de los primeros stack de microservicios fue el stack de Netflix y fue adotado por Spring bajo el nombre [spring-cloud-netflix](https://spring.io/projects/spring-cloud-netflix). Está compuesto por los siguientes elementos:
-	config-server: servicio que externaliza la toda la configuración de los microservicios
-	eureka: servicio para el registro de microservicios
-	zuul: servicio que actuala como api gateway y que actúa como punto de entrada 
-	hystrix: servicio para la gestión de errors utilizando el patron circuit breaker
  
[Steeltoe](https://steeltoe.io/) ofrece una serie de librerías cuyo objetivo es integrarse con los servicios Spring Cloud necesarios en una arquitectura de microservicios
# Tye
Es un proyecto de código abierto y experimental, el objetivo de esta herramienta es el facilitar el desarrollo, testeo y despliegue de microservicios y aplicaciones distribuidas basadas en NetCore. Más información, [aqui](./docs/type.md#Tye)

# Container instances (ACI)
[Azure container instances](https://azure.microsoft.com/en-us/services/container-instances/) puede arrancar un contenedor en cuestión de segundos utilizando imágenes Linux o Windows desde docker hub o desde un registro de contenedores privado. El acceso a los contenedores puede realizarse a través de una dirección IP o un nombre de dominio, también se puede acceder a la consola del contenedor a fin de revisar o solucionar problemas durante el desarrollo. Más información, [aqui](./docs/aci.md#ACI)

# AKS
[Azure Kubernetes Service (AKS)](https://docs.microsoft.com/en-us/azure/aks/) facilita el despliegue de un clúster kubernetes como servicio en la nube de Azure reduciendo la complejidad de su administración y facilitando las tareas de mantenimiento.  Más información, [aqui](./docs/aks.md#AKS)


# Bridge to kubernetes
[Bridge to kubernetes](https://github.com/microsoft/vscode-docs/blob/master/docs/containers/bridge-to-kubernetes.md) Permite redirigir tráfico desde el clúster de kubernetes donde está desplegada nuestra aplicación hasta la máquina del desarrollador, de esta manera podemos desarrollar y depurar un servicio localmente mientras interactúa con el resto de servicios desplegados en el clúster. 

# Integración y despliegue continuo


##  Build Status
| Image | Status |
| ------------- | ------------- |
| Customer API | [![Build Status](https://dev.azure.com/fjvelaaylon/DotNetFullMasterStack/_apis/build/status/microservices/customer-api?branchName=master)](https://dev.azure.com/fjvelaaylon/DotNetFullMasterStack/_build/latest?definitionId=20&branchName=master) |
| Kitchen API | [![Build Status](https://dev.azure.com/fjvelaaylon/DotNetFullMasterStack/_apis/build/status/microservices/kitchen-api?branchName=master)](https://dev.azure.com/fjvelaaylon/DotNetFullMasterStack/_build/latest?definitionId=23&branchName=master) |
| Ordering API | [![Build Status](https://dev.azure.com/fjvelaaylon/DotNetFullMasterStack/_apis/build/status/microservices/ordering-api?branchName=master)](https://dev.azure.com/fjvelaaylon/DotNetFullMasterStack/_build/latest?definitionId=24&branchName=master) |
| Resturant API | [![Build Status](https://dev.azure.com/fjvelaaylon/DotNetFullMasterStack/_apis/build/status/microservices/restaurant-api?branchName=master)](https://dev.azure.com/fjvelaaylon/DotNetFullMasterStack/_build/latest?definitionId=25&branchName=master) |
| UI ASP.NET blazor | [![Build Status](https://dev.azure.com/fjvelaaylon/DotNetFullMasterStack/_apis/build/status/ui-aspnet-blazor?branchName=master)](https://dev.azure.com/fjvelaaylon/DotNetFullMasterStack/_build/latest?definitionId=34&branchName=master) |
| Infra artefacts | [![Build Status](https://dev.azure.com/fjvelaaylon/DotNetFullMasterStack/_apis/build/status/infra%20artefacts?branchName=master)](https://dev.azure.com/fjvelaaylon/DotNetFullMasterStack/_build/latest?definitionId=35&branchName=master) |
| Notificacion Azure funtions | [![Build Status](https://dev.azure.com/fjvelaaylon/DotNetFullMasterStack/_apis/build/status/Notification%20Azure%20Functions?branchName=master)](https://dev.azure.com/fjvelaaylon/DotNetFullMasterStack/_build/latest?definitionId=29&branchName=master) |
| admin-service | [![Build Status](https://dev.azure.com/fjvelaaylon/DotNetFullMasterStack/_apis/build/status/Springcloud/admin-service?branchName=master)](https://dev.azure.com/fjvelaaylon/DotNetFullMasterStack/_build/latest?definitionId=30&branchName=master)
| config-service | [![Build Status](https://dev.azure.com/fjvelaaylon/DotNetFullMasterStack/_apis/build/status/Springcloud/config-service?branchName=master)](https://dev.azure.com/fjvelaaylon/DotNetFullMasterStack/_build/latest?definitionId=31&branchName=master)|
| eureka-service | [![Build Status](https://dev.azure.com/fjvelaaylon/DotNetFullMasterStack/_apis/build/status/Springcloud/eureka-service?branchName=master)](https://dev.azure.com/fjvelaaylon/DotNetFullMasterStack/_build/latest?definitionId=32&branchName=master) |
| gateway-service | [![Build Status](https://dev.azure.com/fjvelaaylon/DotNetFullMasterStack/_apis/build/status/Springcloud/eureka-service?branchName=master)](https://dev.azure.com/fjvelaaylon/DotNetFullMasterStack/_build/latest?definitionId=32&branchName=master) |