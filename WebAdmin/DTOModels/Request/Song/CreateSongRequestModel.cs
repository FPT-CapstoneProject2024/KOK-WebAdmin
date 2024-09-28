using System.ComponentModel.DataAnnotations;
using WebAdmin.Helpers;

namespace WebAdmin.DTOModels.Request.Song
{
    public class CreateSongRequestModel
    {
        [Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        public string SongName { get; set; } = null!;
        public string? SongDescription { get; set; }
        public IFormFile? SongFile { get; set; }
        [FutureOrTodayDateAttribute(ErrorMessage = Constraints.VALIDATE_PUBLICDATE)]
        public DateTime? PublicDate { get; set; }
        [Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        public Guid CreatorId { get; set; }
        [RegularExpression(Constraints.VALIDATE_AMOUNT, ErrorMessage = Constraints.STAR_INVALID)]
        public decimal Price { get; set; }
        [Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        public static ICollection<SongGenreRequestModel>? SongGenres { get; set; } = new List<SongGenreRequestModel>();
        [Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        public static ICollection<SongArtistRequestModel>? SongArtists { get; set; } = new List<SongArtistRequestModel>();
        [Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        public static ICollection<SongSingerRequestModel>? SongSingers { get; set; } = new List <SongSingerRequestModel>();


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
    public class SongArtistRequestModelExtended
    {
        public Guid ArtistId { get; set; }
        public string ArtistName { get; set; } // New field for the artist name
    }
    public class SongSingerRequestModelExtended
    {
        public Guid SingerId { get; set; }
        public string SingerName { get; set; } // New field for the singer name
    }
    public class SongGenreRequestModelExtended
    {
        public Guid GenreId { get; set; }
        public string GenreName { get; set; } // New field for the genre name
    }

    public class CreateSongRequestModel1
    {
        [Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        public string SongName { get; set; } = null!;
        public string? SongDescription { get; set; }
        public string? SongUrl  { get; set; }
        public DateTime? PublicDate { get; set; }
        [Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        public Guid CreatorId { get; set; }
        [RegularExpression(Constraints.VALIDATE_AMOUNT, ErrorMessage = Constraints.STAR_INVALID)]
        public decimal Price { get; set; }
        [Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        public ICollection<SongGenreRequestModelExtended>? SongGenres { get; set; } = new List<SongGenreRequestModelExtended>();
        [Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        public ICollection<SongArtistRequestModelExtended>? SongArtists { get; set; } = new List<SongArtistRequestModelExtended>();
        [Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        public ICollection<SongSingerRequestModelExtended>? SongSingers { get; set; } = new List<SongSingerRequestModelExtended>();
    }
}
