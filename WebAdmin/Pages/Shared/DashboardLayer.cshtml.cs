using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebAdmin.DTOModels.Response;

namespace WebAdmin.Pages.Shared
{
    public class DashboardLayerModel : PageModel
    {
        public DTOModels.Response.Account Account { get; set; }
        public async Task<IActionResult> OnGet()
        {
            Account = JsonConvert.DeserializeObject<DTOModels.Response.Account>(HttpContext.Session.GetString("AccountData"));

            return Page();
        }
    }
}
