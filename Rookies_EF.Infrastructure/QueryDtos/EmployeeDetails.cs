using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookies_EF.Infrastructure.QueryDtos
{
    public class EmployeeDetails
    {
        public string EmployeeName { get; set; }
        public int Salary { get; set; }
        public DateTime JoinedDate { get; set; }
    }
}
