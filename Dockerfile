FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 51722

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["SBEU.Gramm/SBEU.Gramm.Api/SBEU.Gramm.Api.csproj", "SBEU.Gramm/SBEU.Gramm.Api/"]
COPY ["SBEU.Gramm.Middleware/SBEU.Gramm.Middleware.csproj", "SBEU.Gramm.Middleware/"]
COPY ["SBEU.Gramm/SBEU.Gramm.DataLayer/SBEU.Gramm.DataLayer.csproj", "SBEU.Gramm/SBEU.Gramm.DataLayer/"]
COPY ["SBEU.Gramm/SBEU.Gramm.Models/SBEU.Gramm.Models.csproj", "SBEU.Gramm/SBEU.Gramm.Models/"]
RUN dotnet restore "SBEU.Gramm/SBEU.Gramm.Api/SBEU.Gramm.Api.csproj"
COPY . .
WORKDIR "/src/SBEU.Gramm/SBEU.Gramm.Api"
RUN dotnet build "SBEU.Gramm.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SBEU.Gramm.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

RUN apt-get update && apt-get install -y \
    imagemagick libmagickwand-dev --no-install-recommends

ENTRYPOINT ["dotnet", "SBEU.Gramm.Api.dll"]
