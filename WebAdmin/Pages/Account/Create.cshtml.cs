using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebAdmin.Context;
using WebAdmin.Helpers;
using WebAdmin.Services.Implementation;
using WebAdmin.Services.Interfaces;

namespace WebAdmin.Pages.Account
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public WebAdmin.DTOModels.Request.Account.CreateAccountRequestModel Account { get; set; } = new WebAdmin.DTOModels.Request.Account.CreateAccountRequestModel();

        private IApiClient _client;
        private readonly IHttpClientFactory _clientFactory;
        private static string? imageUrl { get; set; }

        public CreateModel(IApiClient client, IHttpClientFactory clientFactory)
        {
            _client = client;   
            _clientFactory = clientFactory; 
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostCreate(/*IFormFile file*/)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    // Lấy tất cả thông điệp lỗi từ ModelState và gộp thành một chuỗi
                    var errorMessages = ModelState
                        .Where(ms => ms.Value.Errors.Count > 0)
                        .SelectMany(ms => ms.Value.Errors.Select(e => e.ErrorMessage))
                        .ToList();

                    // Gộp tất cả thông điệp thành một chuỗi, có thể sử dụng "\n" để xuống dòng
                    var errors = string.Join("\n", errorMessages);
                    return new JsonResult(new { success = false, message = errors });
                }
                //var dataImage = await SupportingFeature.Instance.UploadImage(_clientFactory, file, KokApiContext.ImgurClientId);

                //if (!dataImage.Item1)
                //{
                //    ViewData["Message"] = dataImage.Item2;
                //    return Page();
                //}
                //else
                //{
                //    ViewData["ImageUrl"] = dataImage.Item2;
                //    imageUrl = dataImage.Item2;
                //}

                //Account.Image = imageUrl;


                var uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.AccountResource;
                var response = await _client.PostAsync(uri, Account);
                var jsonResponse = response.Content.ReadAsStringAsync();
                var data = JsonConvert.SerializeObject(jsonResponse);


            }
            catch (Exception)
            {
                return new JsonResult(new { success = false });
            }
            return new JsonResult(new { success = true });
        }

    }
}
