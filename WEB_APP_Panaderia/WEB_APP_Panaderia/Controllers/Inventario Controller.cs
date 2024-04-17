using Microsoft.AspNetCore.Mvc;

namespace WEB_APP_Panaderia.Controllers
{
	public class Inventario : Controller
	{
		public IActionResult InventarioLista()
		{
			return View();
		}
		public IActionResult NuevoInsumo()
		{
			return View();
		}
	}
}
