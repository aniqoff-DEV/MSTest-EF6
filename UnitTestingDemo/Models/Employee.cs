using System.ComponentModel.DataAnnotations;

namespace UnitTestingDemo.Models
{
    public class Employee
    {
        [Key]
        public Guid EmployeeId {  get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Department { get; set; }

        public DateTime DateOfJoining { get; set; } 

        public string Desination {  get; set; }

        public string Status {  get; set; }
    }
}
