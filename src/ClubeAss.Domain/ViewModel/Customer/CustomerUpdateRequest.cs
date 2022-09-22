using ClubeAss.Domain.Commands;
using MediatR;

namespace ClubeAss.API.Customer.ViewModel.Customer
{
    public class CustomerUpdateRequest : IRequest<BaseResponse>
    {
        public string Nome { get; set; }
    }
}