using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebAdmin.Context;
using WebAdmin.DTOModels.Response.Helpers;
using WebAdmin.Services.Interfaces;

namespace WebAdmin.Pages.Song
{
    public class CreateModel : PageModel
    {
        public IApiClient apiClient { get; set; }

        [BindProperty]
        public DTOModels.Request.CreateSongRequestModel Song { get; set; } = new DTOModels.Request.CreateSongRequestModel();
        public CreateModel(IApiClient apiClient)
        {
            this.apiClient = apiClient;
        }
        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.SongResource;

                var response = await apiClient.PostAsync(uri, Song);

                var jsonResponse = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<ResponseResult<DTOModels.Response.Song>>(jsonResponse);

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
