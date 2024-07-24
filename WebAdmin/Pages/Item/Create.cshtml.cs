using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebAdmin.Context;
using WebAdmin.DTOModels.Response.Helpers;
using WebAdmin.Services.Interfaces;

namespace WebAdmin.Pages.Item
{
    public class CreateModel : PageModel
    {
        public IApiClient apiClient { get; set; }

        [BindProperty]
        public DTOModels.Response.Item Item { get; set; } = new DTOModels.Response.Item();
        private readonly ILogger<CreateModel> logger;
        public CreateModel(IApiClient apiClient, ILogger<CreateModel> logger)
        {
            this.apiClient = apiClient;
            this.logger = logger;
        }
        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            var uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.ItemResource;

            var response = await apiClient.PostAsync(uri, Item);

            var jsonResponse = await response.Content.ReadAsStringAsync();
#pragma warning disable CS8601 // Possible null reference assignment.
            var data = JsonConvert.DeserializeObject<ResponseResult<DTOModels.Response.Item>>(jsonResponse);
#pragma warning restore CS8601 // Possible null reference assignment.

            if (data.result.Value)
            {
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
