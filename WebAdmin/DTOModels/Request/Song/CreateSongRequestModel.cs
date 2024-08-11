using System.ComponentModel.DataAnnotations;
using WebAdmin.Helpers;

namespace WebAdmin.DTOModels.Request.Song
{
    public class CreateSongRequestModel
    {
        [Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        public string SongName { get; set; } = null!;
        public string? SongDescription { get; set; }
        public string? SongUrl { get; set; }
        public DateTime? PublicDate { get; set; }
        [Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        public Guid CreatorId { get; set; }
        [RegularExpression(Constraints.VALIDATE_AMOUNT, ErrorMessage = Constraints.STAR_INVALID)]
        public decimal Price { get; set; }
        [Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        public ICollection<SongGenreRequestModel>? SongGenres { get; set; } = new List<SongGenreRequestModel>();
        [Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        public ICollection<SongArtistRequestModel>? SongArtists { get; set; } = new List<SongArtistRequestModel>();
        [Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        public ICollection<SongSingerRequestModel>? SongSingers { get; set; } = new List <SongSingerRequestModel>();


    }

    public class SongGenreRequestModel
    {
        public Guid GenreId { get; set; }
    }
    public class SongArtistRequestModel
    {
        public Guid ArtistId { get; set; }
    }
    public class SongSingerRequestModel
    {
        public Guid SingerId { get; set; }
    }

}
