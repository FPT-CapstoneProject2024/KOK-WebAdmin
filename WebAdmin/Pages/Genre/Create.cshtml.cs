using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebAdmin.Context;
using WebAdmin.DTOModels.Response.Helpers;
using WebAdmin.Helpers;
using WebAdmin.Services.Interfaces;

namespace WebAdmin.Pages.Genre
{
    public class CreateModel : PageModel
    {
        public IApiClient apiClient { get; set; }

        private readonly IHttpClientFactory _clientFactory;

        [BindProperty]
        public DTOModels.Request.Genre.GenreRequestModel Genre { get; set; } = new DTOModels.Request.Genre.GenreRequestModel();
        //public List<DTOModels.Response.Account>? SearchResults { get; set; }
        private static string? imageUrl { get; set; }

        public CreateModel(IApiClient apiClient, IHttpClientFactory clientFactory)
        {
            this.apiClient = apiClient;
            _clientFactory = clientFactory;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync(IFormFile file)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }

                var dataImage = await SupportingFeature.Instance.UploadImage(_clientFactory, file, KokApiContext.ImgurClientId);

                if (!dataImage.Item1)
                {
                    ViewData["Message"] = dataImage.Item2;
                    return Page();
                }
                else
                {
                    ViewData["ImageUrl"] = dataImage.Item2;
                    imageUrl = dataImage.Item2;
                }

                Genre.Image = imageUrl;

                var uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.GenreResource;

                var response = await apiClient.PostAsync(uri, Genre);

                var jsonResponse = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<ResponseResult<DTOModels.Response.Genre>>(jsonResponse);

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
