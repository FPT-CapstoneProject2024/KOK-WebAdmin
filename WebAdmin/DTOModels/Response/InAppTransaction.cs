using System;
using System.Collections.Generic;

namespace WebAdmin.DTOModels.Response
{
    public partial class InAppTransaction
    {

        public Guid? InAppTransactionId { get; set; }
        public string? Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? TransactionType { get; set; }
        public Guid? MemberId { get; set; }
        public string? UserName { get; set; }
        public Guid? ItemId { get; set; }
        public Guid? SongId { get; set; }
        public decimal? UpAmountBefore { get; set; }
        public Guid? MonetaryTransactionId { get; set; }
        public decimal? UpTotalAmount { get; set; }
    }
}
