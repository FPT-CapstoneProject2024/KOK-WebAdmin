using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebAdmin.Context;
using WebAdmin.DTOModels.Request.Report;
using WebAdmin.DTOModels.Response.Helpers;
using WebAdmin.Services.Interfaces;

namespace WebAdmin.Pages.Report
{
    public class EditModel : PageModel
    {
        private readonly IApiClient _apiClient;
        private readonly IMapper _mapper;


        public EditModel(IApiClient apiClient, IMapper mapper)
        {
            _apiClient = apiClient;
            _mapper = mapper;
        }
        [BindProperty]
        public UpdateReportRequestModel UpdateReport { get; set; }
        //[BindProperty]
        //public string Status { get; set; }
        //[BindProperty]
        public DTOModels.Response.Report Report { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.ReportResource + "?ReportId=" + id;
            var response = await _apiClient.GetAsync(uri);
            var responseJson = await response.Content.ReadAsStringAsync();
            var report = JsonConvert.DeserializeObject<DynamicModelResponse.DynamicModelsResponse<DTOModels.Response.Report>>(responseJson);

            if (report.Results == null)
            {
                return NotFound();
            }
            Report = report.Results.First();
            UpdateReport = _mapper.Map<UpdateReportRequestModel>(Report);
            //Console.WriteLine(UpdateReport);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult(new { success = false });
            }

            try
            {
                var uri = KokApiContext.BaseApiUrl + "/" + KokApiContext.ReportResource + "/update-status/" + UpdateReport.ReportId;
                var response = await _apiClient.PutAsync(uri, UpdateReport.ReportStatus);
                var responseJson = await response.Content.ReadAsStringAsync();
                var report = JsonConvert.DeserializeObject<ResponseResult<DTOModels.Response.Report>>(responseJson);

                if (report.result.Value)
                {
                    return new JsonResult(new { success = true });
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                return new JsonResult(new { success = false });
            }

            return new JsonResult(new { success = false });
        }
    }
}
