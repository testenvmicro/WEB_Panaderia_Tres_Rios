using iText.Kernel.Pdf;
using iText.Layout.Element;
using iText.Layout.Properties;
using WEB_APP_Panaderia.Entities;
using WEB_APP_Panaderia.Interfaces;
using iText.Layout;
using iText.IO.Image;

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
				string urlApi = _configuration.GetSection("Parametros:urlApi").Value + "/RegistroDesechos/ConsultarRegistroDesechos";
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

		public byte[] GenerarPdfRegistroDesechos(List<RegistroDesechosEntities> registros)
		{
			//List<RegistroDesechosEntities> registros = ConsultarRegistroDesechos();

			using (var stream = new MemoryStream())
			{
				var writer = new PdfWriter(stream);
				var pdf = new PdfDocument(writer);
				var document = new Document(pdf);

				// Agregar el título
				var title = new Paragraph("Reporte de Desechos")
					.SetTextAlignment(TextAlignment.CENTER)
					.SetFontSize(20)
					.SetBold()
					.SetMarginBottom(20);
				document.Add(title);

				// Agregar el logo
				//var logoPath = "/assets/img/logo-pizzeria-panaderia.png"; // Ruta al archivo
				//var logo = new Image(ImageDataFactory.Create(logoPath))
				//	.SetHorizontalAlignment(HorizontalAlignment.CENTER)
				//	.SetMarginBottom(20);
				//document.Add(logo);

				Table table = new Table(new float[] { 1, 2, 2, 2, 2, 1.5f, 1.5f });
				table.SetWidth(UnitValue.CreatePercentValue(100));

				table.AddHeaderCell("ID Evento");
				table.AddHeaderCell("Tratamiento Residuo");
				table.AddHeaderCell("Disposición Final");
				table.AddHeaderCell("Transporte");
				table.AddHeaderCell("Fecha Revisión");
				table.AddHeaderCell("Categoría");
				table.AddHeaderCell("Usuario");

				foreach (var item in registros)
				{
					table.AddCell(item.idEvento.ToString());
					table.AddCell(item.tratamientoResiduo);
					table.AddCell(item.disposicionFinal);
					table.AddCell(item.transporte);
					table.AddCell(item.fechaRevision.ToString("dd/MM/yyyy"));
					table.AddCell(item.categoria);
					table.AddCell(item.usuario);
				}

				document.Add(table);
				document.Close();
				writer.Close();

				return stream.ToArray();
			}
		}




	}
}
