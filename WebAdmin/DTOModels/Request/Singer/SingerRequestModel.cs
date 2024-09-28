using System.ComponentModel.DataAnnotations;
using WebAdmin.Helpers;

namespace WebAdmin.DTOModels.Request.Singer
{
    public class SingerRequestModel
    {
        [Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        public string SingerName { get; set; } = null!;
        public string? Image { get; set; }
        public IFormFile? file { get; set; }
    }
}
