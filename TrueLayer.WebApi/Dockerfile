#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src

COPY [".", "."]
WORKDIR "/src/TrueLayer.WebApi"
RUN dotnet restore "TrueLayer.WebApi.csproj"
RUN dotnet publish "TrueLayer.WebApi.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS release
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 5000
ENV ASPNETCORE_URLS http://+:5000
ENV ASPNETCORE_ENVIRONMENT="production"
ENTRYPOINT ["dotnet", "TrueLayer.WebApi.dll"]