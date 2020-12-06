# DotNetFullMasterStack
El máster tiene como objetivo el adquirir conocimientos que permitan desarrollar y desplegar aplicaciones de internet utilizando las últimas tendencias del sector. 

En el recorrido del master se han utilizado tecnologías como java, node, AWS,…. El objetivo trabajo fin de máster es el analizar e implementar varias partes del master con tecnologías .net y afines.

A no ser que se indique lo contrario, todas las librerías indicadas en la memoria son open source y son soportadas por .net Foundation.

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

# Herramientas necesarias
1. .NET Core 3.1 https://dotnet.microsoft.com/download
2. Azure CLI  https://docs.microsoft.com/en-us/cli/azure/install-azure-cli
3. Helm 3 https://github.com/helm/helm/releases
4. Docker desktop https://www.docker.com/products/docker-desktop
5. Visual code or visual studio 2019
    - https://code.visualstudio.com/download
    - https://visualstudio.microsoft.com/vs/community/ 

# Proyectos
ASP.NET Web APIs 
- Customer.API
- Kitchen.API
- Ordering.API
- Restaurant.API

ASP.NET Web Apps
- MyBlazorUI

Spring cloud
- admin-service
- config-service
- eureka-service
- gateway-service

Azure pipelines
- build/azure-devops

Deploy az
- deploy/az/aks
- deploy/az/container-instance-vnet

Deploy on AKS
- deploy/k8s/helm

# ASP.NET
ASP.NET es la extensión del framework .NET con herramientas y librerías  para construir aplicaciones y servicios web con .NET y C# para cualquier plataforma (Linux, Windows, macOS…) 

## ASP.NET Web Apps
Permite el desarrollo de aplicaciones web basadas en HTML5, CSS y javascript. Estas aplicaciones son escalables, rápidas y seguras 
## ASP.NET Web APIs
Con ASP.Net Web APIs se pueden desarrollar APIs de una manera sencilla  para diferentes tipos de clientes con un mismo lenguaje. 

# Testing
Una cuestión importante a la hora de desarrollar una aplicación es incluir los test necesarios a fin de asegurarnos que la aplicación funciona correctamente. Dentro del ecosistema .NET podemos utilizar las siguientes librerías xUnit, moq y coverlet.
- xUnit 
- Moq
- Coverlet 
# Persistencia - EntityFramework (EF)
EF es un framework disponible para acceder a diferentes motores de bases de datos (sql y no sql): mysql, postgresql, mongo, firebird… 

# Feature toggle - Esquio
La librería esquio Ha sido desarrollada por varios desarrolladores españoles y permite utilizar feature toggle en una aplicación .NET de manera rápida y sencilla así como en otros servicios como Azure devops.

# ARM (Azure resource manager)
Azure resource manager (ARM) es la solución utilizada por Microsoft para implementar infraestructura como código para las soluciones que utilizan la plataforma Azure. Una plantilla ARM es un JSON (javascript object anotation) que define la infraestructura y configuración necesitaría para crear la infraestructura necesaria para ejecutar una o varias aplicaciones. 

# Azure functions
Permite ejecutar código .net, node, java, power shell o phyton a partir de un evento: Blob storage, Azure cosmos DB, Http, RabbitMQ o Azure queue storage.

Con Azure Functions podemos cubrir diferentes escenarios tales como procesar archivos cuando se almacenan, ejecutar tareas programadas, analizar datos o procesar información sin necesidad de tener un proceso arrancado consumiendo diferentes recursos.
# Microservicios
A día de hoy una de las arquitecturas más utilizadas a la hora de desarrollar aplicaciones en la nube es la arquitectura de microservicios. Se puede definir un microservicio  como un servicio pequeño y autónomo que interactúa  con otros microservicios. 
Uno de los primeros stack de microservicios fue el stack de Netflix y fue adotado por Spring bajo el nombre spring-cloud-netflix. Está compuesto por los siguientes elementos:
-	config-server: servicio que externaliza la toda la configuración de los microservicios
-	eureka: servicio para el registro de microservicios
-	zuul: servicio que actuala como api gateway y que actúa como punto de entrada 
-	hystrix: servicio para la gestión de errors utilizando el patron circuit breaker
  
Steeltoe ofrece una serie de librerías cuyo objetivo es integrarse con los servicios Spring Cloud necesarios en una arquitectura de microservicios
# Tye
Es un proyecto de código abierto y experimental, el objetivo de esta herramienta es el facilitar el desarrollo, testeo y despliegue de microservicios y aplicaciones distribuidas basadas en NetCore. 

## Ejecución
1. dotnet tool install -g Microsoft.Tye --version 0.4.0-alpha.20371.1
2. tye run
3. Open dashboard http://localhost:8000/
4. tye init (it adds a yaml file in order to setup services)
5. Add a new service like 'redis"
6. Open dashboard http://localhost:8000/
7. tye deploy

# Container instances (ACI)
ACI puede arrancar un contenedor en cuestión de segundos utilizando imágenes Linux o Windows desde docker hub o desde un registro de contenedores privado. El acceso a los contenedores puede realizarse a través de una dirección IP o un nombre de dominio, también se puede acceder a la consola del contenedor a fin de revisar o solucionar problemas durante el desarrollo.

# AKS
Azure Kubernetes Service (AKS) facilita el despliegue de un clúster kubernetes como servicio en la nube de Azure reduciendo la complejidad de su administración y facilitando las tareas de mantenimiento. 

# Bridge to kubernetes
Permite redirigir tráfico desde el clúster de kubernetes donde está desplegada nuestra aplicación hasta la máquina del desarrollador, de esta manera podemos desarrollar y depurar un servicio localmente mientras interactúa con el resto de servicios desplegados en el clúster. 

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
| config-service| [![Build Status](https://dev.azure.com/fjvelaaylon/DotNetFullMasterStack/_apis/build/status/Springcloud/config-service?branchName=master)](https://dev.azure.com/fjvelaaylon/DotNetFullMasterStack/_build/latest?definitionId=31&branchName=master)|
| eureka-service | [![Build Status](https://dev.azure.com/fjvelaaylon/DotNetFullMasterStack/_apis/build/status/Springcloud/eureka-service?branchName=master)](https://dev.azure.com/fjvelaaylon/DotNetFullMasterStack/_build/latest?definitionId=32&branchName=master) |
| gateway-service | [![Build Status](https://dev.azure.com/fjvelaaylon/DotNetFullMasterStack/_apis/build/status/Springcloud/eureka-service?branchName=master)](https://dev.azure.com/fjvelaaylon/DotNetFullMasterStack/_build/latest?definitionId=32&branchName=master) |