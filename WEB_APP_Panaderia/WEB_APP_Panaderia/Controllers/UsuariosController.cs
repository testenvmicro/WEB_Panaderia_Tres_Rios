using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WEB_APP_Panaderia.Entities;
using WEB_APP_Panaderia.Interfaces;
using WEB_APP_Panaderia.Models;
using System.Text.Json;
using System.Text.Json.Serialization;


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
		[AllowAnonymous]
		[HttpPost]
		public IActionResult ValidarCredenciales(UsuariosEntities entidad)
		{
			try
			{
				var resultado = _usuariosModel.ValidarCredenciales(entidad);

				if (resultado != null)
				{
					HttpContext.Session.SetString("Token", resultado.Token);
					HttpContext.Session.SetString("Usuario", System.Text.Json.JsonSerializer.Serialize(resultado));
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
				// Log the exception
				return View("Error");
			}
		}

		public IActionResult Logout()
        {
            HttpContext.Session.Clear();
			return RedirectToAction("Index", "Home");

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
                return View("Error");
            }
        }

        [HttpPost]
        public IActionResult RegistrarUsuarios(UsuariosEntities entidad)
        {
            try
            {
               
                Console.WriteLine($"WEB: Enviando datos al API: Correo={entidad.correo}, Nombre={entidad.nombre}, IdRol={entidad.idRol}, Contrasenna={entidad.contrasenna}");

                var resultado = _usuariosModel.RegistrarUsuarios(entidad);

                if (resultado > 0)
                {
                    TempData["Mensaje"] = "Usuario registrado exitosamente.";
                    return RedirectToAction("ListaUsuarios");
                }
                else
                {
                    TempData["Mensaje"] = "No se puede agregar el usuario. El correo electrónico ya existe.";
                    return RedirectToAction("ListaUsuarios");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"WEB: Error al registrar usuario: {ex.Message}");
                TempData["Mensaje"] = "Ocurrió un error al registrar el usuario.";
                return RedirectToAction("ListaUsuarios");
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

    }
}
