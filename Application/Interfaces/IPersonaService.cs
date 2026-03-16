using Domain.Entities;

namespace Application.Interfaces;

/// <summary>
/// Interfaz para el servicio de Persona (Capa de Aplicación)
/// Define el contrato para las operaciones de negocio (Principio ISP)
/// </summary>
public interface IPersonaService
{
    Task<Persona?> ObtenerPersonaPrincipalAsync();
    Task<IEnumerable<Persona>> ObtenerTodasLasPersonasAsync();
    Task<Persona?> ObtenerPersonaPorIdAsync(int id);
    Task<Persona> CrearPersonaAsync(Persona persona);
    Task<bool> ActualizarPersonaAsync(Persona persona);
    Task<bool> EliminarPersonaAsync(int id);
}
