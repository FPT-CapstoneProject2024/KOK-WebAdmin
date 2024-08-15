using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebAdmin.Context;
using WebAdmin.DTOModels.Response.Helpers;
using WebAdmin.Services.Interfaces;

namespace WebAdmin.Pages.Report
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
        public DTOModels.Response.Report Report { get; set; } = default!;

        public async Task<IActionResult> OnGet(string id)
        {
            var uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.ReportResource;

            var response = await apiClient.GetAsync(uri + "?ReportId=" + id);

            var jsonResponse = await response.Content.ReadAsStringAsync();

            var report = JsonConvert.DeserializeObject<DynamicModelResponse.DynamicModelsResponse<DTOModels.Response.Report>>(jsonResponse).Results.First();


            if (id == null || report == null)
            {
                return NotFound();
            }

            if (report == null)
            {
                return NotFound();
            }
            else
            {
                Report = report;
            }

            return Page();
        }
    }
}
