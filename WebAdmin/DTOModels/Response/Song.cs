using System;
using System.Collections.Generic;

namespace WebAdmin.DTOModels.Response
{
    public partial class Song
    {
        public Guid? SongId { get; set; }
        public string? SongName { get; set; }
        public string? SongDescription { get; set; }
        public string? SongUrl { get; set; }
        public SongStatus? SongStatus { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? SongCode { get; set; }
        public DateTime? PublicDate { get; set; }
        public Guid? CreatorId { get; set; }
        public decimal? Price { get; set; }
        public ICollection<string>? Genre { get; set; }
        public ICollection<string>? Singer { get; set; }
        public ICollection<string>? Artist { get; set; }

        public ICollection<SongArtist>? SongArtists { get; set; }
        public ICollection<SongGenre>? SongGenres { get; set; }
        public ICollection<SongSinger>? SongSingers { get; set; }
    }
}
