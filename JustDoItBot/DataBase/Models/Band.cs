using System;
using System.Collections.Generic;
using System.Text;

namespace BotControllerProj.DataBase.Models
{
    public class Band
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public virtual List<User> Users { get; set; }
        public DateTime TimeCreated;
    }
}
