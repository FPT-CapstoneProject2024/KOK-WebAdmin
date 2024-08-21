using System.ComponentModel.DataAnnotations;

namespace WebAdmin.DTOModels.Filter
{
    public class AccountFilter
    {
        public string? UserName { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public string? Gender { get; set; }
        public string? Role { get; set; }
        public decimal? UpBalance { get; set; }
        public bool? IsOnline { get; set; }
        public string? Fullname { get; set; }
        public int? Yob { get; set; }
        public string? IdentityCardNumber { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? CreatedTime { get; set; }
        public string? CharaterItemCode { get; set; }
        public string? RoomItemCode { get; set; }

        public string? AccountStatus { get; set; }
        public string? Description { get; set; }
    }
}
