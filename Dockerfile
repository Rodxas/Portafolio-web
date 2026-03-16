# ============================================
# Dockerfile - Portafolio ASP.NET Core MVC
# Optimizado para producción
# ============================================

# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copiar archivos de proyecto
COPY Portafolio.csproj ./Portafolio.csproj

# Restaurar dependencias
RUN dotnet restore Portafolio.csproj

# Copiar todo el código
COPY . .

# Publicar en modo Release
WORKDIR /src
RUN dotnet publish Portafolio.csproj -c Release -o /src/publish --no-restore

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /src

# Copiar archivos publicados
COPY --from=build /src/publish .

# Exponer puerto
EXPOSE 8080

# Punto de entrada
ENTRYPOINT ["dotnet", "Portafolio.dll"]
