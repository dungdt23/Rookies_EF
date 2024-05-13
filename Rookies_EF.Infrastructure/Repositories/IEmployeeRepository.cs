using Rookies_EF.Common.GenericRepository;
using Rookies_EF.Infrastructure.QueryDtos;
using Rookies_EFCore.Infrastructure.Models;

namespace Rookies_EF.Infrastructure.Repositories
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        Task<List<Employee>> GetByDepartmentIdAsync(int departmentId);
        Task<List<Project_Employee>> GetByProjectIdAsync(int projectId);
        Task<IEnumerable<EmployeeDepartment>> GetEmployeesWithDepartmentName();
        Task<IEnumerable<EmployeeProject>> GetEmployeesWithProjectName();
        Task<IEnumerable<EmployeeDetails>> GetEmployeesBaseOnSalaryAndJoinDate();

    }
}
