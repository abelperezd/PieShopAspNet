using Microsoft.EntityFrameworkCore;

namespace PieShop.Models
{
    public class PieShopDbContext: DbContext
    {
        public PieShopDbContext(DbContextOptions<PieShopDbContext> options) : base(options)
        {

        }

        //Classes that we want to be added to the DB ()
        public DbSet<Category> Categories { get; set; }
        public DbSet<Pie> Pies { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}
