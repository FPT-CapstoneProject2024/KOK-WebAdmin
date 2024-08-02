using WebAdmin.DTOModels.Response;

namespace WebAdmin.DTOModels.Request
{
    public class CreateSongRequestModel
    {
        public string SongName { get; set; } = null!;
        public string? SongDescription { get; set; }
        public string? SongUrl { get; set; }
        public DateTime? PublicDate { get; set; }
        public Guid CreatorId { get; set; }
        public decimal Price { get; set; }


        public ICollection<SongArtistRequestModel> SongArtists { get; set; } = null!;
        public ICollection<SongGenrneRequestModel> SongGenres { get; set; } = null!;
        public ICollection<SongSingerRequestModel> SongSingers { get; set; } = null!;
    }

    public class SongGenrneRequestModel
    {
        public Guid GenreId { get; set; }
    }
    public class SongArtistRequestModel
    {
        public Guid ArtistId { get; set; }
    }
    public class SongSingerRequestModel
    {
        public Guid SingId { get; set; }
    }
}
