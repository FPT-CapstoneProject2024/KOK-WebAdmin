using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Security.Cryptography.X509Certificates;
using WebAdmin.Context;
using WebAdmin.Services.Interfaces;

namespace WebAdmin.Pages.Dashboard
{
    public class IndexModel : PageModel
    {
        public class DashboardResponse<TEntity>
        {
            public string? Message { get; set; }
            public Dictionary<TEntity, decimal>? Values { get; set; }
        }
        private readonly IApiClient _apiClient;

        [BindProperty]
        public Dictionary<string, decimal>? DataMonth { get; set; }
        [BindProperty]
        public Dictionary<DateTime, decimal>? DataDate { get; set; }

        public IndexModel(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }
        public async Task<IActionResult> OnGet()
        {
            try
            {
                #region Month
                var uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.DashboardResource + "/" + "get-month-transactions";

                var response = await _apiClient.GetAsync(uri);
                var jsonResponse = await response.Content.ReadAsStringAsync();

                DataMonth = JsonConvert.DeserializeObject<DashboardResponse<string>>(jsonResponse)?.Values;
                #endregion

                #region Date
                var uriDate = KokApiContext.BaseApiUrl + "/" + KokApiContext.DashboardResource + "/" + "get-date-transactions";

                var responseDate = await _apiClient.GetAsync(uriDate);
                var jsonResponseDate = await responseDate.Content.ReadAsStringAsync();

                DataDate = JsonConvert.DeserializeObject<DashboardResponse<DateTime>>(jsonResponseDate)?.Values;
                #endregion
            }
            catch (Exception)
            {
                return RedirectToPage("../Error");
            }
            return Page();
        }
    }
}