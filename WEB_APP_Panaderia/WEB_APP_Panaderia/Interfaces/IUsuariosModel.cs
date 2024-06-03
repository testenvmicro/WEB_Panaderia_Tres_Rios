using WEB_APP_Panaderia.Entities;

namespace WEB_APP_Panaderia.Interfaces
{
    public interface IUsuariosModel
    {
        public UsuariosEntities? ValidarCredenciales(UsuariosEntities entidad);

        public void Recuperar_Contrasenna(UsuariosEntities entidad);

        public int RegistrarUsuarios(UsuariosEntities entidad);

	}
}
