﻿using Newtonsoft.Json;
using System.Text;
using System.Text.Json;
using WEB_APP_Panaderia.Controllers;
using WEB_APP_Panaderia.Entities;
using WEB_APP_Panaderia.Interfaces;

namespace WEB_APP_Panaderia.Models
{
    public class UsuariosModel : IUsuariosModel
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _contextAccessor;
	
		public UsuariosModel(IConfiguration configuration, IHttpContextAccessor contextAccessor)
        {
			
			_configuration = configuration;
            _contextAccessor = contextAccessor;
        }

		public List<UsuariosEntities> GetAllUsers() // Cambiar el tipo de retorno a List<UsuariosEntities>
		{
			using (var client = new HttpClient())
			{
				string urlApi = _configuration.GetSection("Parametros:urlApi").Value + "/Usuarios/GetAllUsers";
				HttpResponseMessage response = client.GetAsync(urlApi).Result;

				//_logger.LogInformation("Calling API: " + urlApi);
				//_logger.LogInformation("Response Status: " + response.StatusCode);

				if (response.IsSuccessStatusCode)
				{
					var result = response.Content.ReadFromJsonAsync<List<UsuariosEntities>>().Result;
					//_logger.LogInformation("API Result: " + JsonConvert.SerializeObject(result));
					return result;
				}

				if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
					throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);

				return new List<UsuariosEntities>();
			}
		}

        public void DesactivarUsuario(int idUsuario)
        {
            using (var client = new HttpClient())
            {
                string urlApi = _configuration.GetSection("Parametros:urlApi").Value + $"/Usuarios/DesactivarUsuario?idUsuario={idUsuario}";
                HttpResponseMessage response = client.PostAsync(urlApi, null).Result;

                if (!response.IsSuccessStatusCode)
                    throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);
            }
        }

        public void ActualizarUsuario(UsuariosEntities entidad)
		{
			using (var client = new HttpClient())
			{
				string urlApi = _configuration.GetSection("Parametros:urlApi").Value + "/Usuarios/ActualizarUsuario";

				JsonContent body = JsonContent.Create(entidad);
				HttpResponseMessage response = client.PostAsync(urlApi, body).Result;

				if (!response.IsSuccessStatusCode)
					throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);
			}
		}

		public UsuariosEntities? ValidarCredenciales(UsuariosEntities entidad)
		{
			using (var client = new HttpClient())
			{
				string urlApi = _configuration.GetSection("Parametros:urlApi").Value + "/Usuarios/ValidarCredenciales";

				JsonContent body = JsonContent.Create(entidad);
				HttpResponseMessage response = client.PostAsync(urlApi, body).Result;

				if (response.IsSuccessStatusCode)
				{
					var jsonString = response.Content.ReadAsStringAsync().Result;
					var options = new JsonSerializerOptions
					{
						PropertyNameCaseInsensitive = true
					};
					var result = System.Text.Json.JsonSerializer.Deserialize<LoginResponse>(jsonString, options);

					if (result != null && result.Usuario != null)
					{
						result.Usuario.Token = result.Token;
						return result.Usuario;
					}
				}

				if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
					throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);

				return null;
			}
		}

        public int RegistrarUsuarios(UsuariosEntities entidad)
        {
            using (var client = new HttpClient())
            {
                string urlApi = _configuration.GetSection("Parametros:urlApi").Value + "/Usuarios/RegistrarUsuarios";

                // Serializa manualmente el objeto para asegurarte de que todos los campos se incluyan
                var json = JsonConvert.SerializeObject(entidad);
                Console.WriteLine($"WEB: JSON enviado al API: {json}");

                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(urlApi, content).Result;

                if (response.IsSuccessStatusCode)
                {
                    var resultString = response.Content.ReadAsStringAsync().Result;
                    Console.WriteLine($"WEB: Respuesta del API: {resultString}");
                    return JsonConvert.DeserializeObject<int>(resultString);
                }

                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var errorMessage = response.Content.ReadAsStringAsync().Result;
                    Console.WriteLine($"WEB: Error del API: {errorMessage}");
                    throw new Exception("Excepción Web Api: " + errorMessage);
                }

                return 0;
            }
        }

        public void RecuperarContrasenna(UsuariosEntities entidad)
        {
            using (var client = new HttpClient())
            {
                string urlApi = _configuration.GetSection("Parametros:urlApi").Value + "/Usuarios/RecuperarContrasenna";

                //Serializar convertir un objeto a json
                JsonContent body = JsonContent.Create(entidad);
                HttpResponseMessage response = client.PostAsync(urlApi, body).Result;
            }
        }

        public int BuscarExisteCorreo(string Correo)
        {
            using (var client = new HttpClient())
            {
                string urlApi = _configuration.GetSection("Parametros:urlApi").Value + "/Usuarios/BuscarExisteCorreo?Correo=" + Correo;
                HttpResponseMessage response = client.GetAsync(urlApi).Result;

                if (response.IsSuccessStatusCode)
                    return response.Content.ReadFromJsonAsync<int>().Result;

                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);

                return response.Content.ReadFromJsonAsync<int>().Result;
            }
        }

        public UsuariosEntities GetUsuarioById(int idUsuario)
        {
            using (var client = new HttpClient())
            {
                string urlApi = _configuration.GetSection("Parametros:urlApi").Value + $"/Usuarios/GetUsuarioById?idUsuario={idUsuario}";
                HttpResponseMessage response = client.GetAsync(urlApi).Result;

                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadFromJsonAsync<UsuariosEntities>().Result;
                }

                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);

                return null;
            }
        }

    }
}
