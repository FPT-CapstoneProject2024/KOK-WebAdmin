using System;
using System.Collections.Generic;

namespace WebAdmin.DTOModels.Response
{
    public partial class PurchasedSong
    {
        public Guid? PurchasedSongId { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public Guid? MemberId { get; set; }
        public Guid? SongId { get; set; }
        public Guid? InAppTransactionId { get; set; }

    }
}
