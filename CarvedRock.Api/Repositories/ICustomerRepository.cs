using CarvedRock.Api.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarvedRock.Api.Repositories
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAll();
        Task<Customer> GetOne(int id);
    }
}