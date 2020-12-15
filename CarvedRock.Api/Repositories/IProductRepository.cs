using CarvedRock.Api.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarvedRock.Api.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAll();
        Task<Product> GetOne(int id);
    }
}