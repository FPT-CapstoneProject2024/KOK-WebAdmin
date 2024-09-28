using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebAdmin.Context;
using WebAdmin.DTOModels;
using WebAdmin.DTOModels.Response;
using WebAdmin.DTOModels.Response.Helpers;
using WebAdmin.Services.Interfaces;

namespace WebAdmin.Pages.Report.Ban
{
    public class PostModel : PageModel
    {
        private readonly IApiClient apiClient;
        [BindProperty]
        public DTOModels.Response.Report Report{ get; set; } = new DTOModels.Response.Report();
        [BindProperty]
        public DTOModels.Response.Post data { get; set; } = default!;

        public PostModel(IApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public async Task<IActionResult> OnGet(string id)
        {
            try
            {

                var uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.PostResource + "/get-posts-for-admin/?PostId=" + id;

                var response = await apiClient.GetAsync(uri);

                var jsonResponse = await response.Content.ReadAsStringAsync();

                data = JsonConvert.DeserializeObject<DynamicModelResponse.DynamicModelsResponse<DTOModels.Response.Post>>(jsonResponse).Results.First();

                uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.ReportResource + "?PostId=" + id;

                response = await apiClient.GetAsync(uri);

                jsonResponse = await response.Content.ReadAsStringAsync();

                Report = JsonConvert.DeserializeObject<DynamicModelResponse.DynamicModelsResponse<DTOModels.Response.Report>>(jsonResponse).Results.First();


                data.ReportId = string.Concat(Report.ReportId);

                  Report.ReportCategory = ReportCategory.List[(int)Enum.Parse(typeof(ReportCatagory), Report.ReportCategory)];
                Report.Status = ReportStatuses.List[(int)Enum.Parse(typeof(ReportStatus), Report.Status)];
                Report.ReportType = ReportTypes.List[(int)Enum.Parse(typeof(ReportType), Report.ReportType)];

            }
            catch (Exception)
            {
                return RedirectToPage("/Error");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostBanPost(string id, string reportId)
        {

            try
            {
                var uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.PostResource + "/" + id;
                var response = await apiClient.DeleteAsync(uri);

                var jsonResponse = await response.Content.ReadAsStringAsync();

                //var data1 = JsonConvert.DeserializeObject<ResponseResult<DTOModels.Response.Post>>(jsonResponse);

                uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.ReportResource + "/update-status/" + reportId;

                response = await apiClient.PutAsync(uri, "COMPLETE");
                jsonResponse = await response.Content.ReadAsStringAsync();


                var data1 = JsonConvert.DeserializeObject<ResponseResult<DTOModels.Response.Account>>(jsonResponse);


                if (data1.result.HasValue)
                {
                    if (!data1.result.Value)
                    {
                        throw new Exception();
                    }
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                return RedirectToPage("/Error");
            }

            return RedirectToPage("/Report/Index");
        }
    }
}
