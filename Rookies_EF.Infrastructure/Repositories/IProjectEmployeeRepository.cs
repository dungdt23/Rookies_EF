using Rookies_EFCore.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookies_EF.Infrastructure.Repositories
{
    public interface IProjectEmployeeRepository
    {
        Task<IEnumerable<Project_Employee>> GetAllAsync();
        Task<int> AddAsync(Project_Employee project_Employee);
        Task<int> UpdateAsync(Project_Employee projectEmployee);
        Task<int> DeleteAsync(int projectId, int employeeId);
    }
}
