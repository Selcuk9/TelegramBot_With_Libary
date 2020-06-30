using System;
using System.Collections.Generic;
using System.Text;

namespace BotControllerProj.DataBase.Models
{
    public class UserInfo
    {
        public int Id { get; set; }
        public long ChatId { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public string Username { get; set; }
        public string PhoneNumber { get; set; }

        public string Name { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
        public string Description { get; set; }

        public bool HasPhoto { get; set; }

        public DateTime TimeCreated { get; set; }
    }
}
