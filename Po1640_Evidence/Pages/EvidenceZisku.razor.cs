using Microsoft.JSInterop;

namespace Po1640_Evidence.Pages
{
	public partial class EvidenceZisku
	{
		#region Vlastnosti
		public List<Models.Polozka> Polozky { get; private set; } = new List<Models.Polozka>();

		private Models.Polozka Polozka { get; set; } = new Models.Polozka();

		private bool IsEditace { get; set; } = false;
		#endregion

		#region Metody
		private void PridatZaznam()
		{
			Polozky.Add(new Models.Polozka(Polozka.Datum, Polozka.Naklady, Polozka.Vynosy, Polozka.Popis));
			//Polozka = new Models.Polozka();
		}
		private async Task SmazatZaznam(Models.Polozka mazanaPolozka)
		{
			var zprava = $"Opravdu chcete smazat záznam z {mazanaPolozka.Datum} se ziskem {mazanaPolozka.Zisk}?";
			bool smazat = await JavaScript.InvokeAsync<bool>("confirm", zprava);
			if (smazat)
				Polozky.Remove(mazanaPolozka);
		}

		private void UpravitZaznam(Models.Polozka upravovanaPolozka)
		{
			Polozka = upravovanaPolozka;
			IsEditace = true;
		}

		private void UkoncitEditaci()
		{
			IsEditace = false;
			Polozka = new Models.Polozka();
		}

		private void SetriditDleDatumu()
		{
			Polozky.Sort((x,y) => x.Datum.CompareTo(y.Datum));
		}

		private void SetriditSestupneDleDatumu()
		{
			Polozky.Sort((x, y) => y.Datum.CompareTo(x.Datum));
		}
		#endregion
	}
}
