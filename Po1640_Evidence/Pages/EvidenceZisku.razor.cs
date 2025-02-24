namespace Po1640_Evidence.Pages
{
	public partial class EvidenceZisku
	{
		#region Vlastnosti
		public List<Models.Polozka> Polozky { get; private set; } = new List<Models.Polozka>();

		public Models.Polozka Polozka { get; set; } = new Models.Polozka();
		#endregion
	}
}
