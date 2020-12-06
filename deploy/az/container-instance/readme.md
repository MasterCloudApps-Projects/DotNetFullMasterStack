./createresources.sh container-instance/container-instance tfm -c westeurope


# Deploy with docker compose
1.- az group create --name tfm --location westeurope
2.- az acr create --resource-group tfm --name dotnetfullmasterstack --sku Basic


docker context create aci myacicontext