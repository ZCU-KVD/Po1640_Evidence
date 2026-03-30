using System.ComponentModel.DataAnnotations;

namespace Evidence.Models
{
	public class Transakce
	{
		private decimal vynosy;
		private decimal naklady;
		private string popis = string.Empty;

		public Transakce() { }
		public Transakce(DateOnly datum, string popis, decimal vynosy, decimal naklady)
		{
			Datum = datum;
			Popis = popis;
			Vynosy = vynosy;
			Naklady = naklady;
		}

		public Guid Id { get; set; } = Guid.NewGuid();
		public DateOnly Datum { get; set; } = DateOnly.FromDateTime(DateTime.Today);

		[Required(ErrorMessage = "Popis je povinný")]
		public string Popis
		{
			get => popis;
			set
			{
				if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Popis je povinný", nameof(Popis));
				popis = value;
			}
		}
		[Range(0, double.MaxValue, ErrorMessage = "Výnosy musí být nezáporné")]
		public decimal Vynosy
		{
			get => vynosy;
			set
			{
				if(value <0) 				{
					throw new ArgumentOutOfRangeException(nameof(Vynosy), "Výnosy musí být nezáporné");
				}
				vynosy = value;
			}
		}

		[Range(0, double.MaxValue, ErrorMessage = "Náklady musí být nezáporné")]
		public decimal Naklady
		{
			get => naklady;
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException(nameof(Naklady), "Náklady musí být nezáporné");
				}
				naklady = value;
			}
		}
		public decimal Zisk => Vynosy - Naklady;

		public Transakce Klonovat()
		{
			return new Transakce(this.Datum, this.Popis, this.Vynosy, this.Naklady)
			{
				Id = this.Id
			};
		}

		public void Aktualizovat(Transakce zdroj)
		{
			this.Datum = zdroj.Datum;
			this.Popis = zdroj.Popis;
			this.Vynosy = zdroj.Vynosy;
			this.Naklady = zdroj.Naklady;
		}

	}
}
