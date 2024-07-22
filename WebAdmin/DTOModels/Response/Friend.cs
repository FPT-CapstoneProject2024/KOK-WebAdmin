using System;
using System.Collections.Generic;

namespace WebAdmin.DTOModels.Response
{
    public partial class Friend
    {
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }
        public int Status { get; set; }
    }
}
