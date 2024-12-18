using Microsoft.EntityFrameworkCore;

namespace MenuApp.Model
{
    public class MenuDbContext : DbContext
    {
        public MenuDbContext(DbContextOptions<MenuDbContext> options)
            : base(options)
        {
        }

        public DbSet<Admin> Admin { get; set; } // DbSet for Admin model
        public DbSet<Category> Categories { get; set; } // DbSet for Category model
        public DbSet<MenuItem> MenuItems { get; set; } // DbSet for MenuItem model

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the foreign key relationship between MenuItem and Category
            modelBuilder.Entity<MenuItem>()
                .HasOne(mi => mi.Category) // Each MenuItem is linked to a Category
                .WithMany() // A Category can be linked to many MenuItems
                .HasForeignKey(mi => mi.CategoryId) // Foreign key property in MenuItem
                .IsRequired(); // Ensure this relationship is required
        }
    }
}
