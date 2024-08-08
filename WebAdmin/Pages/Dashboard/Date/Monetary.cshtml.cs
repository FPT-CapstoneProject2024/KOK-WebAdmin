using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebAdmin.Context;
using WebAdmin.Services.Interfaces;

namespace WebAdmin.Pages.Dashboard.Date
{
    public class MonetaryModel : PageModel
    {
        private readonly IApiClient _apiClient;
        public static Dictionary<DateTime, decimal>? DataDateInApp { get; set; }
        public static Dictionary<DateTime, decimal>? DataDate { get; set; }
        [BindProperty]
        public DateTime? StartDate { get; set; }
        [BindProperty]
        public DateTime? EndDate { get; set; }

        public static Dictionary<DateTime, decimal>? DataDateOrder { get; set; }

        public MonetaryModel(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }
        public async Task<IActionResult> OnGet(DateTime? startDate = null, DateTime? endDate = null)
        {
            try
            {
                if (startDate == null)
                {
                    startDate = new DateTime(DateTime.Now.Year, 1, 1);
                }
                if (endDate == null)
                {
                    endDate = new DateTime(DateTime.Now.Year, 12, 1, 23, 59, 59);
                }
                #region MonthInApp


                string uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.DashboardResource + "/" + "get-date-transactions?StartDate=" + startDate + "&EndDate=" + endDate;


                var response = await _apiClient.GetAsync(uri);
                var jsonResponse = await response.Content.ReadAsStringAsync();

                if (DataDateInApp == null)
                {
                    DataDateInApp = JsonConvert.DeserializeObject<DTOModels.Response.Helpers.DashboardResponse<DateTime>>(jsonResponse)?.Values;

                    DataDate = DataDateInApp;

                    DataDateOrder = DataDateInApp.OrderByDescending(x => x.Value).ToDictionary(t => t.Key, t => t.Value);


                }
                DataDateInApp = JsonConvert.DeserializeObject<DTOModels.Response.Helpers.DashboardResponse<DateTime>>(jsonResponse)?.Values;





                #endregion
            }
            catch (Exception)
            {
                return RedirectToPage("/../Error");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostSearchMonth()
        {
            return await OnGet(StartDate, EndDate);
        }
    }
}