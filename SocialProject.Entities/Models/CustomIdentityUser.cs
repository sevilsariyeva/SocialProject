using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialProject.Entities.Concrete
{
    public class CustomIdentityUser : IdentityUser
    {
        public string? ImageUrl { get; set; }
        public bool IsFriend { get; set; }
        public bool IsOnline { get; set; }
        public bool HasRequestPending { get; set; }
        public DateTime DisconnectTime { get; set; } = DateTime.Now;
        public string ConnectTime { get; set; } = "";

        public virtual ICollection<Friend>? Friends { get; set; }
        public virtual ICollection<FriendRequest>? FriendRequests { get; set; }
        public virtual ICollection<Chat>? Chats { get; set; }
        public CustomIdentityUser()
        {
            Friends = new List<Friend>();
            FriendRequests = new List<FriendRequest>();
            Chats = new List<Chat>();
        }
    }
}
