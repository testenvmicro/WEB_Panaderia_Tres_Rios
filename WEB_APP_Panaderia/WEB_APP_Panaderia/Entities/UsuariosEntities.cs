﻿namespace WEB_APP_Panaderia.Entities
{
    public class UsuariosEntities
    {

            public long idUsuario { get; set; }
            public string correo { get; set; } = string.Empty;
            public string contrasenna { get; set; } = string.Empty;
            public bool estado { get; set; } = false;
            public int idRol { get; set; } = 0;
		    public string descripcion { get; set; } = string.Empty;
		    public string nombre { get; set; } = string.Empty;
            public string Token { get; set; } = string.Empty;       
		    
		

	}
}
