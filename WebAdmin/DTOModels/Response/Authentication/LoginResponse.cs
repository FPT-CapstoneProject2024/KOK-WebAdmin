//using WebAdmin.DTOModels.Response.Account;

namespace WebAdmin.DTOModels.Response.Authentication
{
    public class LoginResponse
    {
        public string Message { get; set; } = string.Empty;
        public Account? Value { get; set; } = null;
        public bool? Result { get; set; } = false;
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}
