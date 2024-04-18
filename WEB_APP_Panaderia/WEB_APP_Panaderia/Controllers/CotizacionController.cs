using Microsoft.AspNetCore.Mvc;

namespace WEB_APP_Panaderia.Controllers
{
	public class CotizacionController : Controller
	{
		public IActionResult CotizacionesLista()
		{
			return View();
		}
		public IActionResult EditarCotizacion()
		{
			return View();
		}
	}
}
