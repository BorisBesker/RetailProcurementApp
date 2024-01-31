FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["RetailProcurementApp/RetailProcurementApp.csproj", "RetailProcurementApp/"]
RUN dotnet restore "RetailProcurementApp/RetailProcurementApp.csproj" -v detailed
COPY . .

WORKDIR "/src/RetailProcurementApp"
RUN dotnet build "RetailProcurementApp.csproj" -c Release -o /app -v detailed

FROM build AS publish
RUN dotnet publish "RetailProcurementApp.csproj" -c Release -o /app -v detailed

FROM base AS final
COPY --from=publish /app .

#ENV ASPNETCORE_URLS http://*:8080
EXPOSE 8080
EXPOSE 8081

ENTRYPOINT ["dotnet", "RetailProcurementApp.dll"]
