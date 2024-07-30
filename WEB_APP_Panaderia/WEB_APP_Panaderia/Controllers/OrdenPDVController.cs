using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml.Export.HtmlExport.StyleCollectors.StyleContracts;
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

		public IActionResult OrdenesPizzeria()
		{
			try
			{
				var viewModel = new OrdenesPizzeriaViewModel
				{
					Ordenes = _ordenModel.OrdenesPizzeria(),
					Orden = new DetalleOrdenEntities()
				};
				if (viewModel.Ordenes == null || viewModel.Ordenes.Count() == 0)
				{
					ViewData["Message"] = "No hay ordenes pendientes, ¡Buen trabajo!.";
				}

				return View(viewModel);
			}
			catch (Exception ex)
			{
				return View("Error");
			}
		}

		[HttpGet]
		public IActionResult ConsultarDetalleOrdenPDV(int id)
		{
			var detalleOrden = _ordenModel.ConsultarDetalleOrden(id);
			if (detalleOrden == null)
			{
				return NotFound(new { message = "No se encontraron detalles para la orden." });
			}
			return Json(detalleOrden);
		}

		[HttpGet]
		public IActionResult ConsultarEstadosOrden()
		{
			return Json(_ordenModel.ConsultarEstadosOrden());
		}

		[HttpPost]
		public IActionResult ActualizarEstadoOrden([FromBody] DetalleOrdenEntities orden)
		{
			try
			{
				_ordenModel.ActualizarEstadoOrden(orden);
				return Json(new { success = true });
			}
			catch (Exception ex)
			{
				return Json(new { success = false, message = ex.Message });
			}
		}

	}
}
