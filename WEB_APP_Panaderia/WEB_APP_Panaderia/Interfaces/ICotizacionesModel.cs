using WEB_APP_Panaderia.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WEB_APP_Panaderia.Interfaces
{
    public interface ICotizacionesModel
    {
        Task<List<CotizacionEntity>> ObtenerTodasLasCotizaciones();
        Task GuardarCotizacion(CotizacionEntity cotizacion);
        Task ActualizarRecomendaciones(List<CotizacionEntity> cotizaciones);
    }
}