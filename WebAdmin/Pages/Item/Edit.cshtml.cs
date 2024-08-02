using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebAdmin.Context;
using WebAdmin.DTOModels.Response.Helpers;
using WebAdmin.Services.Implementation;
using WebAdmin.Services.Interfaces;

namespace WebAdmin.Pages.Item
{
    public class EditModel : PageModel
    {
        private readonly IApiClient _apiClient;

        public EditModel(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        [BindProperty]
        public DTOModels.Response.Item Item { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {

            if (id == null)
            {
                return NotFound();
            }


            var uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.ItemResource + "?ItemId=" + id;
            var response = await _apiClient.GetAsync(uri);
            var responeJson = await response.Content.ReadAsStringAsync();
            var item = JsonConvert.DeserializeObject<DynamicModelResponse.DynamicModelsResponse<DTOModels.Response.Item>>(responeJson);

            if (item.Results == null)
            {
                return NotFound();
            }
            Item = item.Results.First();
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }



            try
            {
                //_context.Attach(Item).State = EntityState.Modified;
                var uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.ItemResource + "/" + Item.ItemId;
                var response = await _apiClient.PutAsync(uri, Item);
                var responeJson = await response.Content.ReadAsStringAsync();
                var item = JsonConvert.DeserializeObject<ResponseResult<DTOModels.Response.Item>>(responeJson);

                if(item.result.Value == false)
                {
                    return Page();
                }

            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!ItemExists(Item.ItemId))
                //{
                //    return NotFound();
                //}
                //else
                //{
                //    throw;
                //}
                return RedirectToPage("./Error");
            }

            return RedirectToPage("./Index");
        }

        //private bool ItemExists(Guid id)
        //{
        //    return (_context.Items?.Any(e => e.ItemId == id)).GetValueOrDefault();
        //}
    }
}