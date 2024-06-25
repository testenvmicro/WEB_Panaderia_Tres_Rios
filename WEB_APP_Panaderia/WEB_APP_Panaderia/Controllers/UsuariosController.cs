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
        private readonly IUsuariosModel _usuariosModel;

        public UsuariosController(ILogger<UsuariosController> logger, IUsuariosModel usuariosModel)
        {
            _logger = logger;
            _usuariosModel = usuariosModel;
 
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
                    HttpContext.Session.SetString("id_usuario", resultado.idUsuario.ToString());
                    HttpContext.Session.SetString("Correo", resultado.correo);
                   // HttpContext.Session.SetString("Token", resultado.Token);
                   // HttpContext.Session.SetString("Nombre", resultado.nombre);
                   // HttpContext.Session.SetString("Prim_Ape", resultado.prim_ap);
                    return RedirectToAction("Metricas", "Home");
                }

                else
                {
                    ViewBag.mensaje = "<div class='alert alert-warning' role='alert'> Usuario y contraseña son incorrectos </div>";
                    return View("Index");
                }

            }
            catch (Exception ex)
            {
               // RegistrarBitacora(ex, ControllerContext);
                return View("Error");

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
				// Manejar error
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
				// RegistrarBitacora(ex, ControllerContext);
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
                //RegistrarBitacora(ex, ControllerContext);
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

    }
}
