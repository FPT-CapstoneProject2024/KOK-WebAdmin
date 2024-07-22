using System;
using System.Collections.Generic;

namespace WebAdmin.DTOModels.Response
{
    public partial class Conversation
    {
        public Conversation()
        {
            Messages = new HashSet<Message>();
        }

        public Guid ConversationId { get; set; }
        public Guid MemberId1 { get; set; }
        public Guid MemberId2 { get; set; }
        public int ConversationType { get; set; }
        public Guid SupportRequestId { get; set; }
        public  ICollection<Message> Messages { get; set; }
    }
}
