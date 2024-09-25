using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebAdmin.Context;
using WebAdmin.DTOModels;
using WebAdmin.DTOModels.Filter;
using WebAdmin.DTOModels.Response.Helpers;
using WebAdmin.Services.Interfaces;

namespace WebAdmin.Pages.Report
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> logger;
        private readonly IApiClient apiClient;

        [BindProperty]
        public DynamicModelResponse.DynamicModelsResponse<DTOModels.Response.Report> data { get; set; } = new DynamicModelResponse.DynamicModelsResponse<DTOModels.Response.Report>();
        [BindProperty]
        public int PageSzie { get; set; } = 3;
        [BindProperty]
        public int CurrentPage { get; set; }
        [BindProperty]
        public static int TotalPage { get; set; } = 1;
        [BindProperty]
        public int TotalPage2 { get; set; }
        [BindProperty]
        public ReportFilter? filter { get; set; }
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


                var uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.ReportResource + "/" + "get-reports";

                var response = await apiClient.GetAsync(uri + "?page=" + CurrentPage + filter);

                var jsonResponse = await response.Content.ReadAsStringAsync();

                data = JsonConvert.DeserializeObject<DynamicModelResponse.DynamicModelsResponse<DTOModels.Response.Report>>(jsonResponse);

                data.Results = data.Results.Select(x =>
                {
                    x.ReportCategory = ReportCategory.List[(int)Enum.Parse(typeof(ReportCatagory), x.ReportCategory)];
                    x.Status = ReportStatuses.List[(int)Enum.Parse(typeof(ReportStatus), x.Status)];
                    x.ReportType = ReportTypes.List[(int)Enum.Parse(typeof(ReportType), x.ReportType)];
                    return x;
                }).ToList();

                if (data.Results is not null)
                {
                    TotalPage = (int)MathF.Ceiling((float)data.Metadata.Total / (float)data.Metadata.Size);
                    TotalPage2 = TotalPage;
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
           // string? search = Request.Form["txt_search"];
            ViewData["filter_search"] = search;
            return await OnGet(filter: "&filter" + "=" + search);
        }


        public async Task<IActionResult> OnPostSearchAdvanced(string? number = null)
        {

            CurrentPage = int.Parse(number ?? "1");
            CurrentPage = (CurrentPage < 1) ? 1 : CurrentPage;
            CurrentPage = (CurrentPage > TotalPage) ? TotalPage : CurrentPage;

            var uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.ReportResource;
            var queryParams = new Dictionary<string, string>()
            {
                {"ReportCategory", string.Concat(ReportCategory.List.IndexOf(filter.ReportCategory)) },
                {"Status", string.Concat(ReportStatuses.List.IndexOf(filter.Status)) },
                {"Reason" , filter.Reason },
                {"CreateTime", string.Concat(filter.CreateTime) },
                {"ReportType", string.Concat(ReportTypes.List.IndexOf(filter.ReportType)) },
                {"Comment", filter.Comment },
                {"PostCaption", filter.PostCaption },

            };

            var response = await apiClient.GetAsync(QueryHelpers.AddQueryString(uri, queryParams));



            var jsonResponse = await response.Content.ReadAsStringAsync();
#pragma warning disable CS8601 // Possible null reference assignment.
            data = JsonConvert.DeserializeObject<DynamicModelResponse.DynamicModelsResponse<DTOModels.Response.Report>>(jsonResponse);
#pragma warning restore CS8601 // Possible null reference assignment.

            if (data.Results is not null)
            {
                TotalPage = (int)MathF.Ceiling((float)data.Metadata.Total / (float)data.Metadata.Size);
                TotalPage2 = TotalPage;
            }
            return Page();
        }
    }



}
