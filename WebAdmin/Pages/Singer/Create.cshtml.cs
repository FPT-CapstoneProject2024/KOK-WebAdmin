using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebAdmin.Context;
using WebAdmin.DTOModels.Response.Helpers;
using WebAdmin.Helpers;
using WebAdmin.Pages.Authentication;
using WebAdmin.Services.Interfaces;

namespace WebAdmin.Pages.Singer
{
    public class CreateModel : PageModel
    {
        public IApiClient apiClient { get; set; }

        private readonly IHttpClientFactory _clientFactory;

        private static string? imageUrl { get; set; }
        [BindProperty]
        public DTOModels.Request.Singer.SingerRequestModel Singer { get; set; } = new DTOModels.Request.Singer.SingerRequestModel();
        //public List<DTOModels.Response.Account>? SearchResults { get; set; }

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
                //if (!ModelState.IsValid)
                //{
                //    return new JsonResult(new { success = false });
                //}

                var dataImage = await SupportingFeature.Instance.UploadImage(_clientFactory, file, KokApiContext.ImgurClientId);

                if (!dataImage.Item1)
                {
                    ViewData["Message"] = dataImage.Item2;
                    return new JsonResult(new { success = false });
                }
                else
                {
                    ViewData["ImageUrl"] = dataImage.Item2;
                    imageUrl = dataImage.Item2;
                }

                Singer.Image = imageUrl;

                var uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.SingerResource;

                var response = await apiClient.PostAsync(uri, Singer);

                var jsonResponse = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<ResponseResult<DTOModels.Response.Singer>>(jsonResponse);

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
