namespace WebAdmin.DTOModels.Filter
{
    public class ReportFilter
    {

        public string? ReportCategory { get; set; }
        public string? Status { get; set; }
        public string? Reason { get; set; }
        public DateTime? CreateTime { get; set; }
        public string? ReportType { get; set; }
        public string? Comment { get; set; }
        public string? PostCaption { get; set; }
    }
}
