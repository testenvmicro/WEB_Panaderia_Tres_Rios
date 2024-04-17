using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WEB_APP_Panaderia.Models;

namespace WEB_APP_Panaderia.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

		public IActionResult Metricas()
		{
			return View();
		}

		public IActionResult Usuarios()
		{
			return View();
		}

		public IActionResult Punto_De_Venta()
		{
			return View();
		}

		public IActionResult Privacy()
        {
            return View();
        }
		public IActionResult RegistroDeshechos()
		{
			return View();
		}
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult ListaNotificaciones()
        {
            return View();
        }
        public IActionResult CrearNotificaciones()
        {
            return View();
        }
        public IActionResult EditarNotificaciones()
        {
            return View();
        }
        public IActionResult VerNotificacion()
        {
            return View();
        }
        public IActionResult RegistroClientes()
        {
            return View();
        }
        public IActionResult ListaClientes()
        {
            return View();
        }
    }
}
