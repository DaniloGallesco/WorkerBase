using AutoMapper;
using ClubeAss.API.Customer.ViewModel.Customer;
using ClubeAss.Application;
using ClubeAss.Domain;
using ClubeAss.Domain.Interface.Repository;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace ClubeAss.Test.UnitTest.Application
{
    public class CustomerApplicationTest
    {
        private Mock<ICustomerRepository> CustomerRepositoryMoq;
        private Mock<IMapper> imapperMoq;
        private Mock<ILogger<CustomerApplication>> logmoq;

        public CustomerApplicationTest()
        {
            CustomerRepositoryMoq = new Mock<ICustomerRepository>();
            imapperMoq = new Mock<IMapper>();
            logmoq = new Mock<ILogger<CustomerApplication>>();
        }


        [Fact]
        public void AddSucesso()
        {
            var guid = Guid.NewGuid();

            imapperMoq.Setup(x => x.Map<CustomerAddRequest, Customer>(It.IsAny<CustomerAddRequest>()))
               .Returns(new Customer() { Nome = "Nome_" + guid.ToString(), Id = guid });

            CustomerRepositoryMoq.Setup(x => x.Add(It.IsAny<Customer>()));

            var result = new CustomerApplication(CustomerRepositoryMoq.Object, imapperMoq.Object, logmoq.Object)
                 .Add(new CustomerAddRequest() { Nome = guid.ToString() }).Result;

            Assert.True(result.StatusCode == System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public void UpdateSucesso()
        {
            var guid = Guid.NewGuid();
            imapperMoq.Setup(x => x.Map<CustomerUpdateRequest, Customer>(It.IsAny<CustomerUpdateRequest>()))
                           .Returns(new Customer() { Nome = "Nome_" + guid.ToString(), Id = guid });

            CustomerRepositoryMoq.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(new Customer() { });

            CustomerRepositoryMoq.Setup(x => x.Update(It.IsAny<Customer>()));

            var result = new CustomerApplication(CustomerRepositoryMoq.Object, imapperMoq.Object, logmoq.Object)
                 .Update(guid, new CustomerUpdateRequest() { Nome = guid.ToString() }).Result;

            Assert.True(result.StatusCode == System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public void UpdateNotExist()
        {
            var guid = Guid.NewGuid();
            imapperMoq.Setup(x => x.Map<CustomerUpdateRequest, Customer>(It.IsAny<CustomerUpdateRequest>()))
                           .Returns(new Customer() { Nome = "Nome_" + guid.ToString(), Id = guid });

            CustomerRepositoryMoq.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync((Customer)null);

            CustomerRepositoryMoq.Setup(x => x.Update(It.IsAny<Customer>()));

            var result = new CustomerApplication(CustomerRepositoryMoq.Object, imapperMoq.Object, logmoq.Object)
                 .Update(guid, new CustomerUpdateRequest() { Nome = guid.ToString() }).Result;

            Assert.True(result.StatusCode == System.Net.HttpStatusCode.BadRequest);
        }

        [Fact]
        public void GetByIdNotExist()
        {
            var guid = Guid.NewGuid();
            CustomerRepositoryMoq.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync((Customer)null);

            var result = new CustomerApplication(CustomerRepositoryMoq.Object, imapperMoq.Object, logmoq.Object).GetByid(new CustomerGetRequest(guid)).Result;

            Assert.Null(result.Content);
        }

        [Fact]
        public void GetAllNotExist()
        {
            var guid = Guid.NewGuid();
            CustomerRepositoryMoq.Setup(x => x.GetAll()).ReturnsAsync((List<Customer>)null);

            imapperMoq.Setup(x => x.Map<IEnumerable<CustomerResponse>>(It.IsAny<IEnumerable<Customer>>()))
                          .Returns((IEnumerable<CustomerResponse>)null);

            var result = new CustomerApplication(CustomerRepositoryMoq.Object, imapperMoq.Object, logmoq.Object).GetAll().Result;

            Assert.Null(result.Content);
        }

        [Fact]
        public void GetAllExist()
        {
            var guid = Guid.NewGuid();
            CustomerRepositoryMoq.Setup(x => x.GetAll()).ReturnsAsync(new List<Customer>() { new Customer() { Id = guid, Nome = "Nome_" + guid.ToString() } });

            imapperMoq.Setup(x => x.Map<IEnumerable<CustomerResponse>>(It.IsAny<IEnumerable<Customer>>()))
                          .Returns(new List<CustomerResponse>() { new CustomerResponse() { Id = guid, Nome = "Nome_" + guid.ToString() } });

            var result = new CustomerApplication(CustomerRepositoryMoq.Object, imapperMoq.Object, logmoq.Object).GetAll().Result;

            Assert.NotNull(result.Content);
        }

        [Fact]
        public void RemoveExist()
        {
            var guid = Guid.NewGuid();
            CustomerRepositoryMoq.Setup(x => x.Remove(It.IsAny<Customer>()));

            CustomerRepositoryMoq.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(new Customer() { Id = guid, Nome = "NOme" + guid.ToString() });


            var result = new CustomerApplication(CustomerRepositoryMoq.Object, imapperMoq.Object, logmoq.Object).Remove(new CustomerDeleteRequest(guid)).Result;

            Assert.True(result.StatusCode == System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public void RemoveNotExist()
        {
            var guid = Guid.NewGuid();
            CustomerRepositoryMoq.Setup(x => x.Remove(It.IsAny<Customer>()));

            CustomerRepositoryMoq.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync((Customer)null);


            var result = new CustomerApplication(CustomerRepositoryMoq.Object, imapperMoq.Object, logmoq.Object).Remove(new CustomerDeleteRequest(guid)).Result;

            Assert.True(result.StatusCode == System.Net.HttpStatusCode.BadRequest);
        }
    }
}
