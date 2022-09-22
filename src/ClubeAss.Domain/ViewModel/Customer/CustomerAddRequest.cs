using ClubeAss.Domain.Commands;
using MediatR;

namespace ClubeAss.API.Customer.ViewModel.Customer
{
    public class CustomerAddRequest : IRequest<BaseResponse>
    {
        public string Nome { get; set; }
    }
}