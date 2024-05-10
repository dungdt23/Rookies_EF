using Rookies_EF.Common.GenericRepository;
using Rookies_EFCore.Infrastructure.Models;

namespace Rookies_EF.Infrastructure.Repositories
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        Task<List<Employee>> GetByDepartmentId(int departmentId);
    }
}
