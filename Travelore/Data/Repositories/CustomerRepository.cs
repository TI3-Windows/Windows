using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Travelore.Model;

namespace Travelore.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Customer> _customers;

        public CustomerRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
            _customers = dbContext.Customers;
        }

        public Customer GetBy(string email)
        {
            return _customers
                .Include(c => c.TravelLists)
                .ThenInclude(t => t.Tasks)
                .Include(c => c.TravelLists)
                .ThenInclude(c => c.Categories)
                .ThenInclude(c => c.Items)
                .Include(c => c.TravelLists)
                .ThenInclude(t => t.Itinerary)
                .SingleOrDefault(c => c.Email == email);
        }

        public void Add(Customer customer)
        {
            _customers.Add(customer);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
