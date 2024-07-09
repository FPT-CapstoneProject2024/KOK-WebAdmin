using System;
using System.Collections.Generic;

namespace WebAdmin.DTOModels.Response
{
    public partial class Genre
    {
        public Genre()
        {
            Songs = new HashSet<Song>();
        }

        public Guid GenreId { get; set; }
        public string GenreName { get; set; } = null!;

        public virtual ICollection<Song> Songs { get; set; }
    }
}
