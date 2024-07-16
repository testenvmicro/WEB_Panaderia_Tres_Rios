using WEB_APP_Panaderia.Entities;

namespace WEB_APP_Panaderia.Interfaces
{
	public interface IOrdenPDVModel
	{
		public OrdenEntities? CompletarOrden(OrdenEntities orden);
	}
}
