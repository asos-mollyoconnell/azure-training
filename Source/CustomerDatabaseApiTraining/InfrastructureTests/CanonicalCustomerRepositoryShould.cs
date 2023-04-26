using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.CanonicalCustomer.Contracts;
using AutoMapper;
using CanonicalCustomerData;
using CanonicalCustomerData.Models;
using Domain.Models;
using Infrastructure.CanonicalCustomer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace InfrastructureTests
{
    public class CanonicalCustomerRepositoryShould : IDisposable
    {
        private readonly CanonicalCustomerDbContext _stubContext;
        private readonly IMapper _stubMapper;
        private readonly Mock<ILogger<CanonicalCustomerRepository>> _stubLogger;

        private ICanonicalCustomerRepository _sut;

        public CanonicalCustomerRepositoryShould()
        {
            var config =
                new MapperConfiguration(m => m.CreateMap<CanonicalCustomerModel, CanonicalCustomer>().ReverseMap());
            _stubMapper = new Mapper(config);
            _stubContext = CreateContext();
            _stubLogger = new Mock<ILogger<CanonicalCustomerRepository>>();
        }

        [Fact]
        public void ReturnCanonicalCustomer_WhenGetCustomerById_GivenValidId()
        {
            // arrange 
            int id = 1;

            var customers = new List<CanonicalCustomer>()
            {
                new CanonicalCustomer() { Id = 1, FullName = "Molly" },
                new CanonicalCustomer() { Id = 2, FullName = "Miranda" }
            };

            var _sut = CreateRepository(customers);

            // act 
            var actualResult = _sut.GetCustomerById(id);

            // assert
            Assert.Equal("Molly", actualResult.Result.FullName);
            Assert.IsType<Task<CanonicalCustomerModel>>(actualResult);
        }

        [Fact]
        public void AddNewCanonicalCustomerWithNextId_WhenInsertCustomer_GivenValidId()
        {
            // arrange 
            var newCustomer = new CanonicalCustomerModel() {FullName = "Jesse"};

            var customers = new List<CanonicalCustomer>()
            {
                new CanonicalCustomer() { Id = 1, FullName = "Molly" },
                new CanonicalCustomer() { Id = 2, FullName = "Miranda" }
            };

            var _sut = CreateRepository(customers);

            // act 
            var actualResult = _sut.InsertCustomer(newCustomer);

            // assert
            Assert.Equal("Jesse", actualResult.Result.FullName);
            Assert.IsType<Task<CanonicalCustomerModel>>(actualResult);
            Assert.Equal(3, actualResult.Result.Id);
        }

        [Fact]
        public void UpdateCanonicalCustomer_WhenUpdateCustomer_GivenValidId()
        {
            // arrange 
            var customerToUpdate = new CanonicalCustomerModel() { Id = 3, FullName = "Jesse Eisenpurrg" };

            var customers = new List<CanonicalCustomer>()
            {
                new CanonicalCustomer() { Id = 1, FullName = "Molly" },
                new CanonicalCustomer() { Id = 2, FullName = "Miranda Clawsgrove" },
                new CanonicalCustomer() {Id = 3, FullName = "Jesse"}
            };

            var _sut = CreateRepository(customers);

            //var requestCustomer = _stubMapper.Map<CanonicalCustomerModel>(customerToUpdate);

            // act
            _sut.UpdateCustomerAsync(customerToUpdate);

            // assert
            var getByIdResult = _sut.GetCustomerById(3);
            Assert.Equal("Jesse Eisenpurrg", getByIdResult.Result.FullName);
        }

        [Fact]
        public void ThrowInvalidOperationException_WhenUpdatedCustomerAsync_GivenCustomerAlreadyExists()
        {
            //arrange
            var customerToUpdate = new CanonicalCustomerModel() { Id = 4, FullName = "Jesse Eisenpurrg" };

            var customers = new List<CanonicalCustomer>()
            {
                new CanonicalCustomer() { Id = 1, FullName = "Molly" },
                new CanonicalCustomer() { Id = 2, FullName = "Miranda Clawsgrove" },
                new CanonicalCustomer() {Id = 3, FullName = "Jesse"}
            };
            var _sut = CreateRepository(customers);
            //act
            Task task() => _sut.UpdateCustomerAsync(customerToUpdate);

            //assert
            Assert.ThrowsAsync<InvalidOperationException>(task);

        }


        public void Dispose() => Teardown();


        private CanonicalCustomerDbContext CreateContext()
        {
            var dbOptions = new DbContextOptionsBuilder<CanonicalCustomerDbContext>().UseInMemoryDatabase(new Guid().ToString()).Options;

            return new CanonicalCustomerDbContext(dbOptions);
        }

        private CanonicalCustomerRepository CreateRepository(IEnumerable<CanonicalCustomer> customers)
        {
            _stubContext.AddRange(customers);
            _stubContext.SaveChanges();
            return new CanonicalCustomerRepository(_stubContext, _stubMapper, _stubLogger.Object);
        }
        
        private void Teardown()
        {
            _stubContext.Database.EnsureDeleted();
        }
    }
}
