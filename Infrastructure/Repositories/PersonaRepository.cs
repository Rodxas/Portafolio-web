using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

/// <summary>
/// Implementación del Repository Pattern - Capa de Infraestructura
/// Implementa todas las operaciones CRUD para Persona
/// </summary>
public class PersonaRepository : IPersonaRepository
{
    private readonly PortafolioDbContext _context;

    public PersonaRepository(PortafolioDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Obtiene todas las personas de la base de datos
    /// </summary>
    public async Task<IEnumerable<Persona>> GetAllAsync()
    {
        return await _context.Personas.ToListAsync();
    }

    /// <summary>
    /// Obtiene una persona por su ID
    /// </summary>
    public async Task<Persona?> GetByIdAsync(int id)
    {
        return await _context.Personas.FindAsync(id);
    }

    /// <summary>
    /// Obtiene una persona por su nombre
    /// </summary>
    public async Task<Persona?> GetByNombreAsync(string nombre)
    {
        return await _context.Personas
            .FirstOrDefaultAsync(p => p.Nombre == nombre);
    }

    /// <summary>
    /// Obtiene la persona principal (la primera en la base de datos)
    /// </summary>
    public async Task<Persona?> GetPersonaPrincipalAsync()
    {
        return await _context.Personas.FirstOrDefaultAsync();
    }

    /// <summary>
    /// Agrega una nueva persona
    /// </summary>
    public async Task<Persona> AddAsync(Persona entity)
    {
        await _context.Personas.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    /// <summary>
    /// Actualiza una persona existente
    /// </summary>
    public async Task UpdateAsync(Persona entity)
    {
        _context.Personas.Update(entity);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Elimina una persona por su ID
    /// </summary>
    public async Task DeleteAsync(int id)
    {
        var persona = await GetByIdAsync(id);
        if (persona != null)
        {
            _context.Personas.Remove(persona);
            await _context.SaveChangesAsync();
        }
    }
}
