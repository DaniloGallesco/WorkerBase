using ClubeAss.Domain.Commands;
using MediatR;
using System;

namespace ClubeAss.API.Customer.ViewModel.Customer
{
    public class CustomerDeleteRequest : IRequest<BaseResponse>
    {
        public CustomerDeleteRequest(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; private set; }
    }
}
