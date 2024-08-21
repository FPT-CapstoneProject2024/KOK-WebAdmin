using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using WebAdmin.Context;
using WebAdmin.DTOModels.Filter;
using WebAdmin.DTOModels.Response.Helpers;
using WebAdmin.Pages.Authentication;
using WebAdmin.Services.Interfaces;

namespace WebAdmin.Pages.Package
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> logger;
        private readonly IApiClient apiClient;

        [BindProperty]
        public DynamicModelResponse.DynamicModelsResponse<DTOModels.Response.Package> data { get; set; } = new DynamicModelResponse.DynamicModelsResponse<DTOModels.Response.Package>();
        [BindProperty]
        public int PageSzie { get; set; } = 3;
        [BindProperty]
        public int CurrentPage { get; set; }
        [BindProperty]
        public static int TotalPage { get; set; } = 1;
        [BindProperty]
        public PackageFilter? filter { get; set; }
        [BindProperty]
        public string? search { get; set; }

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

                var uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.PackageResource + "/" + "get-packages";

                var response = await apiClient.GetAsync(uri + "?page=" + CurrentPage + filter);

                var jsonResponse = await response.Content.ReadAsStringAsync();

                data = JsonConvert.DeserializeObject<DynamicModelResponse.DynamicModelsResponse<DTOModels.Response.Package>>(jsonResponse);

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

            return await OnGet(filter: "&filter" + "=" + search);
        }


        public async Task<IActionResult> OnPostSearchAdvanced(string? number = null)
        {

            CurrentPage = int.Parse(number ?? "1");
            CurrentPage = (CurrentPage < 1) ? 1 : CurrentPage;
            CurrentPage = (CurrentPage > TotalPage) ? TotalPage : CurrentPage;

            var uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.PackageResource;
            var queryParams = new Dictionary<string, string>()
            {
                {"PackageName", filter.PackageName },
                {"MoneyAmount", string.Concat(filter.MoneyAmount) },
                {"StarNumber", string.Concat(filter.StarNumber) },
                {"Status", filter.Status },
                {"CreatedDate", string.Concat(filter.CreatedDate) },

            };

            var response = await apiClient.GetAsync(QueryHelpers.AddQueryString(uri, queryParams));



            var jsonResponse = await response.Content.ReadAsStringAsync();
#pragma warning disable CS8601 // Possible null reference assignment.
            data = JsonConvert.DeserializeObject<DynamicModelResponse.DynamicModelsResponse<DTOModels.Response.Package>>(jsonResponse);
#pragma warning restore CS8601 // Possible null reference assignment.

            if (data.Results is not null)
            {
                TotalPage = (int)MathF.Ceiling((float)data.Metadata.Total / (float)data.Metadata.Size);
            }
            return Page();
        }
    }
}