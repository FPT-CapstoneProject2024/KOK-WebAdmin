using System;
using System.Collections.Generic;

namespace WebAdmin.DTOModels.Response
{
    public partial class Package
    {

        public Guid? PackageId { get; set; }
        public string? PackageName { get; set; }
        public string? Description { get; set; }
        public decimal? MoneyAmount { get; set; }
        public int? StarNumber { get; set; }
        public string? Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? CreatorId { get; set; }
        public ICollection<MonetaryTransaction>? MonetaryTransactions { get; set; }
    }
}
