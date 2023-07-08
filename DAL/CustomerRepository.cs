using MNC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MNC.DAL
{
    public class CustomerRepository
    {
        private readonly MNCDBContext _dbContext;
        public CustomerRepository(MNCDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Customer> GetAllCustomers()
        {
            return _dbContext.Customers.ToList();
        }

        public Customer GetCustomerById(int id)
        {
            return _dbContext.Customers.Find(id);
        }

        public void AddCustomer(Customer Customer)
        {
            _dbContext.Customers.Add(Customer);
            _dbContext.SaveChanges();
        }

        public void UpdateCustomer(Customer Customer)
        {
            _dbContext.Entry(Customer).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void DeleteCustomer(int id)
        {
            var Customer = _dbContext.Customers.Find(id);
            if (Customer != null)
            {
                _dbContext.Customers.Remove(Customer);
                _dbContext.SaveChanges();
            }
        }
    }
}