namespace WEB_APP_Panaderia.Entities
{
    public class CotizacionEntity
    {
        public int Id { get; set; }
        public string NombreProveedor { get; set; }
        public string Producto { get; set; }
        public decimal PrecioPorKg { get; set; }
        public string Recomendacion { get; set; }
    }
}