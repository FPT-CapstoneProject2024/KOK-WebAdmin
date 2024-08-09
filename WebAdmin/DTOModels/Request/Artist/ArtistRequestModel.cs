using System.ComponentModel.DataAnnotations;
using WebAdmin.Helpers;

namespace WebAdmin.DTOModels.Request.Artist
{
    public class ArtistRequestModel
    {
        //[Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        public string ArtistName { get; set; }
        public string? Image { get; set; }
    }
}
