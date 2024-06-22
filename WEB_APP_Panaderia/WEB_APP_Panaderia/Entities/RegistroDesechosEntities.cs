namespace WEB_APP_Panaderia.Entities
{
	public class RegistroDesechosEntities
	{
		public long idEvento { get; set; }
		public string tratamientoResiduo { get; set; } = string.Empty;
		public string disposicionFinal { get; set; } = string.Empty;
		public string transporte { get; set; } = string.Empty;
		public DateTime fechaRevision { get; set; }
		public string categoria { get; set; } = string.Empty;
		public string usuario { get; set; } = string.Empty;
	}
}
