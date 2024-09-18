using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json;
using System.Security.Cryptography.X509Certificates;
using WebAdmin.Context;
using WebAdmin.DTOModels;
using WebAdmin.Services.Interfaces;

namespace WebAdmin.Pages.Dashboard
{
    public class IndexModel : PageModel
    {
        private readonly IApiClient _apiClient;

        //[BindProperty]
        public Dictionary<string, decimal>? DataMonth { get; set; }
        //[BindProperty]
        public Dictionary<DateTime, decimal>? DataDate { get; set; }
        //[BindProperty]
        public Dictionary<string, decimal>? DataMonthInApp { get; set; }
        //[BindProperty]
        public Dictionary<DateTime, decimal>? DataDateInApp { get; set; }

        public IndexModel(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }
        public async Task<IActionResult> OnGet()
        {
            try
            {
                #region Month
                string? uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.DashboardResource + "/" + "get-month-transactions";

                var response = await _apiClient.GetAsync(uri);
                var jsonResponse = await response.Content.ReadAsStringAsync();

                var dataMonthConvert = JsonConvert.DeserializeObject<DTOModels.Response.Helpers.DashboardResponse<string>>(jsonResponse)?.Values;

                DataMonth = new Months().Values;
                int index = 0;

                DataMonth.ToList().ForEach(e =>
                {
                    DataMonth[e.Key] = dataMonthConvert.Values.ToList()[index];
                    index += 1;
                });

                #endregion

                #region Date
                uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.DashboardResource + "/" + "get-date-transactions";

                response = await _apiClient.GetAsync(uri);
                jsonResponse = await response.Content.ReadAsStringAsync();

                DataDate = JsonConvert.DeserializeObject<DTOModels.Response.Helpers.DashboardResponse<DateTime>>(jsonResponse)?.Values;
                #endregion

                #region MonthInApp


                uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.DashboardResource + "/" + "get-month-game-transactions";

                response = await _apiClient.GetAsync(uri);
                jsonResponse = await response.Content.ReadAsStringAsync();

                dataMonthConvert = JsonConvert.DeserializeObject<DTOModels.Response.Helpers.DashboardResponse<string>>(jsonResponse)?.Values;

                DataMonthInApp = new Months().Values;
                index = 0;

                DataMonth.ToList().ForEach(e =>
                {
                    DataMonthInApp[e.Key] = dataMonthConvert.Values.ToList()[index];
                    index += 1;
                });
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
