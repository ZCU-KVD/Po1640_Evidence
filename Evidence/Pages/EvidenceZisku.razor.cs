using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Evidence.Pages
{
	public partial class EvidenceZisku
	{
		[Inject] private Services.EvidenceService EvidenceService { get; set; } = default!;

		[Inject] private JSRuntime JS { get; set; } = default!;

		#region Stav komponenty
		private Models.Transakce formularTransakce = new Models.Transakce();
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
			EvidenceService.PridatTransakci(formularTransakce);

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
		#endregion
	}
}
