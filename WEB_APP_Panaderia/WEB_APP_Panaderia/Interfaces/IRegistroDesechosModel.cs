using WEB_APP_Panaderia.Entities;

namespace WEB_APP_Panaderia.Interfaces
{
	public interface IRegistroDesechosModel
	{
		public List<RegistroDesechosEntities> ConsultarRegistroDesechos();

		public byte[] GenerarPdfRegistroDesechos(List<RegistroDesechosEntities> registros);
	}
}
