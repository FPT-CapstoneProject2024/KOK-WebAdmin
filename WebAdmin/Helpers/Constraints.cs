namespace WebAdmin.Helpers
{
    public class Constraints
    {
        #region Generic
        public const string NOT_FOUND = "Thông tin không tồn tại!";
        public const string LOAD_FAILED = "Tải thông tin thất bại!";
        public const string CREATE_SUCCESS = "Tạo thông tin thành công!";
        public const string CREATE_FAILED = "Tạo thông tin thất bại!";
        public const string DELETE_SUCCESS = "Xóa thông tin thành công!";
        public const string DELETE_FAILED = "Xóa thông tin thất bại!";
        public const string UPDATE_SUCCESS = "Cập nhật thông tin thành công!";
        public const string UPDATE_FAILED = "Cập nhật thông tin thất bại!";
        public const string INFORMATION = "Thông tin: ";
        public const string INFORMATION_EXISTED = "Thông tin đã tồn tại!";
        public const string LOAD_SUCCESS = "Tải thông tin thành công!";
        #endregion

        #region Account
        public const string INFORMATION_ACCOUNT_EXISTED = "Thông tin tài khoản đã tồn tại!";
        public const string INFORMATION_ACCOUNT_NOT_EXISTED = "Thông tin tài khoản không tồn tại!";

        #endregion
        #region Package
        public const string INFORMATION_PACKAGE_EXISTED = "Thông tin gói đã tồn tại!";
        public const string INFORMATION_PACKAGE_NOT_EXISTED = "Thông tin gói không tồn tại!";

        #endregion

        #region Login
        public const string EMAIL_PASSWORD_INVALID = "email hoặc mật khẩu không đúng!";
        #endregion

        #region Page
        public const int DefaultPaging = 50;
        public const int LimitPaging = 500;
        public const int DefaultPage = 1;

        #endregion
        #region Authenticate
        public const string INVALID_VERIFICATION_CODE = "mã xác thực không hợp lệ!";
        #endregion

        #region Validate data
        public const string EMPTY_INPUT_INFORMATION = "Vui lòng nhập thông tin!";
        public const string STAR_INVALID = "Vui lòng nhập số tiền hợp lệ!";
        public const string INFORMATION_INVALID = "Vui lòng nhập thông tin hợp lệ!";
        public const string VALIDATE_NEGATIVE = @"^[0-9]+$";
        public const string VALIDATE_AMOUNT = @"^\d+(\.\d+)?$";
        public const string VALIDATE_RANGE_0_TO_100 = "Vui lòng nhập điểm từ 0 đến 100";
        public const string VALIDATE_ENDDATE = "Vui lòng nhập thời gian kết thúc lớn hơn thời gian bắt đầu!";
        #endregion

        #region Authorize
        public const string ADMIN_STAFF_ROLE = "RequiredAdminOrStaff";
        public const string STAFF_MEMBER_ROLE = "RequireStaffOrMember";
        public const string STAFF_ROLE = "STAFF";
        public const string MEMBER_ROLE = "MEMBER";
        public const string ADMIN_ROLE = "ADMIN";
        #endregion

        #region Dashboard
        public const string ONE_TO_TWELVE = "Vui lòng nhập từ 1 đến 12";
        public const string VALIDATE_ONE_TO_TWELVE = @"^(1[0-2]?|[1-9])$";
        public const string START_MONTH_END_MONTH = "Tháng kết thúc phải lớn hơn hoặc bằng tháng bắt đầu";
        public const string OVER_100_YEARS = "Quá 100 năm";
        public const string START_DATE_END_DATE = "Ngày bắt đầu phải lớn hơn hoặc bằng ngày kết thúc";
        public const string START_YEAR_END_YEAR = "Năm bắt đầu phải lớn hơn hoặc bằng năm kết thúc";
        #endregion

        #region name key of cache
        public const string ACCOUNTS = "ACCOUNTS";
        public const string ONLINE_ACCOUNTS = "ONLINE_ACCOUNTS";
        public const string ACCOUNT = "account, id: ";
        public const string SONGS = "SONGS";
        public const string SONG = "song, id: ";
        public const string ITEMS = "ITEMS";
        public const string ITEM = "item, id: ";
        #endregion

        #region Purchase Song 
        public const string INSUFFICIENT_FUNDS = "Tài khoản không đủ để thực hiện giao dịch";
        public const string PURCHASE_SONG_FAILED = "Mua bài hát thất bại, Vui lòng thử lại!";
        public const string PURCHASE_SONG_SUCCESS = "Mua bài hát thành công!";
        #endregion

        #region Purchase Item
        public const string PURCHASE_ITEM_FAILED = "Mua vật phẩm thất bại, Vui lòng thử lại!";
        public const string PURCHASE_ITEM_SUCCESS = "Mua vật phẩm thành công!";
        #endregion
    }
}
