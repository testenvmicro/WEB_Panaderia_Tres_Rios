using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WEB_APP_Panaderia.Entities;
using WEB_APP_Panaderia.Interfaces;
using WEB_APP_Panaderia.Models;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;


namespace WEB_APP_Panaderia.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRegistroDesechosModel _reportesModel;

		public HomeController(ILogger<HomeController> logger, IRegistroDesechosModel bitacoraModel)
        {
            _logger = logger;
            _reportesModel = bitacoraModel;
        }

        public IActionResult Index()
        {
            return View();
        }

		public IActionResult Metricas()
		{
			return View();
		}

		public IActionResult Recuperar_contrasenna()
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

		public IActionResult Clientes()
		{
			return View();
		}

		public IActionResult Proveedores()
		{
			return View();
		}

		public IActionResult Ordenes_Pizzeria()
		{
			return View();
		}

		public IActionResult Privacy()
        {
            return View();
        }
		public IActionResult RegistroDeshechos()
		{
            try
            {
                var viewModel = new RegistroDesechosViewModel
                {
                    Reportes = _reportesModel.ConsultarRegistroDesechos(),
                    Reporte = new RegistroDesechosEntities()
                };
                return View(viewModel);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
		}

		public IActionResult RegistroDeshechosPdf()
		{

			var resultado = _reportesModel.ConsultarRegistroDesechos();

			if (resultado == null || resultado.Count == 0)
			{
				return NotFound("No se encontraron registros.");
			}

			var pdfBytes = _reportesModel.GenerarPdfRegistroDesechos(resultado); 

			return File(pdfBytes, "application/pdf", "ReporteDesechos.pdf");
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

