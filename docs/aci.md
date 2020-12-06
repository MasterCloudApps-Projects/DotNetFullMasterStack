# Container instances (ACI)
ACI puede arrancar un contenedor en cuestión de segundos utilizando imágenes Linux o Windows desde docker hub o desde un registro de contenedores privado. El acceso a los contenedores puede realizarse a través de una dirección IP o un nombre de dominio, también se puede acceder a la consola del contenedor a fin de revisar o solucionar problemas durante el desarrollo.

## Herramientas necesarias
1. .NET Core 3.1 https://dotnet.microsoft.com/download
2. Azure CLI  https://docs.microsoft.com/en-us/cli/azure/install-azure-cli
3. Docker desktop https://www.docker.com/products/docker-desktop
4. Visual code or visual studio 2019
    - https://code.visualstudio.com/download
    - https://visualstudio.microsoft.com/vs/community/ 


## Despliegue con Azure CLI
Debes estar logeado correctamente en tu suscripción azure

1. Crear grupo recursos:
    ```
    az group create --name dotnetfullmasterstack --location westeurope
    ```
2. Ejecutar (deploy/az/):
    ```
    ./createresources.sh container-instance/container-instance dotnetfullmasterstack -c westeurope
    ```