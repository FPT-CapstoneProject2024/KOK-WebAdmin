using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebAdmin.Context;
using WebAdmin.DTOModels.Response.Helpers;
using WebAdmin.Pages.Authentication;
using WebAdmin.Services.Interfaces;

namespace WebAdmin.Pages.Item
{
    public class CreateModel : PageModel
    {
        public IApiClient apiClient { get; set; }

        [BindProperty]
        public DTOModels.Request.Item.CreateItemRequestModel Item { get; set; } = new DTOModels.Request.Item.CreateItemRequestModel();
        //public List<DTOModels.Response.Account>? SearchResults { get; set; }

        public CreateModel(IApiClient apiClient)
        {
            this.apiClient = apiClient;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return new JsonResult(new { success = false });
                }
                Item.CreatorId = Guid.Parse(HttpContext.Session.GetString("AccountId"));
                var uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.ItemResource;

                var response = await apiClient.PostAsync(uri, Item);

                var jsonResponse = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<ResponseResult<DTOModels.Response.Item>>(jsonResponse);

                if (data.result.Value)
                {
                    return new JsonResult(new { success = true });
                }
            }
            catch (Exception)
            {
                return new JsonResult(new { success = false });
            }

            return new JsonResult(new { success = false });
        }
    }
}
