using Microsoft.EntityFrameworkCore;
using tracker_webapp.Repository;

namespace tracker_webapp.Data;

public class ItemsDBContext : DbContext
{   
    public DbSet<Category> Categories { get; set; }
    public DbSet<TrackItem> TrackItems { get; set; }
    public DbSet<UserToTrackItem> UserToTrackItems { get; set; }

    public ItemsDBContext(DbContextOptions<ItemsDBContext> options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Add any additional configuration or constraints here
    }
}
