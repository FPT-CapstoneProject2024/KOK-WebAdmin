namespace WebAdmin.DTOModels.Request.Report
{
    public class UpdateReportRequestModel
    {
        public Guid ReportId { get; set; }
        public string ReportStatus { get; set; } = null!;
    }
}
