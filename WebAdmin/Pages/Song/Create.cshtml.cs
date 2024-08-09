using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebAdmin.Context;
using WebAdmin.DTOModels.Response.Helpers;
using WebAdmin.Helpers;
using WebAdmin.Pages.Authentication;
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
        public List<DTOModels.Response.Artist> SearchArtistResults { get; set; } = new List<DTOModels.Response.Artist>();
        [BindProperty]
        public DTOModels.Response.Artist Artist { get; set; } = new DTOModels.Response.Artist();


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
                if(!ModelState.IsValid)
                {
                    return Page();
                }
                Song.CreatorId = LoginModel.AccountId.Value;

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

        public IActionResult OnGetSearchAritst(string query)
        {
            var uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.AccountResource + "?email=" + query;
            var response = apiClient.GetAsync(uri).Result;
            var jsonResponse = response.Content.ReadAsStringAsync().Result;

            SearchArtistResults = JsonConvert.DeserializeObject<DynamicModelResponse.DynamicModelsResponse<DTOModels.Response.Artist>>(jsonResponse)?.Results;

            return Partial("_SearchArtistResults", this);


        }

    }
}
