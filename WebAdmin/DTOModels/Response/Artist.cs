using System;
using System.Collections.Generic;

namespace WebAdmin.DTOModels.Response
{
    public partial class Artist
    {
        public Artist()
        {
            Songs = new HashSet<Song>();
        }

        public Guid ArtistId { get; set; }
        public string ArtistName { get; set; } = null!;

        public virtual ICollection<Song> Songs { get; set; }
    }
}
