using System.ComponentModel.DataAnnotations;
using WebAdmin.Helpers;

namespace WebAdmin.DTOModels.Request.Package
{
    public class CreatePackageRequestModel
    {
        [Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        public string PackageName { get; set; } = null!;
        public string? Description { get; set; }
        [RegularExpression(Constraints.VALIDATE_AMOUNT, ErrorMessage = Constraints.STAR_INVALID)]
        public decimal MoneyAmount { get; set; }
        [RegularExpression(Constraints.VALIDATE_AMOUNT, ErrorMessage = Constraints.UP_INVALID)]
        public int StarNumber { get; set; }
        //public PackageStatus Status { get; set; } = PackageStatus.INACTIVE;
        public Guid? CreatorId { get; set; }
    }
}
