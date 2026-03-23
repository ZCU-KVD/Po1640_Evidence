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
	}
}
