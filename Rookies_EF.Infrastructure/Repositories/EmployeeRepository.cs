using Microsoft.EntityFrameworkCore;
using Rookies_EF.Common.GenericRepository;
using Rookies_EFCore.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookies_EF.Infrastructure.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly RookiesEFDBContext _context;
        public EmployeeRepository(RookiesEFDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Employee>> GetByDepartmentId(int departmentId)
        {
            return await _context.Employees
                .Where(x => x.DepartmentId == departmentId && !x.IsDeleted).ToListAsync();
        }
    }
}
