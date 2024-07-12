using WEB_APP_Panaderia.Entities;
using WEB_APP_Panaderia.Interfaces;

namespace WEB_APP_Panaderia.Models
{
	public class InsumosModel : IInsumosModel
	
		{
			private readonly IConfiguration _configuration;
			private readonly IHttpContextAccessor _contextAccessor;

			public InsumosModel(IConfiguration configuration, IHttpContextAccessor contextAccessor)
			{
				_configuration = configuration;
				_contextAccessor = contextAccessor;
			}


			public List<InsumosEntities>? ConsultaInsumos()
			{
				using (var client = new HttpClient())
				{
				string urlApi = _configuration.GetSection("Parametros:urlApi").Value + "/Insumos/ConsultaInsumos";
					HttpResponseMessage response = client.GetAsync(urlApi).Result;

					if (response.IsSuccessStatusCode)
					{
						var result = response.Content.ReadFromJsonAsync<List<InsumosEntities>>().Result;
						return result;
					}

					if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
						throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);

					return new List<InsumosEntities>();
				}
			}


		}
	}

