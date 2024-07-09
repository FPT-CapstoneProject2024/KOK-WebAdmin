using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebAdmin.Context;
using WebAdmin.Services.Implementation;
using WebAdmin.Services.Interfaces;

namespace WebAdmin.Pages.Account
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public WebAdmin.DTOModels.Response.Account Account { get; set; } = new WebAdmin.DTOModels.Response.Account();

        private IApiClient _client;
        
        public CreateModel(IApiClient client)
        {
            _client = client;   
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostCreate()
        {
            try
            {
                var uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.AccountResource;
                var response = await _client.PostAsync(uri, Account);
                var jsonResponse = response.Content.ReadAsStringAsync();
                var data = JsonConvert.SerializeObject(jsonResponse);


            }
            catch (Exception)
            {
                return RedirectToPage("/Error");
            }
            return RedirectToPage("/Index");
        }
    }
}
