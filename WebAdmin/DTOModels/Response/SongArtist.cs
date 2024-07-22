namespace WebAdmin.DTOModels.Response
{
    public class SongArtist
    {
        public Guid? SongId { get; set; }
        public Guid? ArtistId { get; set; }
        public string? SongName { get; set; }
        public string? ArtistName { get; set; }
    }
}
