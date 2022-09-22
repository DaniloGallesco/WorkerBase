using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClubeAss.Domain.Interface.Repository
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAll();

        Task<Customer> GetById(Guid id);

        void Add(Customer customer);

        void Update(Customer customer);

        void Remove(Customer customer);
    }
}