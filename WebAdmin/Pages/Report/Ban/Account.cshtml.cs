using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
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
                var uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.AccountResource + "/" + id;

                var response = await apiClient.GetAsync(uri);

                var jsonResponse = await response.Content.ReadAsStringAsync();

                data = JsonConvert.DeserializeObject<ResponseResult<DTOModels.Response.Account>>(jsonResponse).Value;

              
            }
            catch (Exception)
            {
                return RedirectToPage("/Error");
            }
            
            return Page();
        }

        public async Task<IActionResult> OnPostBanAccount(string id)
        {

            try
            {
                var uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.AccountResource + "/" + id;
                var response = await apiClient.DeleteAsync(uri);

                var jsonResponse = await response.Content.ReadAsStringAsync();

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
