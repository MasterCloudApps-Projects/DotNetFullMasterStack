#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["UI/MyBlazorUI/MyBlazorUI.csproj", "UI/MyBlazorUI/"]
RUN dotnet restore "UI/MyBlazorUI/MyBlazorUI.csproj"
COPY . .
WORKDIR "/src/UI/MyBlazorUI"
RUN dotnet build "MyBlazorUI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyBlazorUI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyBlazorUI.dll"]