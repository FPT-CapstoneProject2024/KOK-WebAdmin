using System;
using System.Collections.Generic;

namespace WebAdmin.DTOModels.Response
{
    public partial class MonetaryTransaction
    {

        public Guid? MonetaryTransactionId { get; set; }
        public string? PaymentType { get; set; }
        public string? PaymentCode { get; set; } 
        public decimal? MoneyAmount { get; set; }
        public string? Currency { get; set; }
        public string? Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? PackageId { get; set; }
        public Guid? MemberId { get; set; }

        public ICollection<InAppTransaction>? InAppTransactions { get; set; }
    }
}
