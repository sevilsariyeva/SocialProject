﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialProject.Entities
{
    public class Chat
    {
        public int Id { get; set; }
        public string? ReceiverId { get; set; }
        public virtual CustomIdentityUser? Receiver { get; set; }
        public string? SenderId { get; set; }
        public virtual List<Message>? Messages { get; set; }
        public Chat()
        {
            Messages = new List<Message>();
        }
    }
}
