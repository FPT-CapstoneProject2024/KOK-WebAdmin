using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebAdmin.Pages.Dashboard
{
    public class GameModel : PageModel
    {
        public IActionResult OnGet()
        {
            return Page();
        }
    }
}
