using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebAdmin.Context;
using WebAdmin.DTOModels.Response.Helpers;
using WebAdmin.Services.Interfaces;

namespace WebAdmin.Pages.Singer
{
    public class DeleteModel : PageModel
    {
        public IApiClient apiClient { get; set; }
        public DeleteModel(IApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        [BindProperty]
        public DTOModels.Response.Singer Singer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.SingerResource + "?SingerId=" + id;
                var response = await apiClient.GetAsync(uri);
                var responeJson = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<DynamicModelResponse.DynamicModelsResponse<DTOModels.Response.Singer>>(responeJson);

                if (data == null)
                {
                    return NotFound();
                }
                else
                {
                    Singer = data.Results.First();
                }
            }
            catch (Exception)
            {
                return RedirectToPage("./Error");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {

            try
            {
                if (id == null)
                {
                    return new JsonResult(new { success = false });
                }

                var uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.SingerResource + "/" + id;
                var response = await apiClient.DeleteAsync(uri);
                var responeJson = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<ResponseResult<DTOModels.Response.Singer>>(responeJson);

                if (!data.result.Value)
                {
                    return new JsonResult(new { success = false });
                }
                else
                {
                    //Item = data.Value;
                    return new JsonResult(new { success = true });
                }
            }
            catch (Exception)
            {
                return new JsonResult(new { success = false });
            }
        }
    }
}
