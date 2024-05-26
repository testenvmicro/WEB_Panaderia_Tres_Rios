namespace WEB_APP_Panaderia.Entities
{
    public class UsuariosEntities
    {
        public long id_usuario { get; set; }
        public string correo { get; set; } = string.Empty;
        public string contrasenna { get; set; } = string.Empty;
        public bool estado { get; set; } = false;
        public int id_rol { get; set; } = 2;
    }
}
