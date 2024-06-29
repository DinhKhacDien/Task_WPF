using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace TASK1_WPF.Models
{
    public class DBContext:DbContext
    {
        public DBContext()
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=LAPTOP-045KIG4Q\\SQLEXPRESS;Initial Catalog=UserManager;Integrated Security=True;Trust Server Certificate=True");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GroupUsers>(e =>
            {
                e.ToTable("GroupUsers");
                e.HasKey(gu => gu.GroupUserID);
                e.Property(gu => gu.NgayTao).HasDefaultValueSql("getutcdate()");
            });

            modelBuilder.Entity<User>(e =>
            {
                e.ToTable("Users");
                e.HasKey(u => u.UserID);
                e.Property(u => u.NgayTao).HasDefaultValueSql("getutcdate()");
                e.HasOne(e => e.GroupUser)
                .WithMany(e => e.Users)
                .HasForeignKey(e => e.GroupUserID)
                .HasConstraintName("FK_Users_GroupUsers");
            });
        }
        public DbSet<GroupUsers> GroupUserses { get; set; }
       public DbSet<User> Users{ get; set; }
    }
}
