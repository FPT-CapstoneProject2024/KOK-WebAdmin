using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net;
using WebAdmin.Context;
using WebAdmin.DTOModels.Response.Authentication;
using WebAdmin.Services.Implementation;
using WebAdmin.Services.Interfaces;

namespace WebAdmin.Pages.Admin
{
    public class ProfileModel : PageModel
    {
        //{
        //    private readonly IApiClient _apiClient;

        //    public ProfileModel(IApiClient apiClient)
        //    {
        //        _apiClient = apiClient;
        //    }
        //    public void OnGet()
        //    {
        //        var uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.AuthenticationResource + "/login";
        //        var response = await apiClient.(uri, Credential);

        //        // Successr
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var jsonResponse = await response.Content.ReadAsStringAsync();
        //            var loginResult = JsonConvert.DeserializeObject<LoginResponse>(jsonResponse);
        //        }
        //}
    }
}
