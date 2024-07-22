using System;
using System.Collections.Generic;

namespace WebAdmin.DTOModels.Response
{
    public partial class Singer
    {
        public Singer()
        {
            Songs = new HashSet<Song>();
        }

        public Guid SingerId { get; set; }
        public string SingerName { get; set; } = null!;

        public ICollection<Song> Songs { get; set; }
    }
}
