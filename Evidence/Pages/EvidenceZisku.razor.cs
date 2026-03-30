using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Evidence.Pages
{
	public partial class EvidenceZisku
	{
		[Inject] private Services.EvidenceService EvidenceService { get; set; } = default!;

		[Inject] private IJSRuntime JS { get; set; } = default!;

		#region Stav komponenty
		private Models.Transakce formularTransakce = new Models.Transakce();
		private Models.Transakce? originalEditovaneTransakce = null;

		private bool JeEditace => originalEditovaneTransakce != null;
		#endregion

		protected override void OnInitialized()
		{
			if(EvidenceService.TransakceSeznam.Count == 0)
			{
				EvidenceService.VygenerovatNahodnaData(5);

			}
		}

		#region Formulář a RCUD
		private void UlozitTransakci()
		{
			if (!JeEditace)
			{
				EvidenceService.PridatTransakci(formularTransakce);
			}
			else 
			{
				//originalEditovaneTransakce.Datum = formularTransakce.Datum;
				//originalEditovaneTransakce.Popis = formularTransakce.Popis;
				EvidenceService.AktualizujTransakci(originalEditovaneTransakce!, formularTransakce);
				originalEditovaneTransakce = null;
			}


			formularTransakce = new Models.Transakce();
		}

		private async Task SmazatTransakci(Models.Transakce transakce) 
		{
			bool potvrzeni = await JS.InvokeAsync<bool>("confirm", $"Opravdu chcete smazat transakci z {transakce.Datum} s popisem '{transakce.Popis}'?");
			if (potvrzeni)
			{
				EvidenceService.SmazatTransakci(transakce);
			}
		}

		private void EditovatTransakci(Models.Transakce transakce)
		{
			originalEditovaneTransakce = transakce;
			formularTransakce = transakce.Klonovat();
			//formularTransakce.Datum = transakce.Datum;
			//formularTransakce.Naklady = transakce.Naklady;
		}

		private void ZrusitEditaci() 
		{ 
			formularTransakce = new Models.Transakce();
			originalEditovaneTransakce = null;
		}
		#endregion
	}
}
