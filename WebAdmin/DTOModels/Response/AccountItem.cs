using System;
using System.Collections.Generic;

namespace WebAdmin.DTOModels.Response
{
    public partial class AccountItem
    {
        public AccountItem()
        {
            AccountCharacterItems = new HashSet<Account>();
            AccountRoomItems = new HashSet<Account>();
        }

        public Guid AccountItemId { get; set; }
        public int ItemStatus { get; set; }
        public DateTime ActivateDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int Quantity { get; set; }
        public Guid ItemId { get; set; }
        public Guid MemberId { get; set; }
        public int ObtainMethod { get; set; }

        public  ICollection<Account> AccountCharacterItems { get; set; }
        public  ICollection<Account> AccountRoomItems { get; set; }
    }
}
