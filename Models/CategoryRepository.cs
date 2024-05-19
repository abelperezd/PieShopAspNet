namespace PieShop.Models
{
    public class CategoryRepository: ICategoryRepository
    {
        private readonly PieShopDbContext _pieShopDbContext;

        public CategoryRepository(PieShopDbContext bethanysPieShopDbContext)
        {
            _pieShopDbContext = bethanysPieShopDbContext;
        }

        public IEnumerable<Category> AllCategories => _pieShopDbContext.Categories.OrderBy(p => p.CategoryName);
    }
}
