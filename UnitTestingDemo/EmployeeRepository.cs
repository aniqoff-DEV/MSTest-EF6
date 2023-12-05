using UnitTestingDemo.Models;

namespace UnitTestingDemo
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DataContext _dbContext;

        public EmployeeRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddEmployee(Employee record)
        {
            _dbContext.Employees.Add(record);

            return _dbContext.SaveChanges() > 0;
        }

        public async Task<bool> AddOrUpdateEmployee(Employee record)
        {
            var response = from row in _dbContext.Employees
                           where row.EmployeeId == record.EmployeeId
                           select row;

            if (response.Any())
            {

                var currentRecord = response.FirstOrDefault();

                currentRecord.FirstName = record.FirstName;
                currentRecord.LastName = record.LastName;
                currentRecord.DateOfJoining = record.DateOfJoining;
                currentRecord.Status = record.Status;
                currentRecord.Desination = record.Desination;
                //    base.Update(currentRecord);
            }
            else
            {
                _dbContext.Employees.Add(record);
            }

            return _dbContext.SaveChanges() > 0;
        }

        public Task<List<Employee>> GetAllEmployee()
        {
            var query = from b in _dbContext.Employees
                        orderby b.DateOfJoining
                        select b;
            return Task.FromResult(query.ToList());
        }

        public async Task<bool> RemoveEmployee(Employee record)
        {
            var response = _dbContext.Employees.Find(record.EmployeeId);
            _dbContext.Employees.Remove(response);

            return _dbContext.SaveChanges() > 0;
        }
    }
}
