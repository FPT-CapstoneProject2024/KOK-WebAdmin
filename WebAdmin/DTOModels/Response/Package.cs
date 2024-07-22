using System;
using System.Collections.Generic;

namespace WebAdmin.DTOModels.Response
{
    public partial class Package
    {
        public Package()
        {
            MonetaryTransactions = new HashSet<MonetaryTransaction>();
        }

        public Guid PackageId { get; set; }
        public string PackageName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal? MoneyAmount { get; set; }
        public int StarNumber { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatorId { get; set; }
        public virtual ICollection<MonetaryTransaction> MonetaryTransactions { get; set; }
    }
}
