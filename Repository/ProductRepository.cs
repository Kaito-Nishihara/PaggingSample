using PaggingSample.Entities;
using PaggingSample.Models;

namespace PaggingSample.Repository
{
    public interface IProductRepository : IRepositoryBase<Product>
    {

    }

    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }
    }
}
