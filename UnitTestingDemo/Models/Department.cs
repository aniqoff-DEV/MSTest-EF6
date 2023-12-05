using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestingDemo.Models
{
    public class Department
    {
        [Key]
        public Guid DepartmentId { get; set; }

        public string Name { get; set; }

        public string HeadOfDepartment { get; set; }

        public string HeadCount { get; set; }

    }
}
