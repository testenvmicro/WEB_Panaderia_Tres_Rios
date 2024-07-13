using WEB_APP_Panaderia.Entities;

namespace WEB_APP_Panaderia.Interfaces
{
	public interface IRegistroDesechosModel
	{
		public List<RegistroDesechosEntities> ConsultarRegistroDesechos();
		public int AgregarRegistroDesecho(RegistroDesechosEntities reporte);
		public void ActualizarRegistroDesecho(RegistroDesechosEntities reporte);

		public void EliminarRegistroDesecho(int idProveedor);
		public RegistroDesechosEntities? ConsultarUnRegistroDesecho(int id);
		public byte[] GenerarPdfRegistroDesechos(List<RegistroDesechosEntities> registros);
		public byte[] GenerarExcelRegistroDesechos(List<RegistroDesechosEntities> registros);
		public List<CategoriaDesechoEntities> ConsultarCategoriaDesecho();
		public List<CategoriaDesechoTratamientoEntities> ConsultarCategoriaDesechoTratamiento();
		public List<DesechoDisposicionFinalEntities> ConsultarDesechoDisposicionFinal();
		public List<DesechoTransporteEntities> ConsultarDesechoTransporte();
	}
}
