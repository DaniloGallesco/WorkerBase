using ClubeAss.Domain;
using ClubeAss.Domain.Interface.Repository;
using ClubeAss.Repository.Ef.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClubeAss.Repository.Ef
{
    public class CustomerRepository : ICustomerRepository, IDisposable
    {
        protected readonly BaseContext db;

        public CustomerRepository(BaseContext context)
        {
            db = context;
        }

        public async void Add(Customer customer)
        {
            await db.Customers.AddAsync(customer);
            db.SaveChanges();
        }

        public void Update(Customer customer)
        {
            db.Customers.Update(customer);
            db.SaveChanges();
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await db.Customers.ToListAsync();
        }

        public async Task<Customer> GetById(Guid id)
        {
            return await db.Customers.FindAsync(id);
        }
        public void Remove(Customer customer)
        {
            db.Customers.Remove(customer);
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
