using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebAdmin.Context;
using WebAdmin.DTOModels.Response.Helpers;
using WebAdmin.Helpers;
using WebAdmin.Services.Interfaces;
namespace WebAdmin.Pages.Song
{
    public class CreateModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;
        private string? imageUrl { get; set; }
        public IApiClient apiClient { get; set; }

        [BindProperty]
        public DTOModels.Request.Song.CreateSongRequestModel Song { get; set; } = new DTOModels.Request.Song.CreateSongRequestModel();


        public CreateModel(IApiClient apiClient, IHttpClientFactory clientFactory)
        {
            this.apiClient = apiClient;
            _clientFactory = clientFactory;
        }
        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(IFormFile file)
        {
            try
            {
                await UploadImage(file);
                if(imageUrl == null)
                {
                    return Page();
                }

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


        public async Task UploadImage(IFormFile file)
        {
            var rs = await SupportingFeature.Instance.UploadImage(_clientFactory, file, KokApiContext.ImgurClientId);
            if (!rs.Item1)
            {
                ViewData["Message"] = rs.Item2;
            }
            else
            {
                ViewData["ImageUrl"] = rs.Item2;
                imageUrl = rs.Item2;
                
            }
        }

    }
}
