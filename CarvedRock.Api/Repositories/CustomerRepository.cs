using System.Collections.Generic;
using System.Threading.Tasks;
using CarvedRock.Api.Data;
using CarvedRock.Api.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarvedRock.Api.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CarvedRockDbContext _dbContext;

        public CustomerRepository(CarvedRockDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await _dbContext.Customers.ToListAsync();
        }

        public async Task<Customer> GetOne(int id)
        {
            return await _dbContext.Customers.SingleOrDefaultAsync(p => p.Id == id);
        }
    }
}
