using System;
using System.Collections.Generic;

namespace WebAdmin.DTOModels.Response
{
    public partial class Singer
    {

        public Guid? SingerId { get; set; }
        public string? SingerName { get; set; }

        public ICollection<SongSinger>? SongSingers { get; set; }
    }
}
