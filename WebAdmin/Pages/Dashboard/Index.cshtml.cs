using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging.Abstractions;
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

        //[BindProperty]
        public Dictionary<string, decimal>? DataMonth { get; set; }
        //[BindProperty]
        public Dictionary<DateTime, decimal>? DataDate { get; set; }
        //[BindProperty]
        public Dictionary<string, decimal>? DataMonthInApp { get; set; }
        //[BindProperty]
        public Dictionary<DateTime, decimal>? DataDateInApp { get; set; }
        [BindProperty]
        public string? Month { get; set; }

        public IndexModel(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }
        public async Task<IActionResult> OnGet(string? Month = null, string? searchType = null)
        {
            try
            {
                #region Month


                string? uri = null;

                if ((Month == null || Month.Equals("")) || (searchType == null || searchType.Equals("")))
                {
                    uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.DashboardResource + "/" + "get-month-transactions";


                }
                else
                {
                    if (searchType.Equals("monetary"))
                    {
                        uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.DashboardResource + "/" + "get-month-transactions?Month=" + Month;
                    }
                }

                var response = await _apiClient.GetAsync(uri);
                var jsonResponse = await response.Content.ReadAsStringAsync();

                DataMonth = JsonConvert.DeserializeObject<DashboardResponse<string>>(jsonResponse)?.Values;



                #endregion

                #region Date
                uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.DashboardResource + "/" + "get-date-transactions";

                response = await _apiClient.GetAsync(uri);
                jsonResponse = await response.Content.ReadAsStringAsync();

                DataDate = JsonConvert.DeserializeObject<DashboardResponse<DateTime>>(jsonResponse)?.Values;
                #endregion

                #region MonthInApp

                uri = null;


                if ((Month == null || Month.Equals("")) || (searchType == null || searchType.Equals("")))
                {
                    uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.DashboardResource + "/" + "get-month-game-transactions";
                }
                else
                {
                    if (searchType.Equals("in_app"))
                    {
                        uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.DashboardResource + "/" + "get-month-game-transactions?Month=" + Month;
                    }
                }

                response = await _apiClient.GetAsync(uri);
                jsonResponse = await response.Content.ReadAsStringAsync();

                DataMonthInApp = JsonConvert.DeserializeObject<DashboardResponse<string>>(jsonResponse)?.Values;
                #endregion
            }
            catch (Exception)
            {
                return RedirectToPage("../Error");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostSearchMonth()
        {
            string? searchType = Request.Form["seach_transaction"];

            return await OnGet(Month, searchType);
        }
    }
}
