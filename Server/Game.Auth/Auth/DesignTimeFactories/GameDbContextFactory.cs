
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Auth.DesignTimeFactories
{
    public class GameDbContextFactory : IDesignTimeDbContextFactory<GameDbContext>
    {
        public GameDbContext CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder<GameDbContext>()
                .UseMySql(
                    DBConnectionSettings.CONNECTION,
                    DBConnectionSettings.MYSQL_SERVER_VERSION
                    )
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
                .Options;

            return new GameDbContext(options);
        }
    }
}
