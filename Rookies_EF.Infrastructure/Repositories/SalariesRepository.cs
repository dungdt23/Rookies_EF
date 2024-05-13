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
    public class SalariesRepository : GenericRepository<Salaries>, ISalariesRepository
    {
        private readonly RookiesEFDBContext _context;
        public SalariesRepository(RookiesEFDBContext context) : base(context)
        {
            _context = context;

        }

        public async Task<Salaries?> GetByEmployeeIdAsync(int employeeId)
        {
            var salaries = await _context.Salaries
                .FirstOrDefaultAsync(x => x.EmployeeId == employeeId && !x.IsDeleted);
            return salaries;
        }
    }
}
