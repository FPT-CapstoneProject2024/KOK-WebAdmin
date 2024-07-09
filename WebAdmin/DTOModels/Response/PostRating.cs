using System;
using System.Collections.Generic;

namespace WebAdmin.DTOModels.Response
{
    public partial class PostRating
    {
        public Guid MemberId { get; set; }
        public Guid PostId { get; set; }
        public int Score { get; set; }

        public virtual Account Member { get; set; } = null!;
        public virtual Post Post { get; set; } = null!;
    }
}
