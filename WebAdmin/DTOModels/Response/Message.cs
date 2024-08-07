﻿using System;
using System.Collections.Generic;

namespace WebAdmin.DTOModels.Response
{
    public partial class Message
    {
        public Guid MessageId { get; set; }
        public string Content { get; set; } = null!;
        public DateTime TimeStamp { get; set; }
        public Guid SenderId { get; set; }
        public Guid ConversationId { get; set; }

        public virtual Conversation Conversation { get; set; } = null!;
        public virtual Account Sender { get; set; } = null!;
    }
}
