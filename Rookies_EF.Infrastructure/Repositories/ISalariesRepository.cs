using Rookies_EF.Common.GenericRepository;
using Rookies_EFCore.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookies_EF.Infrastructure.Repositories
{
    public interface ISalariesRepository : IGenericRepository<Salaries>
    {
        Task<Salaries?> GetByEmployeeIdAsync(Guid employeeId);
    }
}
