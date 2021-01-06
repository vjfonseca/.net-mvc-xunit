using System.Linq;

namespace AppSportsStore.Models
{
    public interface IStoreRepo
    {
        IQueryable<Product> Products { get; }
    }
}