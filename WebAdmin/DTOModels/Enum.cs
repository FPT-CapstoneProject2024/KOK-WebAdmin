
namespace WebAdmin.DTOModels
{
    #region filter order
    public enum Month
    {
        January = 1,
        February,
        March,
        April,
        May,
        June,
        July,
        August,
        September,
        October,
        November,
        December,
    }
    public enum SortOrder
    {
        Descending,
        Ascending,

    }

    public enum PostOrderFilter
    {
        PostId,
        Caption,
        UploadTime,
        UpdateTime,
        MemberId,
        RecordingId,
    }


    public enum ReportOrderFilter
    {
        ReportId, ReporterId, ReportedAccountId, ReportCategory, Status, Reason, CreateTime, ReportType, CommentId, PostId, RoomId
    }

    public enum PostRateOrderFilter
    {
        RateId,
        MemberId,
        PostId,
        VoteType,
        Category,
        Comment,
    }

    public enum PostShareOrderFilter
    {
        PostShareId,
        Caption,
        ShareTime,
        UpdateTime,
        MemberId,
        PostId,
    }

    //public enum ReportOrderFilter
    //{
    //    ReportId,
    //    ReporterId,
    //    ReportedAccountId,
    //    ReportCategory,
    //    Status,
    //    Reason,
    //    CreateTime,
    //    ReportType,
    //    CommentId,
    //    PostId,
    //    RoomId,
    //}



    public enum AccountOrderFilter
    {
        AccountId,
        UserName,
        Password,
        Email,
        Gender,
        AccountName,
        Role,
        Star,
        IsOnline,
        Description,
        CharacterId,
        Fullname,
        Yob,
        IdentityCardNumber,
        PhoneNumber,
        CreatedTime,
        AccountStatus,
        UpBalance,
    }
    public enum SongOrderFilter
    {
        SongId,
        SongName,
        SongDescription,
        SongUrl,
        SongStatus,
        CreatedDate,
        UpdatedDate,
        SongCode,
        PublicDate,
        CreatorId,
        Price,
    }

    public enum FriendOrderFilter
    {
        SenderId,
        ReceiverId,
        Status,
    }

    public enum ItemOrderFilter
    {
        ItemId,
        ItemCode,
        ItemName,
        ItemDescription,
        ItemType,
        ItemStatus,
        CanExpire,
        CanStack,
        CreatedDate,
        CreatorId,
        ItemBuyPrice,
        ItemSetPrice,
        PrefabCode,


    }

    public enum RecordingOrderFilter
    {
        RecordingId,
        RecordingName,
        RecordingType,
        CreatedDate,
        UpdatedDate,
        Volume,
        Score,
        PurchasedSongId,
        HostId,
        OwnerId,
        KaraokeRoomId,

    }

    public enum RatingOrderFilter
    {
        MemberId,
        PostId,
        Score,
    }

    public enum AccountInventoryItemOrderFilter
    {
        AccountInventoryItemId,
        ItemStatus,
        ActivateDate,
        ExpirationDate,
        Quantity,
        ItemId,
        MemberId
    }

    public enum InAppTransactionOrderFilter
    {

        InAppTransactionId,
        UpAmountBefore,
        UpTotalAmount,
        Status,
        CreatedDate,
        TransactionType,
        ItemId,
        SongId,
        MonetaryTransactionId,
        ItemQuantity,
        ItemPrice,
    }
    public enum FavouriteSongOrderFilter
    {
        MemberId,
        SongId
    }

    public enum PackageOrderFilter
    {
        PackageId,
        PackageName,
        Description,
        MoneyAmount,
        StarNumber,
        Status,
        CreatedDate,
        CreatorId,
    }

    public enum MonetaryTransactionOrderFilter
    {
        MonetaryTransactionId,
        PaymentType,
        PaymentCode,
        MoneyAmount,
        Currency,
        Status,
        CreatedDate,
        PackageId,
        MemberId,
    }

    public enum KaraokeRoomOrderFilter
    {
        RoomId,
        RoomLog,
        CreateTime,
        CreatorId,
    }

    public enum PurchasedSongOrderFilter
    {
        PurchasedSongId,
        PurchaseDate,
        MemberId,
        SongId,
    }

    public enum SupportRequestOrderFilter
    {
        TicketId,
        Problem,
        CreateTime,
        Category,
        Status,
        SenderId,
    }

    public enum ConversationOrderFilter
    {
        ConversationId,
        MemberId1,
        MemberId2,
        ConversationType,
        SupportRequestId,
    }

    public enum LoginActivityOrderFilter
    {
        LoginId,
        LoginTime,
        LoginDevice,
        MemberId,
    }

    public enum MessageOrderFilter
    {
        MessageId,
        Content,
        TimeStamp,
        SenderId,
        ConversationId,
    }

    public enum SingerOrderFilter
    {
        SingerId,
        SingerName
    }

    public enum ArtistOrderFilter
    {
        ArtistId,
        ArtistName
    }

    public enum GenreOrderFilter
    {
        GenreId,
        GenreName
    }

    public enum PostCommentFilter
    {
        CommentId,
        Comment,
        CommentType,
        Status,
        ParentCommentId,
        MemberId,
        PostId,
        UploadTime
    }
    #endregion

    #region Account
    public enum AccountRole
    {
        ADMIN = 1,
        STAFF = 2,
        MEMBER = 3,
    }
    public static class AccountRoles
    {
        public static List<string> List = new()
        {
            "Quản Trị Viên",
            "Nhân Viên",
            "Người Dùng",
        };
    }
    public enum AccountGender
    {
        MALE = 1,
        FEMALE = 2,
        OTHERS = 3,
    }

