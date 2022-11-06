using AutoMapper;
using ClubeAss.API.Customer.ViewModel.Customer;
using ClubeAss.Domain;
using ClubeAss.Domain.Commands;
using ClubeAss.Domain.Interface.Application;
using ClubeAss.Domain.Interface.Repository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClubeAss.Application
{
    public class CustomerApplication : ICustomerApplication
    {
        private readonly ICustomerRepository _clienteRepositorio;
        private readonly IMapper _mapper;
        private readonly ILogger<CustomerApplication> _logger;

        public CustomerApplication(ICustomerRepository clienteRepositorio, IMapper mapper, ILogger<CustomerApplication> logger)
        {
            _clienteRepositorio = clienteRepositorio;
            _mapper = mapper;
            _logger = logger;
        }

        public Task<BaseResponse> Add(CustomerAddRequest request)
        {

            var customer = _mapper.Map<CustomerAddRequest, Customer>(request);

            _clienteRepositorio.Add(customer);

            return Task.FromResult(new BaseResponse(System.Net.HttpStatusCode.OK));
        }

        public Task<BaseResponse> GetAll()
        {
            var customers = _clienteRepositorio.GetAll().Result;

            return Task.FromResult(new BaseResponse(System.Net.HttpStatusCode.OK, _mapper.Map<IEnumerable<CustomerResponse>>(customers)));
        }

        public Task<BaseResponse> GetByid(CustomerGetRequest request)
        {
            var customers = _clienteRepositorio.GetById(request.Id).Result;

            return Task.FromResult(new BaseResponse(System.Net.HttpStatusCode.OK, _mapper.Map<CustomerResponse>(customers)));
        }

        public Task<BaseResponse> Remove(CustomerDeleteRequest request)
        {

            var item = _clienteRepositorio.GetById(request.Id).Result;

            if (item == null)
            {
                return Task.FromResult(new BaseResponse(System.Net.HttpStatusCode.BadRequest, null, new List<string>() { "Id não encontrado" }));
            }

            _clienteRepositorio.Remove(item);

            return Task.FromResult(new BaseResponse(System.Net.HttpStatusCode.OK));
        }

        public Task<BaseResponse> Update(Guid id, CustomerUpdateRequest request)
        {

            var customer = _mapper.Map<CustomerUpdateRequest, Customer>(request);
            customer.Id = id;

            var item = _clienteRepositorio.GetById(customer.Id).Result;

            if (item == null)
            {
                return Task.FromResult(new BaseResponse(System.Net.HttpStatusCode.BadRequest, null, new List<string>() { "Id não encontrado" }));
            }

            item.Nome = customer.Nome;

            _clienteRepositorio.Update(item);


            return Task.FromResult(new BaseResponse(System.Net.HttpStatusCode.OK));

        }

    }
}