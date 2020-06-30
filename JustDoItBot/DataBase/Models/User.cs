using System;
using System.Collections.Generic;
using System.Text;
using BotControllerProj.Source.User;

namespace BotControllerProj.DataBase.Models
{
    public class User
    {
        public int Id { get; set; }
        public long ChatId { get; set; }
        public string Role { get; set; }
        public int? BandId { get; set; }
        public Band Band { get; set; }
        public ChatState ChatState { get; set; }
        public UserInfo Info { get; set; }
        public Goal Goal { get; set; }
    }
}
