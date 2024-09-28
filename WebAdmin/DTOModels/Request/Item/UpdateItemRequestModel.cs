﻿using System.ComponentModel.DataAnnotations;
using WebAdmin.Helpers;

namespace WebAdmin.DTOModels.Request.Item
{
    public class UpdateItemRequestModel
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
        public int ItemBuyPrice { get; set; }
        [RegularExpression(Constraints.VALIDATE_AMOUNT, ErrorMessage = Constraints.STAR_INVALID)]
        public int ItemSellPrice { get; set; }
        public ItemStatus ItemStatus { get; set; }
        public bool CanExpire { get; set; }
        public bool CanStack { get; set; }
        public Guid? CreatorId { get; set; }
        [Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        public string PrefabCode { get; set; } = null!;
    }
}
