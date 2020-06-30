using BotControllerProj.DataBase.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BotControllerProj.DataBase.Context
{
    class SampleContextFactory : IDesignTimeDbContextFactory<BotDbContext>
    {
        public BotDbContext CreateDbContext(string[] args)
        {
            DbContextOptions<BotDbContext> options = HelperDataBase.DB_OPTIONS;
            return new BotDbContext(options);
        }
    }
}
