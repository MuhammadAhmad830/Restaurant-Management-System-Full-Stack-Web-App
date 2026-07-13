using Microsoft.EntityFrameworkCore;
using ResturantManagmentSystemApp.Model;     // ← Ye line change kar di

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // Sab tables yahan add kar rahe hain
    public DbSet<Category> Categories { get; set; }
    public DbSet<MenuItem> MenuItems { get; set; }
    public DbSet<Table> Tables { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<InventoryItem> InventoryItems { get; set; }

    public DbSet<User> Users { get; set; }

    // Ye function database mein pehla data (Admin) dalne ke liye hai
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Muhammad Ahmad ka Admin account create ho raha hai
        modelBuilder.Entity<User>().HasData(new User
        {
            Id = 1,
            Username = "muhammadahmad",
            Password = "123",
            FullName = "Muhammad Ahmad",
            Role = "Admin"
        });
    }
}