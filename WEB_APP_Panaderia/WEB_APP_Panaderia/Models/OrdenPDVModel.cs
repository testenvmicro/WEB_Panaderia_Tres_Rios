using System.Collections.Generic;
using WEB_APP_Panaderia.Entities;
using WEB_APP_Panaderia.Interfaces;

namespace WEB_APP_Panaderia.Models
{
	public class OrdenPDVModel : IOrdenPDVModel
	{
		private readonly IConfiguration _configuration;
		private readonly IHttpContextAccessor _contextAccessor;

		public OrdenPDVModel(IConfiguration configuration, IHttpContextAccessor contextAccessor)
		{
			_configuration = configuration;
			_contextAccessor = contextAccessor;
		}

		public OrdenEntities? CompletarOrden(OrdenEntities orden)
		{
			using (var client = new HttpClient())
			{
				string urlApi = _configuration.GetSection("Parametros:urlApi").Value + "/OrdenPDV/CompletarOrden";

				JsonContent body = JsonContent.Create(orden);
				HttpResponseMessage response = client.PostAsync(urlApi, body).Result;

				if (response.IsSuccessStatusCode)
					return response.Content.ReadFromJsonAsync<OrdenEntities>().Result;

				if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
					throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);

				return null;
			}
		}





	}
}
