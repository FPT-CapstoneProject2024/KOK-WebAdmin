using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebAdmin.Context;
using WebAdmin.DTOModels;
using WebAdmin.DTOModels.Response.Helpers;
using WebAdmin.Services.Interfaces;

namespace WebAdmin.Pages.Transactions.Details
{
    public class MonetaryModel : PageModel
    {
        private readonly ILogger<MonetaryModel> logger;
        private readonly IApiClient apiClient;

        public MonetaryModel(ILogger<MonetaryModel> logger, IApiClient apiClient)
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
                monetary.CreatedDate = monetary.CreatedDate.Value.AddHours(7);
                MonetaryTransaction = monetary;
            }

            return Page();
        }
    }
}
