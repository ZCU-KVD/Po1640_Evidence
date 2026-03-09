using Microsoft.AspNetCore.Components;

namespace Evidence.Pages
{
	public partial class EvidenceZisku
	{
		[Inject] private Services.EvidenceService EvidenceService { get; set; } = default!;

		protected override void OnInitialized()
		{
			if(EvidenceService.TransakceSeznam.Count == 0)
			{
				EvidenceService.VygenerovatNahodnaData(30);

			}
		}
	}
}
