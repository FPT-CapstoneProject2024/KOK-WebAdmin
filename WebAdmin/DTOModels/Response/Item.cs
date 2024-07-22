using System;
using System.Collections.Generic;

namespace WebAdmin.DTOModels.Response
{
    public partial class Item
    {

        public Guid? ItemId { get; set; }
        public string? ItemCode { get; set; }
        public string? ItemName { get; set; }
        public string? ItemDescription { get; set; }
        public string? ItemType { get; set; }
        public string? ItemStatus { get; set; }
        public bool? CanExpire { get; set; }
        public bool? CanStack { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? CreatorId { get; set; }
        public string? PrefabCode { get; set; }

        public decimal? ItemBuyPrice { get; set; }
        public decimal? ItemSellPrice { get; set; }



        public ICollection<InAppTransaction>? InAppTransactions { get; set; }
        public ICollection<AccountItem>? AccountItems { get; set; }
    }
    }
