# ============================================
# Dockerfile - Portafolio ASP.NET Core MVC
# Optimizado para producción
# ============================================

# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copiar archivos de proyecto
COPY Portafolio.csproj ./Portafolio.csproj
COPY Directory.Build.props ./Directory.Build.props 2>/dev/null || true

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

# Instalar Brotli compression provider
RUN apt-get update && apt-get install -y --no-install-recommends \
    brotli \
    && rm -rf /var/lib/apt/lists/*

# Crear usuario no root para seguridad
RUN groupadd -r appgroup && useradd -r -g appgroup appuser

# Copiar archivos publicados
COPY --from=build /src/publish .

# Configurar permisos
RUN chown -R appuser:appgroup /src

# Cambiar a usuario no root
USER appuser

# Configuración del puerto
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

# Exponer puerto
EXPOSE 8080

# Punto de entrada
ENTRYPOINT ["dotnet", "Portafolio.dll"]
