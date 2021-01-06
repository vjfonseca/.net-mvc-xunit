using System.Linq;

namespace AppSportsStore.Models
{
    public class EFStoreRepo : IStoreRepo
    {
        private StoreDbContext context;
        public EFStoreRepo(StoreDbContext _context)
        {
            context = _context;
        }

        public IQueryable<Product> Products => context.Products;
    }
}