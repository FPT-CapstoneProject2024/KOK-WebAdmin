using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebAdmin.Context;
using WebAdmin.DTOModels.Response.Helpers;
using WebAdmin.Services.Interfaces;

namespace WebAdmin.Pages.Genre
{
    public class DeleteModel : PageModel
    {
        public IApiClient apiClient { get; set; }
        public DeleteModel(IApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        [BindProperty]
        public DTOModels.Response.Genre Genre { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.GenreResource + "?GenreId=" + id;
                var response = await apiClient.GetAsync(uri);
                var responeJson = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<DynamicModelResponse.DynamicModelsResponse<DTOModels.Response.Genre>>(responeJson);

                if (data == null)
                {
                    return NotFound();
                }
                else
                {
                    Genre = data.Results.First();
                }
            }
            catch (Exception)
            {
                return RedirectToPage("./Error");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {

            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.GenreResource + "/" + id;
                var response = await apiClient.DeleteAsync(uri);
                var responeJson = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<ResponseResult<DTOModels.Response.Genre>>(responeJson);

                if (!data.result.Value)
                {
                    return BadRequest();
                }
                else
                {
                    //Item = data.Value;
                    return RedirectToPage("./Index");
                }
            }
            catch (Exception)
            {
                return RedirectToPage("./Error");
            }

            return Page();
        }
    }
}