    public static class AccountGenres
    {
        public static List<string> List = new()
        {
            "Nam",
            "Nữ",
            "Khác",
        };
    }

    public enum AccountStatus
    {
        NOT_VERIFY = 0,
        ACTIVE = 1,
        INACTIVE = 2,
    }

    public static class AccountStatuses
    {
        public static List<string> List = new()
        {
            "Chưa Xác Thực",
            "Hoạt Động",
            "Không Hoạt Động"
        };
    }
    #endregion

    #region Song
    public enum SongStatus
    {
        DISABLE = 0,
        ENABLE = 1,
    }

    public class SongStatuses
    {
        public List<string> List = new()
        {
            "Vô Hiệu Hóa",
            "Kích Hoạt"
        };
    }

    public enum SongType
    {
        INTERNAL = 1,
        EXTERNAL = 2,
    }

    public class SongTypes
    {
        public List<string> List = new()
        {
            "Nguồn Ngoài",
            "Nội Bộ"
        };
    }

    public enum SongCategory
    {
        VPOP = 0,
        POP = 1,
        KPOP = 2,
        ROCK = 3,
    }

    #endregion

    #region Friend
    public enum FriendStatus
    {
        OFFLINE = 0,
        ONLINE = 1,
    }
    #endregion
    #region Item
    public enum ItemStatus
    {
        DISABLE = 0,
        ENABLE = 1,
        PENDING = 2,
    }

    public class ItemStatuses
    {
        public List<string> List = new()
        {
            "Vô Hiệu Hóa",
            "Kích Hoạt",
            "Chờ Duyệt"
        };
    }

    public enum ItemType
    {
        CHARACTER,
        ROOM,
        DEFAULT,
    }

    public class ItemTypes
    {
        public List<string> List = new()
        {
            "Nhân Vật",
            "Phòng",
            "Mặc Định"
        };
    }

    #endregion

    #region InAppTransaction
    public enum InAppTransactionType
    {
        BUY_ITEM = 1,
        BUY_SONG = 2,
        RECHARGE_UP_BALANCE = 3,
    }

    public class InAppTransactionTypes
    {
        public List<string> List = new()
        {
            "Mua Vật Phẩm",
            "Mua Bài Hát",
            "Nạp Vào Tài Khoản",
        };
    }

    public enum InAppTransactionStatus
    {
        PENDING,
        COMPLETE,
        CANCELED,
    }

    public class InAppTransactionStatuses
    {
        public List<string> List = new()
        {
            "Chờ Xử Lý",
            "Hoàn Thành",
            "Hủy Bỏ",
        };
    }
    #endregion

    #region Recording

    public enum RecordingType
    {
        SINGLE,
        MULTIPLE
    }
    #endregion

    #region Package

    public enum PackageStatus
    {
        INACTIVE = 0,
        ACTIVE = 1,

    }
    public static class PackageStatuses
    {
        public static List<string> list = new()
        {
            "Không Hoạt Động",
            "Hoạt Động"
        };
    }
    #endregion

    #region Payment


    public enum PaymentType
    {
        MOMO = 1,
        PAYOS = 2,
    }

    public enum PaymentStatus
    {
        PENDING,
        COMPLETE,
        CANCELLED,
    }
    #endregion

    #region SupportRequest
    public enum SupportRequestCategory
    {
        TECHNICAL,
        PROBLEM,

    }

    public enum SupportRequestStatus
    {
        CANCELED,
        PROCESSING,
        PROCESSED,

    }

    #endregion

    #region Conversation
    public enum ConversationType
    {
        DEFAULT,
        SUPPORT,
    }
    #endregion
    #region Post

    public enum PostStatus
    {
        ACTIVE,
        DEACTIVE,
    }
    public enum PostType
    {
        POST,
        SHARE,
    }

    public enum PostCommentType
    {
        PARENT,
        CHILD,
    }

    public enum PostCommentStatus
    {
        DEACTIVE,
        ACTIVE,
    }
    #endregion

    #region Report
    public enum ReportStatus
    {
        PROCCESSING,
        COMPLETE,
        CANCELED,
    }

    public static class ReportStatuses
    {
        public static List<string> List = new()
        {
            "Đang Xử Lý",
            "Hoàn Thành",
            "Hủy"
        };
    }
    public enum ReportType
    {
        POST,
        COMMENT,
        ROOM
    }

    public static class ReportTypes
    {
        public static List<string> List = new()
        {
            "Bài Viết",
            "Bình Luận",
            "Phòng"
        };
    }
    public static class ReportCategory
    {
        public static List<string> List = new()
        {
        "Nội dung này là spam",
        "Nội dung liên quan đến tình dục",
        "Bắt nạt hoặc quấy rối",
        "Kêu gọi bạo lực, thù ghét",
        "Tự tử hoặc tự gây thương tích",
        "Bán hoặc quảng cáo mặt hàng bị cấm",
        "Lừa đảo hoặc mạo danh",
        "Thông tin sai sự thật",
        "Vi phạm quyền sở hữu trí tuệ",
        };
    }

    public enum ReportCatagory
    {
        SEXUAL_CONTENT,
        BULLYING_OR_HARASSMENT,
        INCITEMENT_TO_VIOLENCE_OR_HATE,
        SUICIDE_OR_SELF_HARM,
        PROHIBITED_GOODS_SALES_OR_PROMOTION,
        FRAUD_OR_IMPERSONATION, SPAM,
        MISINFORMATION,
        INTELLECTUAL_PROPERTY_VIOLATION

    }

    #endregion
}
