using Microsoft.AspNetCore.Mvc;
using WEB_APP_Panaderia.Entities;
using WEB_APP_Panaderia.Interfaces;

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

    }
}
