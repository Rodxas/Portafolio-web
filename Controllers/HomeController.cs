using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Application.Interfaces;
using Domain.Entities;
using Portafolio.Models;

namespace Portafolio.Controllers
{
    /// <summary>
    /// Controlador Home - Capa de Presentación
    /// Utiliza inyección de dependencias para obtener los servicios
    /// </summary>
    public class HomeController : Controller
    {
        private readonly IPersonaService _personaService;
        private readonly ILogger<HomeController> _logger;

        /// <summary>
        /// Constructor con inyección de dependencias (Principio DIP)
        /// </summary>
        public HomeController(
            IPersonaService personaService, 
            ILogger<HomeController> logger)
        {
            _personaService = personaService;
            _logger = logger;
        }

        /// <summary>
        /// Acción principal del portafolio - Obtiene los datos del servicio
        /// </summary>
        public async Task<IActionResult> Index()
        {
            try
            {
                // Obtiene la persona desde la capa de servicio (lee de appsettings.json)
                var persona = await _personaService.ObtenerPersonaPrincipalAsync();
                
                return View(persona);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los datos del portafolio");
                
                // Re-lanza la excepción para manejo global
                throw;
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
