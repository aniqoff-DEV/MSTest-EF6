using Bogus;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using UnitTestingDemo;
using UnitTestingDemo.Models;

namespace EmployeeRepositoryTest
{
    [TestClass]
    public class EmployeeRepositoryTest
    {
        List<Employee> stub;
        IEmployeeRepository repository;

        [TestInitialize]
        public void Init()
        {
            stub = GenerateData(10);
            var data = stub.AsQueryable();

            var mockDbContext = new Mock<DataContext>();

            // настройка наборы базы данных employee
            var mockSet = new Mock<DbSet<Employee>>();

            mockSet.As<IQueryable<Employee>>().Setup(n => n.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Employee>>().Setup(n => n.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Employee>>().Setup(n => n.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Employee>>().Setup(n => n.GetEnumerator()).Returns(() => data.GetEnumerator());
            mockDbContext.Setup(d => d.Employees).Returns(mockSet.Object);

            mockDbContext.Setup(d => d.SaveChanges()).Returns(1);


            repository = new EmployeeRepository(mockDbContext.Object);

        }

        [TestMethod]
        [Priority(1)]
        public async Task Add_New_EmployeeAsync()
        {
            var employee = GenerateData(1);
            var response = await repository.AddEmployee(employee.FirstOrDefault());

            response.Should().BeTrue();
        }

        [TestMethod]
        [Priority(2)]
        public async Task GetAllEmployee_Test()
        {
            var response = await repository.GetAllEmployee();

            response.Count.Should().Be(10);
        }

        [TestMethod]
        [Priority(3)]
        public async Task AddOrUpdateEmployee_Add_New_Test()
        {
            var employee = GenerateData(1).FirstOrDefault();
            var response = await repository.AddOrUpdateEmployee(employee);

            response.Should().BeTrue();


        }

        [TestMethod]
        [Priority(4)]
        public async Task AddOrUpdateEmployee_Update_Test()
        {
            var employee = stub.FirstOrDefault();
            var response = await repository.AddOrUpdateEmployee(employee);

            response.Should().BeTrue();


        }

        [TestMethod]
        [Priority(5)]
        public async Task RemoveEmployee_Test()
        {
            var employee = stub.FirstOrDefault();
            var response = await repository.RemoveEmployee(employee);

            response.Should().BeTrue();


        }

        private List<Employee> GenerateData(int countFakes)
        {
            var faker = new Faker<Employee>()
                .RuleFor(c => c.FirstName, f => f.Person.FirstName)
                .RuleFor(c => c.LastName, f => f.Person.LastName)
                .RuleFor(c => c.Desination, f => f.Commerce.Department())
                .RuleFor(c => c.DateOfJoining, f => f.Date.Past())
                .RuleFor(c => c.Status, f => "ACTIVE")
                .RuleFor(c => c.EmployeeId, Guid.NewGuid);

            return faker.Generate(countFakes);
        }
    }
}