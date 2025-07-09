FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["NewsPortal.API/NewsPortal.API.csproj", "NewsPortal.API/"]
COPY ["NewsPortal.Application/NewsPortal.Application.csproj", "NewsPortal.Application/"]
COPY ["NewsPortal.Domain/NewsPortal.Domain.csproj", "NewsPortal.Domain/"]
COPY ["NewsPortal.Infrastructure/NewsPortal.Infrastructure.csproj", "NewsPortal.Infrastructure/"]
RUN dotnet restore "./NewsPortal.API/NewsPortal.API.csproj"
COPY . .
WORKDIR "/src/NewsPortal.API"
RUN dotnet build "./NewsPortal.API.csproj" -c %BUILD_CONFIGURATION% -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./NewsPortal.API.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NewsPortal.API.dll"]