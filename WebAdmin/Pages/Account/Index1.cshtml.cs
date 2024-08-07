using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebAdmin.Context;
using WebAdmin.DTOModels.Response.Helpers;
using WebAdmin.Services.Interfaces;

namespace WebAdmin.Pages.Account
{
    public class Index1Model : PageModel
    {
        private readonly ILogger<Index1Model> logger;
        private readonly IApiClient apiClient;

        [BindProperty]
        public DynamicModelResponse.DynamicModelsResponse<DTOModels.Response.Account> data { get; set; } = new DynamicModelResponse.DynamicModelsResponse<DTOModels.Response.Account>();
        [BindProperty]
        public int PageSzie { get; set; } = 3;
        [BindProperty]
        public int CurrentPage { get; set; }
        [BindProperty]
        public static int TotalPage { get; set; } = 1;

        public Index1Model(ILogger<Index1Model> logger, IApiClient apiClient)
        {
            this.logger = logger;
            this.apiClient = apiClient;
        }

        public async Task<IActionResult> OnGet(string? number = null, string? filter = null)
        {

            CurrentPage = int.Parse(number ?? "1");
            CurrentPage = (CurrentPage < 1) ? 1 : CurrentPage;
            CurrentPage = (CurrentPage > TotalPage) ? TotalPage : CurrentPage;

            var uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.AccountResource;

            var response = await apiClient.GetAsync(uri + "?page=" + CurrentPage + filter);



            var jsonResponse = await response.Content.ReadAsStringAsync();
#pragma warning disable CS8601 // Possible null reference assignment.
            data = JsonConvert.DeserializeObject<DynamicModelResponse.DynamicModelsResponse<DTOModels.Response.Account>>(jsonResponse);
#pragma warning restore CS8601 // Possible null reference assignment.

            if (data.Results is not null)
            {
                TotalPage = (int)MathF.Ceiling((float)data.Metadata.Total / (float)data.Metadata.Size);
            }
            return Page();
        }


        public async Task<IActionResult> OnGetInactive(string id)
        {
            var uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.AccountResource + "/" + id;

            var response = await apiClient.DeleteAsync(uri);

            var jsonResponse = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<ResponseResult<DTOModels.Response.Account>>(jsonResponse);

            return await OnGet();
        }

        public async Task<IActionResult> OnGetActive(string id)
        {
            var uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.AccountResource + "/" + "active-account" + "/" + id;

            var response = await apiClient.PutAsync<string>(uri, null);

            var jsonResponse = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<ResponseResult<DTOModels.Response.Account>>(jsonResponse);

            return await OnGet();
        }

        public async Task<IActionResult> OnPostSearch()
        {
            string? filter = Request.Form["txt_filter"];
            string? search = Request.Form["txt_search"];
            return await OnGet(filter: "&" + filter + "=" + search);

        }


    }
}
