using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebAdmin.Context;
using WebAdmin.DTOModels.Response.Helpers;
using WebAdmin.Services.Interfaces;

namespace WebAdmin.Pages.Transactions.Monetary
{
    public class DetailsModel : PageModel
    {
        private readonly ILogger<DetailsModel> logger;
        private readonly IApiClient apiClient;

        public DetailsModel(ILogger<DetailsModel> logger, IApiClient apiClient)
        {
            this.logger = logger;
            this.apiClient = apiClient;
        }

        [BindProperty]
        public DTOModels.Response.MonetaryTransaction MonetaryTransaction { get; set; } = default!;

        public async Task<IActionResult> OnGet(string id)
        {
            var uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.MonetaryResource;

            var response = await apiClient.GetAsync(uri + "?MonetaryTransactionId=" + id);

            var jsonResponse = await response.Content.ReadAsStringAsync();
#pragma warning disable CS8601 // Possible null reference assignment.
            var monetary = JsonConvert.DeserializeObject<DynamicModelResponse.DynamicModelsResponse<DTOModels.Response.MonetaryTransaction>>(jsonResponse).Results.First();
#pragma warning restore CS8601 // Possible null reference assignment.

            if (id == null || monetary == null)
            {
                return NotFound();
            }

            if (monetary == null)
            {
                return NotFound();
            }
            else
            {
                MonetaryTransaction = monetary;
            }

            return Page();
        }
    }
}
