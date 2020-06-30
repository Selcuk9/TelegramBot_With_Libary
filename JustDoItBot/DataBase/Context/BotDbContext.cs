using BotControllerProj.DataBase.Models;
using Microsoft.EntityFrameworkCore;

namespace BotControllerProj.DataBase.Context
{
    public class BotDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserInfo> UserInfo { get; set; }
        public DbSet<Band> Bands { get; set; }
        public DbSet<ChatState> ChatState { get; set; }
        public DbSet<Goal> Goals { get; set; }


        public BotDbContext(DbContextOptions<BotDbContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }

    }
}
