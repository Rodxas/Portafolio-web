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
// Configuración de la base de datos
// Usando InMemory Database para simplificar
// (Se puede cambiar a MySQL/SQL Server)
// ============================================
builder.Services.AddDbContext<PortafolioDbContext>(options =>
    options.UseInMemoryDatabase("PortafolioDB"));

// ============================================
// Inyección de Dependencias - SOLID
// Principio DIP: Depender de abstracciones, no de concreciones
// ============================================

// Registro del Repository
builder.Services.AddScoped<IPersonaRepository, PersonaRepository>();

// Registro del Servicio
builder.Services.AddScoped<IPersonaService, PersonaService>();

var app = builder.Build();

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
