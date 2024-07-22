using System;
using System.Collections.Generic;

namespace WebAdmin.DTOModels.Response
{
    public partial class SupportRequest
    {
        public SupportRequest()
        {
            Conversations = new HashSet<Conversation>();
        }

        public Guid RequestId { get; set; }
        public string Problem { get; set; } = null!;
        public DateTime CreateTime { get; set; }
        public int Category { get; set; }
        public int Status { get; set; }
        public Guid SenderId { get; set; }
        public ICollection<Conversation> Conversations { get; set; }
    }
}
