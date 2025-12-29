FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY MediUp.Backoffice/MediUp.Backoffice.csproj MediUp.Backoffice/
COPY MediUp.Application/MediUp.Application.csproj MediUp.Application/
COPY MediUp.Infrastructure/MediUp.Infrastructure.csproj MediUp.Infrastructure/
COPY MediUp.Domain/MediUp.Domain.csproj MediUp.Domain/
RUN dotnet restore MediUp.Backoffice/MediUp.Backoffice.csproj

COPY . .
WORKDIR /src/MediUp.Backoffice
RUN dotnet publish MediUp.Backoffice.csproj -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:${PORT:-8080}
ENV DOTNET_EnableDiagnostics=0
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "MediUp.Backoffice.dll"]
