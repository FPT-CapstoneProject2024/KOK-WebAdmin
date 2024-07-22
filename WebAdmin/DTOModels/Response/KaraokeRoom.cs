using System;
using System.Collections.Generic;

namespace WebAdmin.DTOModels.Response
{
    public partial class KaraokeRoom
    {
        public Guid? RoomId { get; set; }
        public string? RoomLog { get; set; } = null!;
        public DateTime? CreateTime { get; set; }
        public Guid? CreatorId { get; set; }

        public  ICollection<Recording>? Recordings { get; set; }
        public  ICollection<Report>? Reports { get; set; }
    }
}
