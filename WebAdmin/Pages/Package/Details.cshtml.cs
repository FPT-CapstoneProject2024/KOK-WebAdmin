using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebAdmin.Context;
using WebAdmin.DTOModels.Response.Helpers;
using WebAdmin.Services.Interfaces;

namespace WebAdmin.Pages.Package
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
        public DTOModels.Response.Package Package { get; set; } = default!;

        public async Task<IActionResult> OnGet(string id)
        {
            var uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.PackageResource;

            var response = await apiClient.GetAsync(uri + "?PackageId=" + id);

            var jsonResponse = await response.Content.ReadAsStringAsync();

            var account = JsonConvert.DeserializeObject<DynamicModelResponse.DynamicModelsResponse<DTOModels.Response.Package>>(jsonResponse).Results.First();


            if (id == null || account == null)
            {
                return NotFound();
            }

            if (account == null)
            {
                return NotFound();
            }
            else
            {
                Package = account;
            }

            return Page();
        }
    }
}
