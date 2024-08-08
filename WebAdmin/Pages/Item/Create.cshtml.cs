using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebAdmin.Context;
using WebAdmin.DTOModels.Response.Helpers;
using WebAdmin.Services.Interfaces;

namespace WebAdmin.Pages.Item
{
    public class CreateModel : PageModel
    {
        public IApiClient apiClient { get; set; }

        [BindProperty]
        public DTOModels.Response.Item Item { get; set; } = new DTOModels.Response.Item();
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
                var uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.ItemResource;

                var response = await apiClient.PostAsync(uri, Item);

                var jsonResponse = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<ResponseResult<DTOModels.Response.Item>>(jsonResponse);

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

        public IActionResult OnGetSearch(string query)
        {
            var uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.AccountResource + "?email=" + query;
            var response = apiClient.GetAsync(uri).Result;
            var jsonResponse = response.Content.ReadAsStringAsync().Result;

            SearchResults = JsonConvert.DeserializeObject<DynamicModelResponse.DynamicModelsResponse<DTOModels.Response.Account>>(jsonResponse)?.Results;

            return Partial("_SearchResults", this);

           
        }
    }
}
