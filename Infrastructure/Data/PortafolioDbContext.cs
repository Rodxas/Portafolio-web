using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infrastructure.Data;

/// <summary>
/// DbContext para el portafolio - Capa de Infraestructura
/// Maneja la conexión y operaciones con la base de datos
/// </summary>
public class PortafolioDbContext : DbContext
{
    public PortafolioDbContext(DbContextOptions<PortafolioDbContext> options) 
        : base(options)
    {
    }

    /// <summary>
    /// DbSet de Personas - Tabla en la base de datos
    /// </summary>
    public DbSet<Persona> Personas { get; set; } = null!;

    /// <summary>
    /// Configuración del modelo de Persona
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuración de la entidad Persona
        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(100);
            
            entity.Property(e => e.Edad)
                .IsRequired();

            entity.Property(e => e.Correo)
                .HasMaxLength(150);

            entity.Property(e => e.Telefono)
                .HasMaxLength(20);

            entity.Property(e => e.Ubicacion)
                .HasMaxLength(100);

            entity.Property(e => e.Titulo)
                .HasMaxLength(100);

            entity.Property(e => e.Biografia)
                .HasMaxLength(500);
        });
    }
}
