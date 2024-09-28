using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Numerics;
using WebAdmin.Context;
using WebAdmin.DTOModels.Response;
using WebAdmin.DTOModels.Response.Helpers;
using WebAdmin.Services.Implementation;
using WebAdmin.Services.Interfaces;

namespace WebAdmin.Pages.Report.Ban
{
    public class AccountModel : PageModel
    {
        private readonly IApiClient apiClient;
        [BindProperty]
        public DTOModels.Response.Account data { get; set; } = default!;

        public AccountModel(IApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public async Task<IActionResult> OnGet(string id)
        {
            try
            {
                var uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.ReportResource + "?ReportId=" + id;

                var response = await apiClient.GetAsync(uri);

                var jsonResponse = await response.Content.ReadAsStringAsync();

                var report = JsonConvert.DeserializeObject<DynamicModelResponse.DynamicModelsResponse<DTOModels.Response.Report>>(jsonResponse).Results.First();

                uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.AccountResource + "/" + report.ReportedAccountId;

                response = await apiClient.GetAsync(uri);

                jsonResponse = await response.Content.ReadAsStringAsync();

                data = JsonConvert.DeserializeObject<ResponseResult<DTOModels.Response.Account>>(jsonResponse).Value;

                data.CommentId = string.Concat(report.CommentId);
                data.ReportId = string.Concat(report.ReportId);
            }
            catch (Exception)
            {
                return RedirectToPage("/Error");
            }
            
            return Page();
        }

        public async Task<IActionResult> OnPostBanAccount(string id, string reportId, string commentId)
        {

            try
            {

                var uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.PostCommentResource + "/" +commentId;

                var response = await apiClient.DeleteAsync(uri);

                var jsonResponse = await response.Content.ReadAsStringAsync();

                //var report = JsonConvert.DeserializeObject<ResponseResult<DTOModels.Response.Report>>(jsonResponse).Value;

                uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.AccountResource + "/" + id;

                response = await apiClient.DeleteAsync(uri);

                jsonResponse = await response.Content.ReadAsStringAsync();


                
                uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.ReportResource + "/update-status/" + reportId;

                response = await apiClient.PutAsync(uri, "COMPLETE");
                jsonResponse = await response.Content.ReadAsStringAsync();


                var reported = JsonConvert.DeserializeObject<ResponseResult<DTOModels.Response.Account>>(jsonResponse);

                if (reported.result.HasValue)
                {
                    if (!reported.result.Value)
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
