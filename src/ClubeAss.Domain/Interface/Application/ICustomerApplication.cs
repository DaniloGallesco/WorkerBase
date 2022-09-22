using ClubeAss.API.Customer.ViewModel.Customer;
using ClubeAss.Domain.Commands;
using System;
using System.Threading.Tasks;

namespace ClubeAss.Domain.Interface.Application
{
    public interface ICustomerApplication
    {
        Task<BaseResponse> GetAll();

        Task<BaseResponse> GetByid(CustomerGetRequest id);

        Task<BaseResponse> Add(CustomerAddRequest customer);

        Task<BaseResponse> Update(Guid id, CustomerUpdateRequest customer);

        Task<BaseResponse> Remove(CustomerDeleteRequest id);
    }
}
