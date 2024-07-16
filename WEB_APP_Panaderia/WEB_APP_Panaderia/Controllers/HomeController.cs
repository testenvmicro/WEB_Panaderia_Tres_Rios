using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly IRegistroDesechosModel _registroDesechosModel;
		private readonly IUsuariosModel _usuariosModel;
		private readonly IProveedoresModel _proveedoresModel;

        private readonly IUsuariosRolesModel _usuariosRolesModel;

        private readonly IRegistroDesechosModel _reportesModel;
        public HomeController(ILogger<HomeController> logger, IUsuariosModel usuariosModel, IProveedoresModel proveedoresModel, IUsuariosRolesModel usuariosRolesModel, IRegistroDesechosModel bitacoraModel)


        {
            _logger = logger;
			_usuariosModel = usuariosModel;
			_proveedoresModel = proveedoresModel;

            _usuariosRolesModel = usuariosRolesModel;
			_registroDesechosModel = bitacoraModel;

			_reportesModel = bitacoraModel;
		}

		[AllowAnonymous]
		public IActionResult Index()
        {
            return View();
        }

		[TypeFilter(typeof(JwtAuthorizationFilter))]
		public IActionResult Metricas()
		{
			return View();
		}

		public IActionResult Recuperar_contrasenna()
		{
			return View();
		}
		[TypeFilter(typeof(JwtAuthorizationFilter))]
        public IActionResult Usuarios()
        {
            try
            {
                var viewModel = new ViewModel
                {
                    Usuarios = _usuariosModel.GetAllUsers(),
                    Usuario = new UsuariosEntities(),
                    Roles = _usuariosRolesModel.ConsultarUsuariosRoles()
                };
                return View(viewModel);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        public IActionResult Punto_De_Venta()
		{
			return View();
		}

		public IActionResult Clientes()
		{
			return View();
		}
		[TypeFilter(typeof(JwtAuthorizationFilter))]
		[HttpPost]
        public IActionResult RegistrarProveedores(ProveedoresEntities proveedor)
        {
            try
            {
                _proveedoresModel.RegistrarProveedores(proveedor);
                return RedirectToAction("Proveedores");
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
           
        }
        [HttpPost]
        public IActionResult EliminarProveedor(int idProveedor)
        {
            try
            {
                _proveedoresModel.EliminarProveedor(idProveedor);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }



		[TypeFilter(typeof(JwtAuthorizationFilter))]
		public IActionResult Proveedores()
		{
			try
			{
				var viewModel = new ViewModelProveedores
				{
					Proveedores = _proveedoresModel.GetAllProveedores(),
					Proveedor = new ProveedoresEntities()

				};
                if (Proveedores == null)
                {
                    ViewData["Message"] = "No hay registros de proveedores.";
                }

                return View(viewModel);
			}
			catch (Exception ex)
			{

				return View("Error");
			}
		}

        [HttpPost]
        public IActionResult ActualizarUsuario(UsuariosEntities entidad)
        {
            try
            {
                _usuariosModel.ActualizarUsuario(entidad);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult DesactivarUsuario(int idUsuario)
        {
            try
            {
                _usuariosModel.DesactivarUsuario(idUsuario);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult RegistrarUsuarios(UsuariosEntities entidad)
        {
            try
            {
                var resultado = _usuariosModel.RegistrarUsuarios(entidad);

                if (resultado > 0)
                {

                    return RedirectToAction("Usuarios", "Home");
                }

                else
                {
                    ViewBag.mensaje = "<div class='alert alert-warning' role='alert'> No se puede agregar el usuario </div>";
                    return View("Home");
                }

            }
            catch (Exception ex)
            {
                // RegistrarBitacora(ex, ControllerContext);
                return View("Error");
            }
        }

                [HttpPost]
        public IActionResult ActualizarProveedor(ProveedoresEntities proveedor)
        {
            try
            {
                _proveedoresModel.ActualizarProveedor(proveedor);
                return RedirectToAction("Proveedores");
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        [HttpGet]
        public IActionResult ObtenerProveedor(int id)
        {
            var proveedor = _proveedoresModel.GetProveedorById(id);
            return Json(proveedor);
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
                    Reportes = _registroDesechosModel.ConsultarRegistroDesechos(),
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

			var resultado = _registroDesechosModel.ConsultarRegistroDesechos();

			if (resultado == null || resultado.Count == 0)
			{
				return NotFound("No se encontraron registros.");
			}

			var pdfBytes = _registroDesechosModel.GenerarPdfRegistroDesechos(resultado); 

			return File(pdfBytes, "application/pdf", "ReporteDesechos.pdf");
		}

		public IActionResult RegistroDeshechosExcel()
		{
			var resultado = _registroDesechosModel.ConsultarRegistroDesechos();

			if (resultado == null || resultado.Count == 0)
			{
				return NotFound("No se encontraron registros.");
			}

			var excelBytes = _registroDesechosModel.GenerarExcelRegistroDesechos(resultado);

			return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "RegistroDesechos.xlsx");
		}


		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

		public IActionResult ErrorUsuario()
		{
            return View();
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

        [HttpGet]
        public IActionResult GetUsuarioById(int idUsuario)
        {
            try
            {
                var usuario = _usuariosModel.GetUsuarioById(idUsuario);
                return Json(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }


}


