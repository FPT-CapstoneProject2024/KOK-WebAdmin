namespace WebAdmin.DTOModels.Response.Account
{
    public class AccountDTO
    {
        public Guid? AccountId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public AccountGender? Gender { get; set; }
        public AccountRole? Role { get; set; }
        public decimal? Star { get; set; }
        public bool? IsOnline { get; set; }
        public string Fullname { get; set; } = string.Empty;
        public int? Yob { get; set; }
        public string IdentityCardNumber { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime? CreatedTime { get; set; }
        public Guid? CharacterItemId { get; set; }
        public Guid? RoomItemId { get; set; }
        public AccountStatus? AccountStatus { get; set; }
    }
}
