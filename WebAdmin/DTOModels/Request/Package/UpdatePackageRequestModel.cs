using System.ComponentModel.DataAnnotations;
using WebAdmin.Helpers;

namespace WebAdmin.DTOModels.Request.Package
{
    public class UpdatePackageRequestModel
    {
        [Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        public string PackageName { get; set; } = null!;
        public string Description { get; set; } = null!;
        [RegularExpression(Constraints.VALIDATE_AMOUNT, ErrorMessage = Constraints.VALIDATE_AMOUNT)]
        public decimal MoneyAmount { get; set; }
        [RegularExpression(Constraints.VALIDATE_AMOUNT, ErrorMessage = Constraints.STAR_INVALID)]
        public int StarNumber { get; set; }
        public PackageStatus Status { get; set; }
        public Guid? CreatorId { get; set; }
    }
}
