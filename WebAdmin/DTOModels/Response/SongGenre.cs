namespace WebAdmin.DTOModels.Response
{
    public class SongGenre
    {
        public Guid? SongId { get; set; }
        public Guid? GenreId { get; set; }
        public string? SongName { get; set; }
        public string? GenreName { get; set; }
    }
}
