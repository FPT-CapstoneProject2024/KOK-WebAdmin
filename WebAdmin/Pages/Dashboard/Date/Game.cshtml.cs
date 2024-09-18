using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using NuGet.Packaging;
using System.Globalization;
using System.Text.RegularExpressions;
using WebAdmin.Context;
using WebAdmin.Services.Interfaces;

namespace WebAdmin.Pages.Dashboard.Date
{
    public class GameModel : PageModel
    {
        private readonly IApiClient _apiClient;
        public Dictionary<string, decimal>? DataDateInApp = new Dictionary<string, decimal>();
        public static Dictionary<string, decimal>? DataDate { get; set; }
        [BindProperty]
        public DateTime? StartDate { get; set; }
        [BindProperty]
        public DateTime? EndDate { get; set; }

        public static Dictionary<string, decimal>? DataDateOrder { get; set; }

        public GameModel(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }
        public async Task<IActionResult> OnGet(DateTime? startDate = null, DateTime? endDate = null)
        {
            try
            {
                if(startDate == null)
                {
                    startDate = new DateTime(DateTime.Now.Year, 1, 1);
                }
                if(endDate == null)
                {
                    endDate = new DateTime(DateTime.Now.Year, 12, 1, 23, 59, 59); 
                }
                #region MonthInApp


                string uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.DashboardResource + "/" + "get-date-game-transactions?StartDate=" + startDate + "&EndDate=" + endDate;


                var response = await _apiClient.GetAsync(uri);
                var jsonResponse = await response.Content.ReadAsStringAsync();

                //if (DataDateInApp == null || DataDateInApp.Count <= 0)
                //{
                    var data = JsonConvert.DeserializeObject<DTOModels.Response.Helpers.DashboardResponse<string>>(jsonResponse)?.Values;

                    data?.Keys.ToList().ForEach(e =>
                    {
                        DataDateInApp?.Add(Regex.Match(e, @"^\d{4}-\d{2}-\d{2}").Value, data.GetValueOrDefault(e));

                    });

                    if(DataDate == null)
                    {
                        DataDate = DataDateInApp;
                    }


                    if(DataDateOrder == null)
                    {
                        DataDateOrder = DataDateInApp?.OrderByDescending(x => x.Value).ToDictionary(t => t.Key, t => t.Value);
                    }
                   

                //}
                //DataDateInApp = JsonConvert.DeserializeObject<DTOModels.Response.Helpers.DashboardResponse<string>>(jsonResponse)?.Values;





                #endregion
            }
            catch (Exception)
            {
                return RedirectToPage("../../Error");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostSearchMonth()
        {
            return await OnGet(StartDate, EndDate);
        }
    }
}