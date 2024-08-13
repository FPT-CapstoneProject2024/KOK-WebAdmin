using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebAdmin.Context;
using WebAdmin.DTOModels.Response.Helpers;
using WebAdmin.Pages.Account;
using WebAdmin.Services.Implementation;
using WebAdmin.Services.Interfaces;

namespace WebAdmin.Pages.Item
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> logger;
        private readonly IApiClient apiClient;

        [BindProperty]
        public DynamicModelResponse.DynamicModelsResponse<DTOModels.Response.Item> data { get; set; } = new DynamicModelResponse.DynamicModelsResponse<DTOModels.Response.Item>();
        [BindProperty]
        public int PageSzie { get; set; } = 3;
        [BindProperty]
        public int CurrentPage { get; set; }
        [BindProperty]
        public static int TotalPage { get; set; } = 1;

        public IndexModel(ILogger<IndexModel> logger, IApiClient apiClient)
        {
            this.logger = logger;
            this.apiClient = apiClient;
        }

        public async Task<IActionResult> OnGet(string? number = null, string? filter = null)
        {
            try
            {
                CurrentPage = int.Parse(number ?? "1");
                CurrentPage = (CurrentPage < 1) ? 1 : CurrentPage;
                CurrentPage = (CurrentPage > TotalPage) ? TotalPage : CurrentPage;

                var uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.ItemResource + "/" + "get-items";

                var response = await apiClient.GetAsync(uri + "?page=" + CurrentPage + filter);



                var jsonResponse = await response.Content.ReadAsStringAsync();
#pragma warning disable CS8601 // Possible null reference assignment.
                data = JsonConvert.DeserializeObject<DynamicModelResponse.DynamicModelsResponse<DTOModels.Response.Item>>(jsonResponse);
#pragma warning restore CS8601 // Possible null reference assignment.

                if (data.Results is not null)
                {
                    TotalPage = (int)MathF.Ceiling((float)data.Metadata.Total / (float)data.Metadata.Size);
                }
            }
            catch (Exception)
            {
                return RedirectToPage("/Error");
            }

            return Page();
        }


        public async Task<IActionResult> OnPostSearch()
        {
            //string? filter = Request.Form["txt_filter"];
            string? search = Request.Form["txt_search"];
            return await OnGet(filter: "&filter"  + "=" + search);

        }

    }
}
