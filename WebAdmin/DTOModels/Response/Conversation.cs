using System;
using System.Collections.Generic;

namespace WebAdmin.DTOModels.Response
{
    public partial class Conversation
    {

        public Guid? ConversationId { get; set; }
        public Guid? MemberId1 { get; set; }
        public Guid? MemberId2 { get; set; }
        public string? ConversationType { get; set; }
        public Guid? SupportRequestId { get; set; }
        public  ICollection<Message>? Messages { get; set; }
    }
}
