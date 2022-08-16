using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.DataBaseContexts
{
    public class GLSTablesDataBaseContext : DbContext
    {
        public GLSTablesDataBaseContext(DbContextOptions<GLSTablesDataBaseContext> options) : base(options)
        {

        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserLoginLog> UserLoginLogs { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
            .HasMany<Role>(s => s.Roles)
            .WithMany(c => c.Users);
            //.Map(cs =>
            //{
            //    cs.MapLeftKey("UserRefId");
            //    cs.MapRightKey("RoleRefId");
            //    cs.ToTable("UserRole");
            //});
            //modelBuilder.Entity<Role>().ToTable("Role");
            //modelBuilder.Entity<User>().ToTable("User");

        }
    }
}