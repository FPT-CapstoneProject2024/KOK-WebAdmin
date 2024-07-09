using System;
using System.Collections.Generic;

namespace WebAdmin.DTOModels.Response
{
    public partial class Friend
    {
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }
        public int Status { get; set; }

        public virtual Account Receiver { get; set; } = null!;
        public virtual Account Sender { get; set; } = null!;
    }
}
