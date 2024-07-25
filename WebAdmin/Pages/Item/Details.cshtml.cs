using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebAdmin.Context;
using WebAdmin.DTOModels.Response.Helpers;
using WebAdmin.Services.Interfaces;

namespace WebAdmin.Pages.Item
{
    public class DetailsModel : PageModel
    {
        private readonly ILogger<DetailsModel> logger;
        private readonly IApiClient apiClient;

        //private readonly EXE201_LEARNING_ENGLISH_DataLayer.Models.EXE201_LEARNING_ENGLISHContext _context;
        public DetailsModel(ILogger<DetailsModel> logger, IApiClient apiClient)
        {
            this.logger = logger;
            this.apiClient = apiClient;
        }

        [BindProperty]
        public DTOModels.Response.Item Item { get; set; } = default!;

        public async Task<IActionResult> OnGet(string id)
        {
            var uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.ItemResource;

            var response = await apiClient.GetAsync(uri + "?ItemId=" + id);

            var jsonResponse = await response.Content.ReadAsStringAsync();
#pragma warning disable CS8601 // Possible null reference assignment.
            var account = JsonConvert.DeserializeObject<DynamicModelResponse.DynamicModelsResponse<DTOModels.Response.Item>>(jsonResponse).Results.First();
#pragma warning restore CS8601 // Possible null reference assignment.

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
                Item = account;
            }

            return Page();
        }
    }
}
