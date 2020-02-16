using Microsoft.EntityFrameworkCore;

namespace GretaFoodCore.Api.DAL
{
    public sealed class GretaFoodDbContext : DbContext
    {
        public DbSet<RestaurantEntity> Restaurants { get; set; }
        public DbSet<FoodEntity> Foods { get; set; }
        
        public GretaFoodDbContext(DbContextOptions<GretaFoodDbContext> options)
            : base(options)
        {
            this.Database.EnsureCreated();
        }
    }
}