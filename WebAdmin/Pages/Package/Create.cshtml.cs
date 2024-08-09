using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebAdmin.Context;
using WebAdmin.DTOModels.Response.Helpers;
using WebAdmin.Pages.Authentication;
using WebAdmin.Services.Interfaces;

namespace WebAdmin.Pages.Package
{
    public class CreateModel : PageModel
    {
        public IApiClient apiClient { get; set; }

        [BindProperty]
        public DTOModels.Response.Package Package { get; set; } = new DTOModels.Response.Package();
        public List<DTOModels.Response.Account>? SearchResults { get; set; }

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
                var uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.PackageResource;

                var response = await apiClient.PostAsync(uri, Package);

                var jsonResponse = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<ResponseResult<DTOModels.Response.Package>>(jsonResponse);

                if (data.result.Value)
                {
                    return RedirectToPage("./Index");
                }
            }
            catch (Exception)
            {
                return RedirectToPage("/Error");
            }

            return Page();
        }
    }
}
