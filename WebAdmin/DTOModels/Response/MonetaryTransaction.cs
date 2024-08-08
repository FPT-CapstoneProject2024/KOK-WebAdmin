using System;
using System.Collections.Generic;

namespace WebAdmin.DTOModels.Response
{
    public class MonetaryTransaction
    {
        public Guid? MonetaryTransactionId { get; set; }
        public PaymentType? PaymentType { get; set; }
        public string? PaymentCode { get; set; }
        public decimal? MoneyAmount { get; set; }
        public decimal? PackageMoneyAmount { get; set; }
        public string? Currency { get; set; }
        public PaymentStatus? Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? PackageId { get; set; }
        public string? PackageName { get; set; }
        public Guid? MemberId { get; set; }
        public string? Username { get; set; }
    }
}
