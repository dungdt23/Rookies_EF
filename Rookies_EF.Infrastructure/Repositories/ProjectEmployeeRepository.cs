using Microsoft.EntityFrameworkCore;
using Rookies_EF.Common;
using Rookies_EFCore.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookies_EF.Infrastructure.Repositories
{
    public class ProjectEmployeeRepository : IProjectEmployeeRepository
    {
        private readonly RookiesEFDBContext _context;
        public ProjectEmployeeRepository(RookiesEFDBContext context)
        {
            _context = context;
        }
        public async Task<int> AddAsync(Project_Employee project_Employee)
        {
            project_Employee.CreatedAt = DateTime.Now;
            await _context.ProjectEmployees.AddAsync(project_Employee);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int projectId, int employeeId)
        {
            var projectEmployee = await _context.ProjectEmployees
                .FirstOrDefaultAsync(x => x.ProjectId == projectId && x.EmployeeId == employeeId && !x.IsDeleted);
            if (projectEmployee == null) return ConstantsStatus.Failed;
            projectEmployee.DeletedAt = DateTime.Now;
            projectEmployee.IsDeleted = true;
            projectEmployee.Enable = false;
            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Project_Employee>> GetAllAsync()
        {
            return await _context.ProjectEmployees.Where(x => !x.IsDeleted).ToListAsync();
        }

        public async Task<int> UpdateAsync(Project_Employee projectEmployee)
        {
            projectEmployee.UpdatedAt = DateTime.Now;
            _context.ProjectEmployees.Update(projectEmployee);
            return await _context.SaveChangesAsync();
        }
    }
}
