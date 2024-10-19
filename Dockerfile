#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["WebApiForHikka/WebApiForHikka.WebApi.csproj", "WebApiForHikka/"]
COPY ["WebApiForHikka.Application/WebApiForHikka.Application.csproj", "WebApiForHikka.Application/"]
COPY ["WebApiForHikka.Constants/WebApiForHikka.Constants.csproj", "WebApiForHikka.Constants/"]
COPY ["WebApiForHikka.Domain/WebApiForHikka.Domain.csproj", "WebApiForHikka.Domain/"]
COPY ["WebApiForHikka.SharedFunction/WebApiForHikka.SharedFunction.csproj", "WebApiForHikka.SharedFunction/"]
COPY ["WebApiForHikka.Dtos/WebApiForHikka.Dtos.csproj", "WebApiForHikka.Dtos/"]
COPY ["WebApiForHikka.EfPersistence/WebApiForHikka.EfPersistence.csproj", "WebApiForHikka.EfPersistence/"]
RUN dotnet restore "./WebApiForHikka/WebApiForHikka.WebApi.csproj"
COPY . .
WORKDIR "/src/WebApiForHikka"
RUN dotnet build "./WebApiForHikka.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./WebApiForHikka.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApiForHikka.WebApi.dll"]
