using System.ComponentModel.DataAnnotations;
using WebAdmin.Helpers;

namespace WebAdmin.DTOModels.Request.Account
{
    public class CreateAccountRequestModel
    {
        [Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        public string UserName { get; set; } = null!;
        [Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        public string Password { get; set; } = null!;
        [EmailAddress]
        public string Email { get; set; } = null!;
        public AccountGender? Gender { get; set; }
        ///public bool IsVerified { get; set; }
        public AccountRole Role { get; set; }
        [RegularExpression(Constraints.VALIDATE_AMOUNT, ErrorMessage = Constraints.STAR_INVALID)]
        public decimal UpBalance { get; set; }
        //public bool? IsOnline { get; set; } = false;3
        public string? Fullname { get; set; }
        [InRangeOneHundredAttribute(ErrorMessage = Constraints.INFORMATION_INVALID)]
        public int? Yob { get; set; }
        [RegularExpression(Constraints.VALIDATE_NEGATIVE, ErrorMessage = Constraints.INFORMATION_INVALID)]
        public string? IdentityCardNumber { get; set; }
        [RegularExpression(Constraints.VALIDATE_NEGATIVE, ErrorMessage = Constraints.INFORMATION_INVALID)]
        public string? PhoneNumber { get; set; }
        public Guid? CharacterItemId { get; set; }
        public Guid? RoomItemId { get; set; }
        public AccountStatus AccountStatus { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
    }
}
