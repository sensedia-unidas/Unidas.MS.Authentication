#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Unidas.MS.Authentication.API/Unidas.MS.Authentication.API.csproj", "Unidas.MS.Authentication.API/"]
COPY ["Unidas.MS.Authentication.Application/Unidas.MS.Authentication.Application.csproj", "Unidas.MS.Authentication.Application/"]
COPY ["Unidas.MS.Authentication.Domain/Unidas.MS.Authentication.Domain.csproj", "Unidas.MS.Authentication.Domain/"]
COPY ["Unidas.MS.Authentication.Infra.IoC/Unidas.MS.Authentication.Infra.IoC.csproj", "Unidas.MS.Authentication.Infra.IoC/"]
COPY ["Unidas.MS.Authentication.Infra/Unidas.MS.Authentication.Infra.csproj", "Unidas.MS.Authentication.Infra/"]
RUN dotnet restore "Unidas.MS.Authentication.API/Unidas.MS.Authentication.API.csproj"
COPY . .
WORKDIR "/src/Unidas.MS.Authentication.API"
RUN dotnet build "Unidas.MS.Authentication.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Unidas.MS.Authentication.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Unidas.MS.Authentication.API.dll"]