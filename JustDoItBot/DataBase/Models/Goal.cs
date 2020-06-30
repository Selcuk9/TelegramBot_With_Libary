using System;
using System.Collections.Generic;
using System.Text;

namespace BotControllerProj.DataBase.Models
{
    public class Goal
    { 
        public int Id { get; set; }
        public long ChatId { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public string GoalDescription { get; set; }
        public string GoalStatus { get; set; }
    }
}
