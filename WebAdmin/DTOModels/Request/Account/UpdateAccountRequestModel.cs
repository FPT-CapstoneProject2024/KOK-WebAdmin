namespace WebAdmin.DTOModels.Request.Account
{
    public class UpdateAccountRequestModel
    {
        public AccountGender? Gender { get; set; }
        //public AccountRole? Role { get; set; }
        public string Fullname { get; set; } = null!;
        public int? Yob { get; set; }
        public string? IdentityCardNumber { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
    }
}
