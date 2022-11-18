#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MsSensediaTemplate.API/MsSensediaTemplate.API.csproj", "MsSensediaTemplate.API/"]
COPY ["MsSensediaTemplate.Application/MsSensediaTemplate.Application.csproj", "MsSensediaTemplate.Application/"]
COPY ["MsSensediaTemplate.Domain/MsSensediaTemplate.Domain.csproj", "MsSensediaTemplate.Domain/"]
COPY ["MsSensediaTemplate.Infra.IoC/MsSensediaTemplate.Infra.IoC.csproj", "MsSensediaTemplate.Infra.IoC/"]
COPY ["MsSensediaTemplate.Infra/MsSensediaTemplate.Infra.csproj", "MsSensediaTemplate.Infra/"]
RUN dotnet restore "MsSensediaTemplate.API/MsSensediaTemplate.API.csproj"
COPY . .
WORKDIR "/src/MsSensediaTemplate.API"
RUN dotnet build "MsSensediaTemplate.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MsSensediaTemplate.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MsSensediaTemplate.API.dll"]