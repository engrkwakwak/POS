FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Directory.Build.props", "."]
COPY ["Directory.Packages.props", "."]
COPY ["src/POS.Api/POS.Api.csproj", "src/POS.Api/"]
COPY ["src/POS.Infrastructure/POS.Infrastructure.csproj", "src/POS.Infrastructure/"]
COPY ["src/POS.Application/POS.Application.csproj", "src/POS.Application/"]
COPY ["src/POS.SharedKernel/POS.SharedKernel.csproj", "src/POS.SharedKernel/"]
RUN dotnet restore "./src/POS.Api/POS.Api.csproj"
COPY . .
WORKDIR "/src/src/POS.Api"
RUN dotnet build "./POS.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./POS.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "POS.Api.dll"]