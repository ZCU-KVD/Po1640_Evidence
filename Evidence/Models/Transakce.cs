namespace Evidence.Models
{
	public class Transakce
	{
		public Transakce(DateOnly datum, string popis, decimal vynosy, decimal naklady)
		{
			Datum = datum;
			Popis = popis;
			Vynosy = vynosy;
			Naklady = naklady;
		}

		public Guid Id { get; set; } = Guid.NewGuid();
		public DateOnly Datum { get; set; }
		public string Popis { get; set; } = string.Empty;
		public decimal Vynosy { get; set; }
		public decimal Naklady { get; set; }
		public decimal Zisk => Vynosy - Naklady;

	}
}
