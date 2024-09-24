using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebAdmin.Context;
using WebAdmin.DTOModels.Request.Package;
using WebAdmin.DTOModels.Response.Helpers;
using WebAdmin.Pages.Authentication;
using WebAdmin.Services.Implementation;
using WebAdmin.Services.Interfaces;

namespace WebAdmin.Pages.Package
{
    public class EditModel : PageModel
    {
        private readonly IApiClient _apiClient;
        private readonly IMapper _mapper;

        public EditModel(IApiClient apiClient, IMapper mapper)
        {
            _apiClient = apiClient;
            _mapper = mapper;
        }

        [BindProperty]
        public DTOModels.Request.Package.UpdatePackageRequestModel UpdatePackage { get; set; } = default!;
        [BindProperty]
        public DTOModels.Response.Package Package { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.PackageResource + "?PackageId=" + id;
            var response = await _apiClient.GetAsync(uri);
            var responseJson = await response.Content.ReadAsStringAsync();
            var package = JsonConvert.DeserializeObject<DynamicModelResponse.DynamicModelsResponse<DTOModels.Response.Package>>(responseJson);

            if (package.Results == null)
            {
                return NotFound();
            }
            Package = package.Results.First();
            UpdatePackage = _mapper.Map<UpdatePackageRequestModel>(Package);
            Console.WriteLine(UpdatePackage);
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult(new { success = false });
            }

            try
            {
                UpdatePackage.CreatorId = Guid.Parse(HttpContext.Session.GetString("AccountId"));
                var uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.PackageResource + "/" + Package.PackageId;
                var uri2 = KokApiContext.BaseApiUrl + "/" + KokApiContext.PackageResource + "/update-status/" + Package.PackageId;
                if (UpdatePackage.Status == DTOModels.PackageStatus.INACTIVE || UpdatePackage.Status.Equals("Không Hoạt Động"))
                {
                    var response2 = await _apiClient.PutAsync(uri2, "INACTIVE");
                    var responseJson2 = await response2.Content.ReadAsStringAsync();
                    var package2 = JsonConvert.DeserializeObject<ResponseResult<DTOModels.Response.Package>>(responseJson2);
                }
                else if (UpdatePackage.Status == DTOModels.PackageStatus.ACTIVE)
                {
                    var response2 = await _apiClient.PutAsync(uri2, "ACTIVE");
                    var responseJson2 = await response2.Content.ReadAsStringAsync();
                    var package2 = JsonConvert.DeserializeObject<ResponseResult<DTOModels.Response.Package>>(responseJson2);
                }
                var response = await _apiClient.PutAsync(uri, UpdatePackage);
                var responseJson = await response.Content.ReadAsStringAsync();
                var package = JsonConvert.DeserializeObject<ResponseResult<DTOModels.Response.Package>>(responseJson);

                if (package.result.Value)
                {
                    return new JsonResult(new { success = true });
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                return new JsonResult(new { success = false });
            }

            return new JsonResult(new { success = false });
        }
    }
}
