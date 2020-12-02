#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src

COPY ["TrueLayer.Services.Tests/TrueLayer.Services.Tests.csproj", "TrueLayer.Services.Tests/"]
RUN dotnet restore "TrueLayer.Services.Tests/TrueLayer.Services.Tests.csproj"
COPY ["TrueLayer.WebApi/TrueLayer.WebApi.csproj", "TrueLayer.WebApi/"]
RUN dotnet restore "TrueLayer.WebApi/TrueLayer.WebApi.csproj"
COPY . .
WORKDIR "/src/TrueLayer.Services.Tests"
RUN dotnet build "TrueLayer.Services.Tests.csproj"
WORKDIR "/src/TrueLayer.WebApi"
RUN dotnet build "TrueLayer.WebApi.csproj" -c Release -o /app/build

FROM build AS test
WORKDIR "/src/TrueLayer.Services.Tests/"
RUN dotnet test

FROM build AS publish
WORKDIR "/src/TrueLayer.WebApi/" 
RUN dotnet publish "TrueLayer.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TrueLayer.WebApi.dll"]