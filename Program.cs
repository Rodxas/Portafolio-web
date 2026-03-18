using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.ResponseCompression;
using Domain.Interfaces;
using Application.Interfaces;
using Application.Services;
using Application.Configuration;
using Infrastructure.Data;
using Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// ============================================
// Configuración de la aplicación
// ============================================

// Registro de opciones de configuración (Pattern Options)
builder.Services.Configure<PersonaSettings>(
    builder.Configuration.GetSection(PersonaSettings.SectionName));

// ============================================
// Memory Cache - Rendimiento
// ============================================
builder.Services.AddMemoryCache();

// ============================================
// Configuración de producción - Compression & Caching
// ============================================
if (!builder.Environment.IsDevelopment())
{
    builder.Services.AddResponseCompression(options =>
    {
        options.EnableForHttps = true;
        options.Providers.Add<BrotliCompressionProvider>();
        options.Providers.Add<GzipCompressionProvider>();
    });
    
    builder.Services.AddResponseCaching();
}

// ============================================
// Base de datos - SQLite (Producción)
// ============================================
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? "Data Source=portafolio.db";

builder.Services.AddDbContext<PortafolioDbContext>(options =>
    options.UseSqlite(connectionString));

// ============================================
// Inyección de Dependencias - SOLID
// Principio DIP: Depender de abstracciones, no de concreciones
// ============================================

// Registro del Repository
builder.Services.AddScoped<IPersonaRepository, PersonaRepository>();

// Registro del Servicio
builder.Services.AddScoped<IPersonaService, PersonaService>();

var app = builder.Build();

// ============================================
// Inicializar base de datos SQLite
// ============================================
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<PortafolioDbContext>();
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
    app.UseResponseCompression();
    app.UseResponseCaching();
}

// HTTPS Redirection - Comment out for containerized deployment
// app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
