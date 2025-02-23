#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80

#RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
#USER appuser

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src
COPY ["Services/Project/ProjectApi/ProjectApi.csproj", "Services/Project/ProjectApi/"]
COPY ["Infra/Common/Common.csproj", "Infra/Common/"]
COPY ["Services/Project/ProjectApplication/ProjectApplication.csproj", "Services/Project/ProjectApplication/"]
COPY ["Services/Project/ProjectDomain/ProjectDomain.csproj", "Services/Project/ProjectDomain/"]
COPY ["Services/Project/ProjectInfrastructure/ProjectInfrastructure.csproj", "Services/Project/ProjectInfrastructure/"]
RUN dotnet restore "./Services/Project/ProjectApi/ProjectApi.csproj"
COPY . .
WORKDIR "/src/Services/Project/ProjectApi"
RUN dotnet build "./ProjectApi.csproj" -c Release -o /app/build

FROM build AS publish

RUN dotnet publish "./ProjectApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProjectApi.dll"]