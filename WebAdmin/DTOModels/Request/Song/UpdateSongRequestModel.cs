using System.ComponentModel.DataAnnotations;
using WebAdmin.Helpers;

namespace WebAdmin.DTOModels.Request.Song
{
    public class UpdateSongRequestModel
    {
        [Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        public string SongName { get; set; } = null!;
        public string? SongDescription { get; set; }
        public string? SongUrl { get; set; }
        public string? SongCode { get; set; }
        public DateTime? PublicDate { get; set; }
        //[Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        public Guid? CreatorId { get; set; }
        [RegularExpression(Constraints.VALIDATE_AMOUNT, ErrorMessage = Constraints.STAR_INVALID)]
        public int Price { get; set; }
        //[Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        public string? Category { get; set; }
        //[Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        public string? Author { get; set; } 
        //[Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        public string? Singer { get; set; }
    }
}
