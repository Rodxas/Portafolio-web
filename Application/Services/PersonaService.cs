using Domain.Entities;
using Domain.Interfaces;
using Application.Interfaces;
using Application.Configuration;
using Microsoft.Extensions.Options;

namespace Application.Services;

/// <summary>
/// Servicio de Persona - Capa de Negocio/Aplicación
/// Implementa SOLID:
/// - SRP: Una única responsabilidad - gestionar operaciones de Persona
/// - OCP: Abierto para extensión, cerrado para modificación
/// - LSP: Puede sustituirse por cualquier implementación de IPersonaService
/// - ISP: Interfaz pequeña y específica
/// - DIP: Depende de abstracciones (IPersonaRepository), no de implementaciones
/// </summary>
public class PersonaService : IPersonaService
{
    private readonly IPersonaRepository _personaRepository;
    private readonly PersonaSettings _personaSettings;
    private readonly ILogger<PersonaService> _logger;

    /// <summary>
    /// Constructor con inyección de dependencias (Principio DIP)
    /// </summary>
    public PersonaService(
        IPersonaRepository personaRepository,
        IOptions<PersonaSettings> personaSettings,
        ILogger<PersonaService> logger)
    {
        _personaRepository = personaRepository;
        _personaSettings = personaSettings.Value;
        _logger = logger;
    }

    /// <summary>
    /// Obtiene la persona principal del portafolio desde configuración
    /// </summary>
    public async Task<Persona?> ObtenerPersonaPrincipalAsync()
    {
        _logger.LogInformation("Obteniendo persona principal desde configuración");

        // Retorna persona desde configuración
        return await Task.FromResult(new Persona
        {
            Id = 1,
            Nombre = _personaSettings.Nombre,
            Edad = _personaSettings.Edad,
            Correo = _personaSettings.Correo,
            Telefono = _personaSettings.Telefono,
            Ubicacion = _personaSettings.Ubicacion,
            Titulo = _personaSettings.Titulo,
            Biografia = _personaSettings.Biografia,
            FechaCreacion = DateTime.UtcNow
        });
    }

    /// <summary>
    /// Obtiene todas las personas
    /// </summary>
    public async Task<IEnumerable<Persona>> ObtenerTodasLasPersonasAsync()
    {
        return await _personaRepository.GetAllAsync();
    }

    /// <summary>
    /// Obtiene una persona por su ID
    /// </summary>
    public async Task<Persona?> ObtenerPersonaPorIdAsync(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("El ID debe ser mayor a 0", nameof(id));
        }

        return await _personaRepository.GetByIdAsync(id);
    }

    /// <summary>
    /// Crea una nueva persona
    /// </summary>
    public async Task<Persona> CrearPersonaAsync(Persona persona)
    {
        ValidarPersona(persona);
        
        persona.FechaCreacion = DateTime.UtcNow;
        return await _personaRepository.AddAsync(persona);
    }

    /// <summary>
    /// Actualiza una persona existente
    /// </summary>
    public async Task<bool> ActualizarPersonaAsync(Persona persona)
    {
        if (persona == null)
        {
            throw new ArgumentNullException(nameof(persona));
        }

        ValidarPersona(persona);
        
        var existente = await _personaRepository.GetByIdAsync(persona.Id);
        if (existente == null)
        {
            return false;
        }

        persona.FechaActualizacion = DateTime.UtcNow;
        await _personaRepository.UpdateAsync(persona);
        return true;
    }

    /// <summary>
    /// Elimina una persona por su ID
    /// </summary>
    public async Task<bool> EliminarPersonaAsync(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("El ID debe ser mayor a 0", nameof(id));
        }

        var persona = await _personaRepository.GetByIdAsync(id);
        if (persona == null)
        {
            return false;
        }

        await _personaRepository.DeleteAsync(id);
        return true;
    }

    /// <summary>
    /// Método privado para validar persona (SRP - responsabilidad única de validación)
    /// </summary>
    private void ValidarPersona(Persona persona)
    {
        if (persona == null)
        {
            throw new ArgumentNullException(nameof(persona));
        }

        if (string.IsNullOrWhiteSpace(persona.Nombre))
        {
            throw new ArgumentException("El nombre es requerido", nameof(persona.Nombre));
        }

        if (persona.Edad < 0)
        {
            throw new ArgumentException("La edad no puede ser negativa", nameof(persona.Edad));
        }
    }
}
