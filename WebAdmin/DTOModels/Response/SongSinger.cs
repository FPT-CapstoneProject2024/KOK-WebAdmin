namespace WebAdmin.DTOModels.Response
{
    public class SongSinger
    {
        public Guid? SongId { get; set; }
        public Guid? SingerId { get; set; }
        public string? SongName { get; set; }
        public string? SingerName { get; set; }
    }
}
