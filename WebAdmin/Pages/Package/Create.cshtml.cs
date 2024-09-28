using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using WebAdmin.Context;
using WebAdmin.DTOModels.Response.Helpers;
using WebAdmin.Pages.Authentication;
using WebAdmin.Services.Interfaces;

namespace WebAdmin.Pages.Package
{
    public class CreateModel : PageModel
    {
        public IApiClient apiClient { get; set; }

        [BindProperty]
        public DTOModels.Request.Package.CreatePackageRequestModel Package { get; set; } = new DTOModels.Request.Package.CreatePackageRequestModel();
        public List<DTOModels.Response.Account>? SearchResults { get; set; }

        public CreateModel(IApiClient apiClient)
        {
            this.apiClient = apiClient;
        }
        public void OnGet()
        {
        }

        /*public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.PackageResource;
                Package.CreatorId = HttpContext.Session.GetString("AccountId");
                var response = await apiClient.PostAsync(uri, Package);

                var jsonResponse = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<ResponseResult<DTOModels.Response.Package>>(jsonResponse);

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
        }*/

        public async Task<IActionResult> OnPostAsync()
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
                var uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.PackageResource;
                Package.CreatorId = JsonConvert.DeserializeObject<DTOModels.Response.Account>(HttpContext.Request.Cookies["AccountData"])?.AccountId;
                var response = await apiClient.PostAsync(uri, Package);

                var jsonResponse = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<ResponseResult<DTOModels.Response.Package>>(jsonResponse);

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
