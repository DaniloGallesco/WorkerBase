using ClubeAss.Domain;
using ClubeAss.Repository.Ef;
using ClubeAss.Repository.Ef.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Xunit;

namespace ClubeAss.Test.UnitTest.Repository
{
    public class CustomerRepositoryTest
    {
        private CustomerRepository CustomerRepositoryMoq;

        public CustomerRepositoryTest()
        {
            var dbName = $"ApiBase_{DateTime.Now.ToFileTimeUtc()}";

            DbContextOptions<BaseContext> DbContextOptionsMoq = new DbContextOptionsBuilder<BaseContext>()
             .UseInMemoryDatabase(dbName)
             .Options;

            CustomerRepositoryMoq = new CustomerRepository(new BaseContext(DbContextOptionsMoq));
        }


        [Fact]
        public void AddSucesso()
        {

            Guid guid = Guid.NewGuid();
            var obj = new Customer() { Id = guid, Nome = "Nome_" + guid.ToString() };

            CustomerRepositoryMoq.Add(obj);

            var validar = CustomerRepositoryMoq.GetById(guid).Result;

            Assert.True(validar.Id == guid && validar.Nome == "Nome_" + guid.ToString());
        }

        [Fact]
        public void UpdateSucesso()
        {

            Guid guid = Guid.NewGuid();
            var obj = new Customer() { Id = guid, Nome = "Nome_" + guid.ToString() };

            CustomerRepositoryMoq.Add(obj);
            obj.Nome = "Nome_v2" + guid.ToString();

            CustomerRepositoryMoq.Update(obj);

            var validar = CustomerRepositoryMoq.GetById(guid).Result;

            Assert.True(validar.Id == guid && validar.Nome == "Nome_v2" + guid.ToString());
        }

        [Fact]
        public void UpdateNotExist()
        {
            Guid guid = Guid.NewGuid();
            var obj = new Customer() { Id = guid, Nome = "Nome_" + guid.ToString() };

            Assert.Throws<DbUpdateConcurrencyException>(() => CustomerRepositoryMoq.Update(obj));
        }

        [Fact]
        public void GetByIdNotExist()
        {
            var result = CustomerRepositoryMoq.GetById(Guid.NewGuid()).Result;

            Assert.Null(result);
        }

        [Fact]
        public void GetAllNotExist()
        {
            var result = CustomerRepositoryMoq.GetAll().Result;

            Assert.False(result.Any());
        }

        [Fact]
        public void GetAllExist()
        {
            Guid guid = Guid.NewGuid();
            var obj = new Customer() { Id = guid, Nome = "Nome_" + guid.ToString() };

            CustomerRepositoryMoq.Add(obj);
            var result = CustomerRepositoryMoq.GetAll().Result;

            Assert.True(result.Any() && result.Count() == 1);
        }

        [Fact]
        public void RemoveExist()
        {
            Guid guid = Guid.NewGuid();
            var obj = new Customer() { Id = guid, Nome = "Nome_" + guid.ToString() };

            CustomerRepositoryMoq.Add(obj);
            CustomerRepositoryMoq.Remove(obj);
            var result = CustomerRepositoryMoq.GetById(guid).Result;

            Assert.Null(result);
        }

        [Fact]
        public void RemoveNotExist()
        {
            Guid guid = Guid.NewGuid();
            var obj = new Customer() { Id = guid, Nome = "Nome_" + guid.ToString() };

            Assert.Throws<DbUpdateConcurrencyException>(() => CustomerRepositoryMoq.Remove(obj));
        }
    }
}
