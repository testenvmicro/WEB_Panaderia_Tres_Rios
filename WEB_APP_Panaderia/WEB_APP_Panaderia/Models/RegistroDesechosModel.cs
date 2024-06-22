using WEB_APP_Panaderia.Entities;
using WEB_APP_Panaderia.Interfaces;

namespace WEB_APP_Panaderia.Models
{
	public class RegistroDesechosModel : IRegistroDesechosModel
	{
		private readonly IConfiguration _configuration;
		private readonly IHttpContextAccessor _contextAccessor;

		public RegistroDesechosModel(IConfiguration configuration, IHttpContextAccessor contextAccessor)
		{
			_configuration = configuration;
			_contextAccessor = contextAccessor;
		}

		public List<RegistroDesechosEntities> ConsultarRegistroDesechos() 
		{
			using (var client = new HttpClient()) 
			{
				string urlApi = _configuration.GetSection("Parametros:urlApi").Value + "/Bitacora/ConsultarRegistroDesechos";
				HttpResponseMessage response = client.GetAsync(urlApi).Result;

				if (response.IsSuccessStatusCode) 
				{
					var result = response.Content.ReadFromJsonAsync<List<RegistroDesechosEntities>>().Result;
					return result;
				}

				if(response.StatusCode == System.Net.HttpStatusCode.BadRequest)
				
					throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);
				return new List<RegistroDesechosEntities>();
			}
		
		}




	}
}
