using System;
using System.Collections.Generic;

namespace WebAdmin.DTOModels.Response
{
    public partial class MonetaryTransaction
    {
        public MonetaryTransaction()
        {
            InAppTransactions = new HashSet<InAppTransaction>();
        }

        public Guid MonetaryTransactionId { get; set; }
        public int PaymentType { get; set; }
        public string PaymentCode { get; set; } = null!;
        public decimal MoneyAmount { get; set; }
        public string Currency { get; set; } = null!;
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid PackageId { get; set; }
        public Guid MemberId { get; set; }

        public virtual Account Member { get; set; } = null!;
        public virtual Package Package { get; set; } = null!;
        public virtual ICollection<InAppTransaction> InAppTransactions { get; set; }
    }
}
