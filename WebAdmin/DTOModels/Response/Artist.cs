using System;
using System.Collections.Generic;

namespace WebAdmin.DTOModels.Response
{
    public partial class Artist
    {

        public Guid? ArtistId { get; set; }
        public string? ArtistName { get; set; }
        public string? Image { get; set; }

        public  ICollection<SongArtist>? SongArtists { get; set; }
    }
}
