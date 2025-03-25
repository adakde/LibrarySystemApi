using LibaryApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibaryApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Entities.Book> Books { get; set; }
        public DbSet<Entities.BookStatusHistory> BookStatusHistories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>()
                .Property(b => b.Title)
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<Book>()
                .Property(b => b.Author)
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<Book>()
                .Property(b => b.Description)
                .HasMaxLength(250);
            modelBuilder.Entity<Book>()
                .Property(b => b.CreatedAt)
                .HasDefaultValueSql("getdate()");
            modelBuilder.Entity<Book>()
                .Property(b => b.Status)
                .HasConversion<string>()
                .HasMaxLength(20);
        }
    }
}
