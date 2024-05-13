using Rookies_EFCore.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookies_EF.Infrastructure.QueryDtos
{
    public class EmployeeProject
    {
        public string EmployeeName { get; set; }
        public List<string> ProjectNames { get; set; }
    }
}
