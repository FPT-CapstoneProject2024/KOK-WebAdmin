using System;
using System.Collections.Generic;

namespace WebAdmin.DTOModels.Response
{
    public partial class Genre
    {

        public Guid? GenreId { get; set; }
        public string? GenreName { get; set; }

        public ICollection<SongGenre>? SongGernes { get; set; }
    }
}
