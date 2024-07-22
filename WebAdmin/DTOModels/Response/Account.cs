﻿namespace WebAdmin.DTOModels.Response
{
    public partial class Account
    {

        public Guid? AccountId { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }   
        public string? Email { get; set; }
        public string? Gender { get; set; }
        public string? Role { get; set; }
        public bool? IsOnline { get; set; }
        public string? Fullname { get; set; }
        public int? Yob { get; set; }
        public string? IdentityCardNumber { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? CreatedTime { get; set; }
        public Guid? CharacterItemId { get; set; }
        public Guid? RoomItemId { get; set; }
        public string? AccountStatus { get; set; }
        public decimal? UpBalance { get; set; }

        public ICollection<AccountItem>? AccountItems { get; set; }
        public ICollection<Conversation>? ConversationMemberId1Navigations { get; set; }
        public ICollection<Conversation>? ConversationMemberId2Navigations { get; set; }
        public ICollection<Friend>? FriendReceivers { get; set; }
        public ICollection<Friend>? FriendSenders { get; set; }
        public ICollection<InAppTransaction>? InAppTransactions { get; set; }
        public ICollection<Item>? Items { get; set; }
        public ICollection<KaraokeRoom>? KaraokeRooms { get; set; }
        public ICollection<LoginActivity>? LoginActivities { get; set; }
        public ICollection<Message>? Messages { get; set; }
        public ICollection<MonetaryTransaction>? MonetaryTransactions { get; set; }
        public ICollection<Package>? Packages { get; set; }
        public ICollection<PostComment>? PostComments { get; set; }
        public ICollection<PostRating>? PostRatings { get; set; }
        public ICollection<PostShare>? PostShares { get; set; }
        public ICollection<Post>? Posts { get; set; }
        public ICollection<PurchasedSong>? PurchasedSongs { get; set; }
        public ICollection<Recording>? RecordingHosts { get; set; }
        public ICollection<Recording>? RecordingOwners { get; set; }
        public ICollection<Report>? ReportReportedAccounts { get; set; }
        public ICollection<Report>? ReportReporters { get; set; }
        public ICollection<Song>? SongsNavigation { get; set; }
        public ICollection<SupportRequest>? SupportRequests { get; set; }
        public ICollection<VoiceAudio>? VoiceAudios { get; set; }

        public ICollection<Song> Songs { get; set; }
    }
}
