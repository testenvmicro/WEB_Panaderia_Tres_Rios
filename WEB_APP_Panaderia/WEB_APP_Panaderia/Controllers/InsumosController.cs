using Microsoft.AspNetCore.Mvc;
using WEB_APP_Panaderia.Entities;
using WEB_APP_Panaderia.Interfaces;
using WEB_APP_Panaderia.Models;

namespace WEB_APP_Panaderia.Controllers
{
	public class InsumosController : Controller
	{
		private readonly IInsumosModel _InsumosModel;

		public InsumosController(IInsumosModel InsumosModel)
		{
			_InsumosModel = InsumosModel;
		}
		[HttpGet]
        public IActionResult ConsultaInsumos()
        {
            try
            {
                var viewModel = new InsumosViewModel
                {
                    Insumos = _InsumosModel.ConsultaInsumos(),
                    Insumo = new InsumosEntities()

                };
                if (viewModel.Insumos == null || viewModel.Insumos.Count() == 0)
                {
                    ViewData["Message"] = "No hay registros de Insumos.";
                }

                return View(viewModel);
            }
            catch (Exception ex)
            {

                return View("Error");
            }
        }
    }
}
