using System;
using System.Collections.Generic;

namespace WebAdmin.DTOModels.Response
{
    public partial class InAppTransaction
    {
        public InAppTransaction()
        {
            PurchasedSongs = new HashSet<PurchasedSong>();
        }

        public Guid InAppTransactionId { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public int TransactionType { get; set; }
        public Guid MemberId { get; set; }
        public Guid ItemId { get; set; }
        public Guid SongId { get; set; }
        public decimal UpAmountBefore { get; set; }
        public Guid MonetaryTransactionId { get; set; }
        public decimal UpTotalAmount { get; set; }

        public virtual Item Item { get; set; } = null!;
        public virtual Account Member { get; set; } = null!;
        public virtual MonetaryTransaction MonetaryTransaction { get; set; } = null!;
        public virtual Song Song { get; set; } = null!;
        public virtual ICollection<PurchasedSong> PurchasedSongs { get; set; }
    }
}
