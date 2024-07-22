using System;
using System.Collections.Generic;

namespace WebAdmin.DTOModels.Response
{
    public partial class SupportRequest
    {
        public Guid? RequestId { get; set; }
        public string? Problem { get; set; }
        public DateTime? CreateTime { get; set; }
        public string? Category { get; set; }
        public string? Status { get; set; }
        public Guid? SenderId { get; set; }
        public ICollection<Conversation>? Conversations { get; set; }
    }
}
