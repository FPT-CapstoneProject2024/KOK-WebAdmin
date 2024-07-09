using System;
using System.Collections.Generic;

namespace WebAdmin.DTOModels.Response
{
    public partial class Song
    {
        public Song()
        {
            InAppTransactions = new HashSet<InAppTransaction>();
            PurchasedSongs = new HashSet<PurchasedSong>();
            Recordings = new HashSet<Recording>();
            Artists = new HashSet<Artist>();
            Genres = new HashSet<Genre>();
            Members = new HashSet<Account>();
            Singers = new HashSet<Singer>();
        }

        public Guid SongId { get; set; }
        public string SongName { get; set; } = null!;
        public string? SongDescription { get; set; }
        public string? SongUrl { get; set; }
        public int SongStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string? SongCode { get; set; }
        public DateTime? PublicDate { get; set; }
        public Guid CreatorId { get; set; }
        public decimal Price { get; set; }

        public virtual Account Creator { get; set; } = null!;
        public virtual ICollection<InAppTransaction> InAppTransactions { get; set; }
        public virtual ICollection<PurchasedSong> PurchasedSongs { get; set; }
        public virtual ICollection<Recording> Recordings { get; set; }

        public virtual ICollection<Artist> Artists { get; set; }
        public virtual ICollection<Genre> Genres { get; set; }
        public virtual ICollection<Account> Members { get; set; }
        public virtual ICollection<Singer> Singers { get; set; }
    }
}
