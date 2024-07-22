using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebAdmin.DTOModels.Response
{
    public partial class Kok_DatabaseContext : DbContext
    {
        public Kok_DatabaseContext()
        {
        }

        public Kok_DatabaseContext(DbContextOptions<Kok_DatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<AccountItem> AccountItems { get; set; } = null!;
        public virtual DbSet<Artist> Artists { get; set; } = null!;
        public virtual DbSet<Conversation> Conversations { get; set; } = null!;
        public virtual DbSet<Friend> Friends { get; set; } = null!;
        public virtual DbSet<Genre> Genres { get; set; } = null!;
        public virtual DbSet<InAppTransaction> InAppTransactions { get; set; } = null!;
        public virtual DbSet<Item> Items { get; set; } = null!;
        public virtual DbSet<KaraokeRoom> KaraokeRooms { get; set; } = null!;
        public virtual DbSet<LoginActivity> LoginActivities { get; set; } = null!;
        public virtual DbSet<Message> Messages { get; set; } = null!;
        public virtual DbSet<MonetaryTransaction> MonetaryTransactions { get; set; } = null!;
        public virtual DbSet<Package> Packages { get; set; } = null!;
        public virtual DbSet<Post> Posts { get; set; } = null!;
        public virtual DbSet<PostComment> PostComments { get; set; } = null!;
        public virtual DbSet<PostRating> PostRatings { get; set; } = null!;
        public virtual DbSet<PostShare> PostShares { get; set; } = null!;
        public virtual DbSet<PurchasedSong> PurchasedSongs { get; set; } = null!;
        public virtual DbSet<Recording> Recordings { get; set; } = null!;
        public virtual DbSet<Report> Reports { get; set; } = null!;
        public virtual DbSet<Singer> Singers { get; set; } = null!;
        public virtual DbSet<Song> Songs { get; set; } = null!;
        public virtual DbSet<SupportRequest> SupportRequests { get; set; } = null!;
        public virtual DbSet<VoiceAudio> VoiceAudios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=MSI\\SQLEXPRESS01;Uid=sa;Pwd=1234;Database=Kok_Database;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");

                entity.HasIndex(e => e.CharacterItemId, "IX_Account_character_item_id");

                entity.HasIndex(e => e.RoomItemId, "IX_Account_room_item_id");

                entity.Property(e => e.AccountId)
                    .HasColumnName("account_id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.AccountStatus).HasColumnName("account_status");

                entity.Property(e => e.CharacterItemId).HasColumnName("character_item_id");

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasColumnName("created_time");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Fullname)
                    .HasMaxLength(150)
                    .HasColumnName("fullname");

                entity.Property(e => e.Gender).HasColumnName("gender");

                entity.Property(e => e.IdentityCardNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("identity_card_number");

                entity.Property(e => e.IsOnline).HasColumnName("is_online");

                entity.Property(e => e.Password)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("phone_number");

                entity.Property(e => e.Role).HasColumnName("role");

                entity.Property(e => e.RoomItemId).HasColumnName("room_item_id");

                entity.Property(e => e.UpBalance)
                    .HasColumnType("money")
                    .HasColumnName("up_balance");

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("user_name");

                entity.Property(e => e.Yob).HasColumnName("yob");

                entity.HasOne(d => d.CharacterItem)
                    .WithMany(p => p.AccountCharacterItems)
                    .HasForeignKey(d => d.CharacterItemId)
                    .HasConstraintName("FK__Account__charact__7C4F7684");

                entity.HasOne(d => d.RoomItem)
                    .WithMany(p => p.AccountRoomItems)
                    .HasForeignKey(d => d.RoomItemId)
                    .HasConstraintName("FK__Account__room_it__7D439ABD");

                entity.HasMany(d => d.Songs)
                    .WithMany(p => p.Members)
                    .UsingEntity<Dictionary<string, object>>(
                        "FavouriteSong",
                        l => l.HasOne<Song>().WithMany().HasForeignKey("SongId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Favourite__song___03F0984C"),
                        r => r.HasOne<Account>().WithMany().HasForeignKey("MemberId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Favourite__membe__02FC7413"),
                        j =>
                        {
                            j.HasKey("MemberId", "SongId").HasName("PK__Favourit__68C8DFD514CDDEC7");

                            j.ToTable("FavouriteSong");

                            j.HasIndex(new[] { "SongId" }, "IX_FavouriteSong_song_id");

                            j.IndexerProperty<Guid>("MemberId").HasColumnName("member_id");

                            j.IndexerProperty<Guid>("SongId").HasColumnName("song_id");
                        });
            });

            modelBuilder.Entity<AccountItem>(entity =>
            {
                entity.ToTable("AccountItem");

                entity.HasIndex(e => e.ItemId, "IX_AccountItem_item_id");

                entity.HasIndex(e => e.MemberId, "IX_AccountItem_member_id");

                entity.Property(e => e.AccountItemId)
                    .HasColumnName("account_item_id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.ActivateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("activate_date");

                entity.Property(e => e.ExpirationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("expiration_date");

                entity.Property(e => e.ItemId).HasColumnName("item_id");

                entity.Property(e => e.ItemStatus).HasColumnName("item_status");

                entity.Property(e => e.MemberId).HasColumnName("member_id");

                entity.Property(e => e.ObtainMethod).HasColumnName("obtain_method");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.AccountItems)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__AccountIn__item___7E37BEF6");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.AccountItems)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__AccountIn__membe__7F2BE32F");
            });

            modelBuilder.Entity<Artist>(entity =>
            {
                entity.ToTable("Artist");

                entity.Property(e => e.ArtistId)
                    .HasColumnName("artist_id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.ArtistName)
                    .HasMaxLength(150)
                    .HasColumnName("artist_name");
            });

            modelBuilder.Entity<Conversation>(entity =>
            {
                entity.ToTable("Conversation");

                entity.HasIndex(e => e.MemberId1, "IX_Conversation_member_id_1");

                entity.HasIndex(e => e.MemberId2, "IX_Conversation_member_id_2");

                entity.HasIndex(e => e.SupportRequestId, "IX_Conversation_support_request_id");

                entity.Property(e => e.ConversationId)
                    .HasColumnName("conversation_id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.ConversationType).HasColumnName("conversation_type");

                entity.Property(e => e.MemberId1).HasColumnName("member_id_1");

                entity.Property(e => e.MemberId2).HasColumnName("member_id_2");

                entity.Property(e => e.SupportRequestId).HasColumnName("support_request_id");

                entity.HasOne(d => d.MemberId1Navigation)
                    .WithMany(p => p.ConversationMemberId1Navigations)
                    .HasForeignKey(d => d.MemberId1)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Conversat__membe__00200768");

                entity.HasOne(d => d.MemberId2Navigation)
                    .WithMany(p => p.ConversationMemberId2Navigations)
                    .HasForeignKey(d => d.MemberId2)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Conversat__membe__01142BA1");

                entity.HasOne(d => d.SupportRequest)
                    .WithMany(p => p.Conversations)
                    .HasForeignKey(d => d.SupportRequestId)
                    .HasConstraintName("FK__Conversat__suppo__02084FDA");
            });

            modelBuilder.Entity<Friend>(entity =>
            {
                entity.HasKey(e => new { e.SenderId, e.ReceiverId })
                    .HasName("PK__Friend__39A74E2FEA2EB197");

                entity.ToTable("Friend");

                entity.HasIndex(e => e.ReceiverId, "IX_Friend_receiver_id");

                entity.Property(e => e.SenderId).HasColumnName("sender_id");

                entity.Property(e => e.ReceiverId).HasColumnName("receiver_id");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Receiver)
                    .WithMany(p => p.FriendReceivers)
                    .HasForeignKey(d => d.ReceiverId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Friend__receiver__04E4BC85");

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.FriendSenders)
                    .HasForeignKey(d => d.SenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Friend__sender_i__05D8E0BE");
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.ToTable("Genre");

                entity.Property(e => e.GenreId)
                    .HasColumnName("genre_id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.GenreName)
                    .HasMaxLength(150)
                    .HasColumnName("genre_name");
            });

            modelBuilder.Entity<InAppTransaction>(entity =>
            {
                entity.ToTable("InAppTransaction");

                entity.HasIndex(e => e.MonetaryTransactionId, "IX_InAppTransaction_MonetaryTransactionId");

                entity.HasIndex(e => e.ItemId, "IX_InAppTransaction_item_id");

                entity.HasIndex(e => e.MemberId, "IX_InAppTransaction_member_id");

                entity.HasIndex(e => e.SongId, "IX_InAppTransaction_song_id");

                entity.Property(e => e.InAppTransactionId)
                    .HasColumnName("in_app_transaction_id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.ItemId).HasColumnName("item_id");

                entity.Property(e => e.MemberId).HasColumnName("member_id");

                entity.Property(e => e.SongId).HasColumnName("song_id");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.TransactionType).HasColumnName("transaction_type");

                entity.Property(e => e.UpAmountBefore)
                    .HasColumnType("money")
                    .HasColumnName("up_amount_before");

                entity.Property(e => e.UpTotalAmount)
                    .HasColumnType("money")
                    .HasColumnName("up_total_amount");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.InAppTransactions)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__InAppTran__item___06CD04F7");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.InAppTransactions)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__InAppTran__membe__07C12930");

                entity.HasOne(d => d.MonetaryTransaction)
                    .WithMany(p => p.InAppTransactions)
                    .HasForeignKey(d => d.MonetaryTransactionId);

                entity.HasOne(d => d.Song)
                    .WithMany(p => p.InAppTransactions)
                    .HasForeignKey(d => d.SongId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__InAppTran__song___08B54D69");
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.ToTable("Item");

                entity.HasIndex(e => e.CreatorId, "IX_Item_creator_id");

                entity.Property(e => e.ItemId)
                    .HasColumnName("item_id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.CanExpire).HasColumnName("can_expire");

                entity.Property(e => e.CanStack).HasColumnName("can_stack");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.CreatorId).HasColumnName("creator_id");

                entity.Property(e => e.ItemCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("item_code");

                entity.Property(e => e.ItemDescription)
                    .HasMaxLength(250)
                    .HasColumnName("item_description");

                entity.Property(e => e.ItemName)
                    .HasMaxLength(50)
                    .HasColumnName("item_name");

                entity.Property(e => e.ItemPrice)
                    .HasColumnType("money")
                    .HasColumnName("item_price");

                entity.Property(e => e.ItemSellPrice)
                    .HasColumnType("money")
                    .HasColumnName("item_sell_price");

                entity.Property(e => e.ItemStatus).HasColumnName("item_status");

                entity.Property(e => e.ItemType).HasColumnName("item_type");

                entity.Property(e => e.PrefabCode)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("prefab_code");

                entity.HasOne(d => d.Creator)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.CreatorId)
                    .HasConstraintName("FK__Item__creator_id__0A9D95DB");
            });

            modelBuilder.Entity<KaraokeRoom>(entity =>
            {
                entity.HasKey(e => e.RoomId)
                    .HasName("PK__KaraokeR__19675A8A6BA327E4");

                entity.ToTable("KaraokeRoom");

                entity.HasIndex(e => e.CreatorId, "IX_KaraokeRoom_creator_id");

                entity.Property(e => e.RoomId)
                    .HasColumnName("room_id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("create_time");

                entity.Property(e => e.CreatorId).HasColumnName("creator_id");

                entity.Property(e => e.RoomLog)
                    .HasColumnType("text")
                    .HasColumnName("room_log");

                entity.HasOne(d => d.Creator)
                    .WithMany(p => p.KaraokeRooms)
                    .HasForeignKey(d => d.CreatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__KaraokeRo__creat__0B91BA14");
            });

            modelBuilder.Entity<LoginActivity>(entity =>
            {
                entity.HasKey(e => e.LoginId)
                    .HasName("PK__LoginAct__C2C971DB811BF7A3");

                entity.ToTable("LoginActivity");

                entity.HasIndex(e => e.MemberId, "IX_LoginActivity_member_id");

                entity.Property(e => e.LoginId)
                    .HasColumnName("login_id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.LoginDevice)
                    .HasMaxLength(150)
                    .HasColumnName("login_device");

                entity.Property(e => e.LoginTime)
                    .HasColumnType("datetime")
                    .HasColumnName("login_time");

                entity.Property(e => e.MemberId).HasColumnName("member_id");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.LoginActivities)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__LoginActi__membe__0C85DE4D");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("Message");

                entity.HasIndex(e => e.ConversationId, "IX_Message_conversation_id");

                entity.HasIndex(e => e.SenderId, "IX_Message_sender_id");

                entity.Property(e => e.MessageId)
                    .HasColumnName("message_id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Content)
                    .HasColumnType("text")
                    .HasColumnName("content");

                entity.Property(e => e.ConversationId).HasColumnName("conversation_id");

                entity.Property(e => e.SenderId).HasColumnName("sender_id");

                entity.Property(e => e.TimeStamp)
                    .HasColumnType("datetime")
                    .HasColumnName("time_stamp");

                entity.HasOne(d => d.Conversation)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.ConversationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Message__convers__0E6E26BF");

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.SenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Message__sender___0F624AF8");
            });

            modelBuilder.Entity<MonetaryTransaction>(entity =>
            {
                entity.ToTable("MonetaryTransaction");

                entity.HasIndex(e => e.MemberId, "IX_MonetaryTransaction_member_id");

                entity.HasIndex(e => e.PackageId, "IX_MonetaryTransaction_package_id");

                entity.Property(e => e.MonetaryTransactionId)
                    .HasColumnName("monetary_transaction_id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.Currency)
                    .HasColumnType("text")
                    .HasColumnName("currency");

                entity.Property(e => e.MemberId).HasColumnName("member_id");

                entity.Property(e => e.MoneyAmount)
                    .HasColumnType("money")
                    .HasColumnName("money_amount");

                entity.Property(e => e.PackageId).HasColumnName("package_id");

                entity.Property(e => e.PaymentCode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("payment_code");

                entity.Property(e => e.PaymentType).HasColumnName("payment_type");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.MonetaryTransactions)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MoneyTran__membe__10566F31");

                entity.HasOne(d => d.Package)
                    .WithMany(p => p.MonetaryTransactions)
                    .HasForeignKey(d => d.PackageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MoneyTran__packa__114A936A");
            });

            modelBuilder.Entity<Package>(entity =>
            {
                entity.ToTable("Package");

                entity.HasIndex(e => e.CreatorId, "IX_Package_creator_id");

                entity.Property(e => e.PackageId)
                    .HasColumnName("package_id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.CreatorId).HasColumnName("creator_id");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .HasColumnName("description");

                entity.Property(e => e.MoneyAmount)
                    .HasColumnType("money")
                    .HasColumnName("money_amount");

                entity.Property(e => e.PackageName)
                    .HasMaxLength(250)
                    .HasColumnName("package_name");

                entity.Property(e => e.StarNumber).HasColumnName("star_number");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Creator)
                    .WithMany(p => p.Packages)
                    .HasForeignKey(d => d.CreatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Package__creator__123EB7A3");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("Post");

                entity.HasIndex(e => e.MemberId, "IX_Post_member_id");

                entity.HasIndex(e => e.OriginPostId, "IX_Post_origin_post_id");

                entity.HasIndex(e => e.RecordingId, "IX_Post_recording_id");

                entity.Property(e => e.PostId)
                    .HasColumnName("post_id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Caption)
                    .HasColumnType("text")
                    .HasColumnName("caption");

                entity.Property(e => e.MemberId).HasColumnName("member_id");

                entity.Property(e => e.OriginPostId).HasColumnName("origin_post_id");

                entity.Property(e => e.PostType).HasColumnName("post_type");

                entity.Property(e => e.RecordingId).HasColumnName("recording_id");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.UpdateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("update_time");

                entity.Property(e => e.UploadTime)
                    .HasColumnType("datetime")
                    .HasColumnName("upload_time");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Post__member_id__1332DBDC");

                entity.HasOne(d => d.OriginPost)
                    .WithMany(p => p.InverseOriginPost)
                    .HasForeignKey(d => d.OriginPostId)
                    .HasConstraintName("FK_Post_Post");

                entity.HasOne(d => d.Recording)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.RecordingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Post__recording___14270015");
            });

            modelBuilder.Entity<PostComment>(entity =>
            {
                entity.HasKey(e => e.CommentId)
                    .HasName("DF_post_comment_id");

                entity.ToTable("PostComment");

                entity.HasIndex(e => e.MemberId, "IX_PostComment_member_id");

                entity.HasIndex(e => e.ParentCommentId, "IX_PostComment_parent_comment_id");

                entity.HasIndex(e => e.PostId, "IX_PostComment_post_id")
                    .IsUnique();

                entity.Property(e => e.CommentId)
                    .HasColumnName("comment_id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Comment).HasColumnName("comment");

                entity.Property(e => e.CommentType).HasColumnName("comment_type");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("create_time");

                entity.Property(e => e.MemberId).HasColumnName("member_id");

                entity.Property(e => e.ParentCommentId).HasColumnName("parent_comment_id");

                entity.Property(e => e.PostId).HasColumnName("post_id");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.PostComments)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PostComment_Account");

                entity.HasOne(d => d.ParentComment)
                    .WithMany(p => p.InverseParentComment)
                    .HasForeignKey(d => d.ParentCommentId);

                entity.HasOne(d => d.Post)
                    .WithOne(p => p.PostComment)
                    .HasForeignKey<PostComment>(d => d.PostId);
            });

            modelBuilder.Entity<PostRating>(entity =>
            {
                entity.HasKey(e => new { e.MemberId, e.PostId });

                entity.ToTable("PostRating");

                entity.HasIndex(e => e.PostId, "IX_PostRating_post_id");

                entity.Property(e => e.MemberId).HasColumnName("member_id");

                entity.Property(e => e.PostId).HasColumnName("post_id");

                entity.Property(e => e.Score).HasColumnName("score");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.PostRatings)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PostRating_Account");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.PostRatings)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PostRating_Post");
            });

            modelBuilder.Entity<PostShare>(entity =>
            {
                entity.ToTable("PostShare");

                entity.HasIndex(e => e.MemberId, "IX_PostShare_member_id");

                entity.HasIndex(e => e.PostId, "IX_PostShare_post_id");

                entity.Property(e => e.PostShareId)
                    .HasColumnName("post_share_id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Caption)
                    .HasColumnType("text")
                    .HasColumnName("caption");

                entity.Property(e => e.MemberId).HasColumnName("member_id");

                entity.Property(e => e.PostId).HasColumnName("post_id");

                entity.Property(e => e.ShareTime)
                    .HasColumnType("datetime")
                    .HasColumnName("share_time");

                entity.Property(e => e.UpdateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("update_time");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.PostShares)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PostShare__membe__17036CC0");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.PostShares)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PostShare__post___17F790F9");
            });

            modelBuilder.Entity<PurchasedSong>(entity =>
            {
                entity.ToTable("PurchasedSong");

                entity.HasIndex(e => e.InAppTransactionId, "IX_PurchasedSong_InAppTransactionId");

                entity.HasIndex(e => e.MemberId, "IX_PurchasedSong_member_id");

                entity.HasIndex(e => e.SongId, "IX_PurchasedSong_song_id");

                entity.Property(e => e.PurchasedSongId)
                    .HasColumnName("purchased_song_id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.MemberId).HasColumnName("member_id");

                entity.Property(e => e.PurchaseDate)
                    .HasColumnType("datetime")
                    .HasColumnName("purchase_date");

                entity.Property(e => e.SongId).HasColumnName("song_id");

                entity.HasOne(d => d.InAppTransaction)
                    .WithMany(p => p.PurchasedSongs)
                    .HasForeignKey(d => d.InAppTransactionId);

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.PurchasedSongs)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Purchased__membe__1AD3FDA4");

                entity.HasOne(d => d.Song)
                    .WithMany(p => p.PurchasedSongs)
                    .HasForeignKey(d => d.SongId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Purchased__song___1BC821DD");
            });

            modelBuilder.Entity<Recording>(entity =>
            {
                entity.ToTable("Recording");

                entity.HasIndex(e => e.HostId, "IX_Recording_host_id");

                entity.HasIndex(e => e.KaraokeRoomId, "IX_Recording_karaoke_room_id");

                entity.HasIndex(e => e.OwnerId, "IX_Recording_owner_id");

                entity.HasIndex(e => e.SongId, "IX_Recording_song_id");

                entity.Property(e => e.RecordingId)
                    .HasColumnName("recording_id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.HostId).HasColumnName("host_id");

                entity.Property(e => e.KaraokeRoomId).HasColumnName("karaoke_room_id");

                entity.Property(e => e.OwnerId).HasColumnName("owner_id");

                entity.Property(e => e.RecordingName)
                    .HasMaxLength(150)
                    .HasColumnName("recording_name");

                entity.Property(e => e.RecordingType).HasColumnName("recording_type");

                entity.Property(e => e.Score).HasColumnName("score");

                entity.Property(e => e.SongId).HasColumnName("song_id");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_date");

                entity.HasOne(d => d.Host)
                    .WithMany(p => p.RecordingHosts)
                    .HasForeignKey(d => d.HostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Recording__host___1CBC4616");

                entity.HasOne(d => d.KaraokeRoom)
                    .WithMany(p => p.Recordings)
                    .HasForeignKey(d => d.KaraokeRoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Recording__karao__1DB06A4F");

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.RecordingOwners)
                    .HasForeignKey(d => d.OwnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Recording__owner__1EA48E88");

                entity.HasOne(d => d.Song)
                    .WithMany(p => p.Recordings)
                    .HasForeignKey(d => d.SongId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Recording__song___1F98B2C1");
            });

            modelBuilder.Entity<Report>(entity =>
            {
                entity.ToTable("Report");

                entity.HasIndex(e => e.PostId, "IX_Report_post_id");

                entity.HasIndex(e => e.ReportedAccountId, "IX_Report_reported_account_id");

                entity.HasIndex(e => e.ReporterId, "IX_Report_reporter_id");

                entity.HasIndex(e => e.RoomId, "IX_Report_room_id");

                entity.Property(e => e.ReportId)
                    .HasColumnName("report_id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.CommentId).HasColumnName("comment_id");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("create_time");

                entity.Property(e => e.PostId).HasColumnName("post_id");

                entity.Property(e => e.Reason)
                    .HasMaxLength(500)
                    .HasColumnName("reason");

                entity.Property(e => e.ReportCategory).HasColumnName("report_category");

                entity.Property(e => e.ReportType).HasColumnName("report_type");

                entity.Property(e => e.ReportedAccountId).HasColumnName("reported_account_id");

                entity.Property(e => e.ReporterId).HasColumnName("reporter_id");

                entity.Property(e => e.RoomId).HasColumnName("room_id");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Reports)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Report__post_id__2180FB33");

                entity.HasOne(d => d.ReportedAccount)
                    .WithMany(p => p.ReportReportedAccounts)
                    .HasForeignKey(d => d.ReportedAccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Report__reported__22751F6C");

                entity.HasOne(d => d.Reporter)
                    .WithMany(p => p.ReportReporters)
                    .HasForeignKey(d => d.ReporterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Report__reporter__236943A5");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Reports)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Report__room_id__245D67DE");
            });

            modelBuilder.Entity<Singer>(entity =>
            {
                entity.ToTable("Singer");

                entity.Property(e => e.SingerId)
                    .HasColumnName("singer_id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.SingerName)
                    .HasMaxLength(150)
                    .HasColumnName("singer_name");
            });

            modelBuilder.Entity<Song>(entity =>
            {
                entity.ToTable("Song");

                entity.HasIndex(e => e.CreatorId, "IX_Song_creator_id");

                entity.HasIndex(e => e.SongCode, "UQ__Song__43F33A39E8877F76")
                    .IsUnique()
                    .HasFilter("([song_code] IS NOT NULL)");

                entity.Property(e => e.SongId)
                    .HasColumnName("song_id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.CreatorId).HasColumnName("creator_id");

                entity.Property(e => e.Price)
                    .HasColumnType("money")
                    .HasColumnName("price");

                entity.Property(e => e.PublicDate)
                    .HasColumnType("datetime")
                    .HasColumnName("public_date");

                entity.Property(e => e.SongCode)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("song_code");

                entity.Property(e => e.SongDescription)
                    .HasMaxLength(500)
                    .HasColumnName("song_description");

                entity.Property(e => e.SongName)
                    .HasMaxLength(250)
                    .HasColumnName("song_name");

                entity.Property(e => e.SongStatus).HasColumnName("song_status");

                entity.Property(e => e.SongUrl)
                    .HasColumnType("text")
                    .HasColumnName("song_url");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_date");

                entity.HasOne(d => d.Creator)
                    .WithMany(p => p.SongsNavigation)
                    .HasForeignKey(d => d.CreatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Song__creator_id__25518C17");

                entity.HasMany(d => d.Artists)
                    .WithMany(p => p.Songs)
                    .UsingEntity<Dictionary<string, object>>(
                        "SongArtist",
                        l => l.HasOne<Artist>().WithMany().HasForeignKey("ArtistId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_SongArtist_Artist"),
                        r => r.HasOne<Song>().WithMany().HasForeignKey("SongId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_SongArtist_Song"),
                        j =>
                        {
                            j.HasKey("SongId", "ArtistId");

                            j.ToTable("SongArtist");

                            j.HasIndex(new[] { "ArtistId" }, "IX_SongArtist_artist_id");

                            j.IndexerProperty<Guid>("SongId").HasColumnName("song_id");

                            j.IndexerProperty<Guid>("ArtistId").HasColumnName("artist_id");
                        });

                entity.HasMany(d => d.Genres)
                    .WithMany(p => p.Songs)
                    .UsingEntity<Dictionary<string, object>>(
                        "SongGenre",
                        l => l.HasOne<Genre>().WithMany().HasForeignKey("GenreId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_SongGenre_Genre"),
                        r => r.HasOne<Song>().WithMany().HasForeignKey("SongId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_SongGenre_Song"),
                        j =>
                        {
                            j.HasKey("SongId", "GenreId");

                            j.ToTable("SongGenre");

                            j.HasIndex(new[] { "GenreId" }, "IX_SongGenre_genre_id");

                            j.IndexerProperty<Guid>("SongId").HasColumnName("song_id");

                            j.IndexerProperty<Guid>("GenreId").HasColumnName("genre_id");
                        });

                entity.HasMany(d => d.Singers)
                    .WithMany(p => p.Songs)
                    .UsingEntity<Dictionary<string, object>>(
                        "SongSinger",
                        l => l.HasOne<Singer>().WithMany().HasForeignKey("SingerId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_SongSinger_Singer"),
                        r => r.HasOne<Song>().WithMany().HasForeignKey("SongId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_SongSinger_Song"),
                        j =>
                        {
                            j.HasKey("SongId", "SingerId");

                            j.ToTable("SongSinger");

                            j.HasIndex(new[] { "SingerId" }, "IX_SongSinger_singer_id");

                            j.IndexerProperty<Guid>("SongId").HasColumnName("song_id");

                            j.IndexerProperty<Guid>("SingerId").HasColumnName("singer_id");
                        });
            });

            modelBuilder.Entity<SupportRequest>(entity =>
            {
                entity.HasKey(e => e.RequestId)
                    .HasName("PK__SupportR__18D3B90FC2899572");

                entity.ToTable("SupportRequest");

                entity.HasIndex(e => e.SenderId, "IX_SupportRequest_sender_id");

                entity.Property(e => e.RequestId)
                    .HasColumnName("request_id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Category).HasColumnName("category");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("create_time");

                entity.Property(e => e.Problem)
                    .HasColumnType("text")
                    .HasColumnName("problem");

                entity.Property(e => e.SenderId).HasColumnName("sender_id");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.SupportRequests)
                    .HasForeignKey(d => d.SenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SupportRe__sende__2645B050");
            });

            modelBuilder.Entity<VoiceAudio>(entity =>
            {
                entity.HasKey(e => e.VoiceId)
                    .HasName("PK__VoiceAud__128AF381A07F9D92");

                entity.ToTable("VoiceAudio");

                entity.HasIndex(e => e.MemberId, "IX_VoiceAudio_member_id");

                entity.HasIndex(e => e.RecordingId, "IX_VoiceAudio_recording_id");

                entity.Property(e => e.VoiceId)
                    .HasColumnName("voice_id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.DurationSecond).HasColumnName("duration_second");

                entity.Property(e => e.EndTime)
                    .HasColumnType("datetime")
                    .HasColumnName("end_time");

                entity.Property(e => e.MemberId).HasColumnName("member_id");

                entity.Property(e => e.Pitch).HasColumnName("pitch");

                entity.Property(e => e.RecordingId).HasColumnName("recording_id");

                entity.Property(e => e.StartTime)
                    .HasColumnType("datetime")
                    .HasColumnName("start_time");

                entity.Property(e => e.UploadTime)
                    .HasColumnType("datetime")
                    .HasColumnName("upload_time");

                entity.Property(e => e.VoiceUrl)
                    .HasColumnType("text")
                    .HasColumnName("voice_url");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.VoiceAudios)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__VoiceAudi__membe__2739D489");

                entity.HasOne(d => d.Recording)
                    .WithMany(p => p.VoiceAudios)
                    .HasForeignKey(d => d.RecordingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__VoiceAudi__recor__282DF8C2");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
