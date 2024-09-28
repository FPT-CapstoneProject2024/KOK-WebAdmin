using System.ComponentModel.DataAnnotations;
using WebAdmin.Helpers;

namespace WebAdmin.DTOModels.Request.Item
{
    public class CreateItemRequestModel
    {
        [Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        [StringLength(10, ErrorMessage = Constraints.ITEM_CODE)]
        public string ItemCode { get; set; } = null!;
        [Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        public string ItemName { get; set; } = null!;
        [Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        public string ItemDescription { get; set; } = null!;
        public ItemType ItemType { get; set; }
        [RegularExpression(Constraints.VALIDATE_AMOUNT, ErrorMessage = Constraints.UP_INVALID)]
        public decimal ItemBuyPrice { get; set; }
        [RegularExpression(Constraints.VALIDATE_AMOUNT, ErrorMessage = Constraints.ITEM_PRICE)]
        public decimal? ItemSellPrice { get; set; }
        public bool? CanExpire { get; set; } = true;
        public bool? CanStack { get; set; } = true;
        public Guid? CreatorId { get; set; }
        public string PrefabCode { get; set; } = null!;
    }
}
