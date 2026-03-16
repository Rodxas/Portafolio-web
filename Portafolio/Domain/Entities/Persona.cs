namespace Domain.Entities;

/// <summary>
/// Entidad que representa una persona en el sistema.
/// Sigue el principio SRP (Single Responsibility Principle) de SOLID.
/// </summary>
public class Persona
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public int Edad { get; set; }
    public string? Correo { get; set; }
    public string? Telefono { get; set; }
    public string? Ubicacion { get; set; }
    public string? Titulo { get; set; }
    public string? Biografia { get; set; }
    public DateTime FechaCreacion { get; set; }
    public DateTime? FechaActualizacion { get; set; }
}
