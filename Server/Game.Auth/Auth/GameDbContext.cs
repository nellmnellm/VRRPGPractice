using Auth.Entities;
using Microsoft.EntityFrameworkCore;

namespace Auth
{
    public class GameDbContext : DbContext
    {
        public GameDbContext(DbContextOptions options) : base(options)
        {

        }


        // Db 쿼리 루트
        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 어셈블리전체에서 IEntityTypeConfiguration 전부 찾아서 적용
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GameDbContext).Assembly);

        }
    }
    
}
