using Microsoft.EntityFrameworkCore;
using UnitTestingDemo.Models;

namespace UnitTestingDemo
{
    public class DataContext : DbContext
    {
        public virtual DbSet<Employee> Employees { get; set; }

        public virtual DbSet<Department> Departments { get; set; }


        public DataContext()
        {
            
        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        
    }
}
