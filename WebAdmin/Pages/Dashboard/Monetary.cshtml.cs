using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Globalization;
using WebAdmin.Context;
using WebAdmin.DTOModels;
using WebAdmin.Services.Interfaces;

namespace WebAdmin.Pages.Dashboard
{
    public class MonetaryModel : PageModel
    {
        private readonly IApiClient _apiClient;
        //public static Dictionary<string, decimal>? DataMonthInApp { get; set; }
        [BindProperty]
        public string? StartMonth { get; set; }
        [BindProperty]
        public string? EndMonth { get; set; }

        public static Dictionary<string, decimal>? DataMonth { get; set; }
        public static Dictionary<string, decimal>? DataMonthOrder { get; set; }

        public MonetaryModel(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }
        public async Task<IActionResult> OnGet(int? month = null, int? year = null, int? endMonth = null, int? enYear = null)
        {
            try
            {
                if (month == null)
                {
                    month = 1;
                }
                if (endMonth == null)
                {
                    endMonth = 12;
                }
                if (year == null)
                {
                    year = DateTime.Now.Year;
                }
                if (enYear == null)
                {
                    enYear = DateTime.Now.Year;
                }
                string? uri = null;

                #region MonthInApp

                uri = null;


                uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.DashboardResource + "/" + "get-month-transactions?StartMonth=" + month + "&EndMonth=" + endMonth + "&StartYear=" + year + "&EndYear=" + enYear;


                var response = await _apiClient.GetAsync(uri);
                var jsonResponse = await response.Content.ReadAsStringAsync();

                if (DataMonth == null)
                {
                    var dataConvert = JsonConvert.DeserializeObject<DTOModels.Response.Helpers.DashboardResponse<string>>(jsonResponse)?.Values;



                    if(DataMonth is null)
                    {
                        DataMonth = new Months().Values;

                    }
                    int index = 0;
                    /*DataMonth = */DataMonth.ToList().ForEach(x =>
                    {
                        DataMonth[x.Key] = dataConvert.Values.ToList().ElementAt(index);
                        index += 1;
                    });


                    DataMonthOrder = DataMonth?.ToList().OrderByDescending(x => x.Value).ToDictionary(t => t.Key, t => t.Value);


                }
                //DataMonth = JsonConvert.DeserializeObject<DTOModels.Response.Helpers.DashboardResponse<string>>(jsonResponse)?.Values;





                #endregion
            }
            catch (Exception)
            {
                return RedirectToPage("/Error");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostSearchMonth()
        {
            DateTime startMonth = DateTime.ParseExact(StartMonth, "yyyy-MM", CultureInfo.InvariantCulture);
            DateTime endMonth = DateTime.ParseExact(EndMonth, "yyyy-MM", CultureInfo.InvariantCulture);
            return await OnGet(startMonth.Month, startMonth.Year, endMonth.Month, endMonth.Year);
        }
    }
}