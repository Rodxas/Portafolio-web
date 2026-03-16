using Domain.Entities;

namespace Domain.Interfaces;

/// <summary>
/// Interfaz genérica para Repository Pattern (Principio ISP - Interface Segregation)
/// </summary>
public interface IBaseRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
}

/// <summary>
/// Interfaz específica para PersonaRepository (Principio DIP - Dependency Inversion)
/// Cumple con el patrón Repository.
/// </summary>
public interface IPersonaRepository : IBaseRepository<Persona>
{
    Task<Persona?> GetByNombreAsync(string nombre);
    Task<Persona?> GetPersonaPrincipalAsync();
}
