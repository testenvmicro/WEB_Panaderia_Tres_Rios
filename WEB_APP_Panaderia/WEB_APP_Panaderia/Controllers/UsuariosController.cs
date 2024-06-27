using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WEB_APP_Panaderia.Entities;
using WEB_APP_Panaderia.Interfaces;
using WEB_APP_Panaderia.Models;

namespace WEB_APP_Panaderia.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly ILogger<UsuariosController> _logger;
		private readonly ILogsModel _generalesModel;
		private readonly IUsuariosModel _usuariosModel;


        public UsuariosController(ILogger<UsuariosController> logger, IUsuariosModel usuariosModel, ILogsModel generalesModel)
        {
            _logger = logger;
            _usuariosModel = usuariosModel;
			_generalesModel = generalesModel;
		}


        [HttpGet]
		public IActionResult ListaUsuarios()
		{
			try
			{
                var viewModel = new ViewModel
                {
                    Usuarios = _usuariosModel.GetAllUsers(),
                    Usuario = new UsuariosEntities()
			};
				//var usuarios = _usuariosModel.GetAllUsers();
				
				return View(viewModel);
			}
			catch (Exception ex)
			{
				RegistrarBitacora(ex, ControllerContext);
				return View("Error");
			}
		}

		[HttpPost]
        public IActionResult ValidarCredenciales(UsuariosEntities entidad)
        {
            try
            {
                
                var resultado = _usuariosModel.ValidarCredenciales(entidad);

                if (resultado != null)
                {
					string rolDescripcion = string.Empty;

					if (resultado.descripcion == "admin")
					{
						rolDescripcion = "Administrador";
					}
					HttpContext.Session.SetString("id_usuario", resultado.idUsuario.ToString());
                    HttpContext.Session.SetString("Correo", resultado.correo);
                    HttpContext.Session.SetString("Nombre", resultado.nombre);
					HttpContext.Session.SetString("Rol", rolDescripcion);
                    // HttpContext.Session.SetString("Token", resultado.Token);
                    return RedirectToAction("Metricas", "Home");
                }

                else
                {
                    TempData["mensaje"] = "Usuario y contraseña incorrectos.\n Por favor verifique sus credenciales e intente de nuevo.";
                    return RedirectToAction("Index", "Home");
				}

            }
            catch (Exception ex)
            {
				//return View("Error");
				return RedirectToAction("ErrorUsuario", "Home");

			}


		}
		[HttpPost]
		public IActionResult ActualizarUsuario(UsuariosEntities entidad)
		{
			try
			{
				_usuariosModel.ActualizarUsuario(entidad);
				return RedirectToAction("ListaUsuarios");
			}
			catch (Exception ex)
			{
				RegistrarBitacora(ex, ControllerContext);
				return View("Error");
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
				RegistrarBitacora(ex, ControllerContext);
				return View("Error");

			}

		}

        [HttpPost]
        public IActionResult RecuperarContrasenna(UsuariosEntities entidad)
        {
            try
            {
                _usuariosModel.RecuperarContrasenna(entidad);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
				RegistrarBitacora(ex, ControllerContext);
				return View("Error");
            }
        }

        [HttpGet]
        public IActionResult BuscarExisteCorreo(string Correo)
        {
            var resultado = _usuariosModel.BuscarExisteCorreo(Correo);
            var jsonResult = JsonConvert.SerializeObject(resultado);
            return Content(jsonResult, "application/json");
        }

		public void RegistrarBitacora(Exception ex, ControllerContext contexto)
		{
			LogsEntities error = new LogsEntities();
			error.Origen = ControllerContext.ActionDescriptor.ControllerName + " - " + ControllerContext.ActionDescriptor.ActionName;
			error.Detalle = ex.Message;

			_generalesModel.RegistrarBitacora(error);
		}




	}
}
