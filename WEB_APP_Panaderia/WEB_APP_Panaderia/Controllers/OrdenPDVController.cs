using Microsoft.AspNetCore.Mvc;
using WEB_APP_Panaderia.Entities;
using WEB_APP_Panaderia.Interfaces;
using WEB_APP_Panaderia.Models;

namespace WEB_APP_Panaderia.Controllers
{
	public class OrdenPDVController : Controller
	{
		private readonly IOrdenPDVModel _ordenModel;

		public OrdenPDVController(IOrdenPDVModel ordenModel)
		{
			_ordenModel = ordenModel;
		}


		public IActionResult CompletarOrden([FromBody] OrdenEntities orden)
		{
			try
			{
				var result = _ordenModel.CompletarOrden(orden);
				if (result != null)
				{
					return Json(new { success = true, idOrden = result.idOrden, nombre = result.ClienteOrden.nombre });
				}
				else
				{
					return Json(new { success = false, message = "No se pudo completar la orden." });
				}
			}
			catch (Exception ex)
			{
				return Json(new { success = false, message = ex.Message });
			}

		}
	}
}
