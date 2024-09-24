using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.DotNet.MSIdentity.Shared;
using Newtonsoft.Json;
using WebAdmin.Context;
using WebAdmin.DTOModels.Response.Helpers;
using WebAdmin.Helpers;
using WebAdmin.Pages.Authentication;
using WebAdmin.Services.Interfaces;

namespace WebAdmin.Pages.Account.Profile
{
    public class ProfileModel : PageModel
    {
        private readonly IApiClient _apiClient;
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _clientFactory;

        public ProfileModel(IApiClient apiClient, IMapper mapper, IHttpClientFactory clientFactory)
        {
            _apiClient = apiClient;
            _mapper = mapper;
            _clientFactory = clientFactory;
        }
        [BindProperty]
        public WebAdmin.DTOModels.Request.Account.UpdateAccountRequestModel Account { get; set; }
        [BindProperty]
        public WebAdmin.DTOModels.Response.Account Profile { get; set; }
        public async Task<IActionResult> OnGet()
        {
            
            try
            {
                var uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.AccountResource + "/" + HttpContext.Session.GetString("AccountId");

                var response = await _apiClient.GetAsync(uri);

                var jsonResponse = await response.Content.ReadAsStringAsync();
#pragma warning disable CS8601 // Possible null reference assignment.
                Profile = JsonConvert.DeserializeObject<ResponseResult<DTOModels.Response.Account>>(jsonResponse)?.Value;

                Account = _mapper.Map<DTOModels.Request.Account.UpdateAccountRequestModel>(Profile);

#pragma warning restore CS8601 // Possible null reference assignment.
            }
            catch (Exception)
            {
                return RedirectToPage("/Error");

            }

            return Page();
        }

        public async Task<IActionResult> OnPostUpdateProfile(IFormFile? file)
        {
            try
            {
                if(file != null)
                {
                    var dataImage = await SupportingFeature.Instance.UploadImage(_clientFactory, file, KokApiContext.ImgurClientId);

                    if (!dataImage.Item1)
                    {
                        return Page();
                    }
                    
                    Account.Image = dataImage.Item2 ?? string.Empty;
                }

                var uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.AccountResource + "/update-account/" + HttpContext.Session.GetString("AccountId");

                var response = await _apiClient.PutAsync(uri, Account);

                var jsonResponse = await response.Content.ReadAsStringAsync();

                var rs = JsonConvert.DeserializeObject<ResponseResult<DTOModels.Response.Account>>(jsonResponse);

                if (!rs.result.Value)
                {

                    throw new Exception();
                }

                TempData["SuccessMessage"] = Constraints.UPDATE_PROFILE_SUCCESS;
                


            }
            catch (Exception)
            {
                TempData["FailedMessage"] = Constraints.UPDATE_PROFILE_ERROR;
                return Page();
            }

            return Page();
        }
    }
}
