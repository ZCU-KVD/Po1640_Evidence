using Evidence.Models;

namespace Evidence.Services
{
	public class EvidenceService
	{
		public List<Transakce> TransakceSeznam { get; set; } = new List<Transakce>();

		public void PridatTransakci(Transakce novaTransakce)
		{
			TransakceSeznam.Add(novaTransakce);
		}
		public void SmazatTransakci(Transakce mazanaTransakce)
		{
			TransakceSeznam.Remove(mazanaTransakce);
		}

		public void AktualizujTransakci(Transakce puvodni, Transakce noveHodnoty)
		{
			puvodni.Aktualizovat(noveHodnoty);
		}

		public void VygenerovatNahodnaData(int pocet)
		{
			var random = new Random();
			string[] popisy = { "Prodej zboží", "Konzultace", "Služby", "Oprava zařízení", "Pronájem" };
			for (int i = 0; i < pocet; i++)
			{
				var transakce = new Transakce(datum: DateOnly.FromDateTime(DateTime.Today.AddDays(-random.Next(365))),
					popis: popisy[random.Next(popisy.Length)],
					vynosy: (decimal)(random.NextDouble() * 50000),
					naklady: (decimal)(random.NextDouble() * 30000)
					);
				TransakceSeznam.Add(transakce);
			}
		}


		public List<Transakce> FiltrovatTransakce(string filtrText, decimal? filtrZiskHodnota, Models.OperatorZisku filtrZiskOperator) 
		{
			//var pom = TransakceSeznam.Where(t => t.Zisk == 0);
			var vysledek = TransakceSeznam.AsEnumerable();

			if (!string.IsNullOrWhiteSpace(filtrText))
			{
				vysledek = vysledek.Where(t => t.Popis.Contains(filtrText, StringComparison.OrdinalIgnoreCase));
			}
			if (filtrZiskHodnota.HasValue)
			{
				vysledek = filtrZiskOperator switch
				{
					Models.OperatorZisku.Rovno => vysledek.Where(t => t.Zisk == filtrZiskHodnota),
					Models.OperatorZisku.VetsiNez => vysledek.Where(t => t.Zisk > filtrZiskHodnota),
					Models.OperatorZisku.MensiNez => vysledek.Where(t => t.Zisk < filtrZiskHodnota),
					_ => vysledek
				};
			}
			return vysledek.ToList();
		}
	}
}
