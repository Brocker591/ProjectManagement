#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80


RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/User/UserApi/UserApi.csproj", "Services/User/UserApi/"]
COPY ["Infra/Common/Common.csproj", "Infra/Common/"]
RUN dotnet restore "./Services/User/UserApi/UserApi.csproj"
COPY . .
WORKDIR "/src/Services/User/UserApi"
RUN dotnet build "./UserApi.csproj" -c Release -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./UserApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserApi.dll"]