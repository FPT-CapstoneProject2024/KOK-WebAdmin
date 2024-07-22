using System;
using System.Collections.Generic;

namespace WebAdmin.DTOModels.Response
{
    public partial class Report
    {
        public Guid? ReportId { get; set; }
        public Guid? ReporterId { get; set; }
        public Guid? ReportedAccountId { get; set; }
        public string? ReportCategory { get; set; }
        public string? Status { get; set; }
        public string? Reason { get; set; }
        public DateTime? CreateTime { get; set; }
        public string? ReportType { get; set; }
        public Guid? CommentId { get; set; }
        public Guid? PostId { get; set; }
        public Guid? RoomId { get; set; }

    }
}
