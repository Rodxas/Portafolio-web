namespace Application.Configuration;

/// <summary>
/// Configuración de Persona leída desde appsettings.json
/// Sigue el patrón Options de .NET para inyección de configuración
/// </summary>
public class PersonaSettings
{
    /// <summary>
    /// Nombre de la sección en appsettings.json
    /// </summary>
    public const string SectionName = "Persona";

    public string Nombre { get; set; } = string.Empty;
    public int Edad { get; set; }
    public string Correo { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
    public string Ubicacion { get; set; } = string.Empty;
    public string Titulo { get; set; } = string.Empty;
    public string Biografia { get; set; } = string.Empty;
}
