using System;
using System.Collections.Generic;

namespace WebAdmin.DTOModels.Response
{
    public partial class LoginActivity
    {
        public Guid? LoginId { get; set; }
        public DateTime? LoginTime { get; set; }
        public string? LoginDevice { get; set; }
        public Guid? MemberId { get; set; }
    }
}
