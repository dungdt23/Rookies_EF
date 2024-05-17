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
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                project_Employee.CreatedAt = DateTime.Now;
                await _context.ProjectEmployees.AddAsync(project_Employee);
                int status = await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return status;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return ConstantsStatus.Failed;
            }

        }

        public async Task<int> DeleteAsync(Guid projectId, Guid employeeId)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var projectEmployee = await _context.ProjectEmployees
                    .FirstOrDefaultAsync(x => x.ProjectId == projectId && x.EmployeeId == employeeId && !x.IsDeleted);
                if (projectEmployee == null) return ConstantsStatus.Failed;
                projectEmployee.DeletedAt = DateTime.Now;
                projectEmployee.IsDeleted = true;
                projectEmployee.Enable = false;
                int status = await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return status;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return ConstantsStatus.Failed;
            }

        }

        public async Task<IEnumerable<Project_Employee>> GetAllAsync()
        {
            return await _context.ProjectEmployees.Where(x => !x.IsDeleted).ToListAsync();
        }

        public async Task<int> UpdateAsync(Project_Employee projectEmployee)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                projectEmployee.UpdatedAt = DateTime.Now;
                _context.ProjectEmployees.Update(projectEmployee);
                int status = await _context.SaveChangesAsync();
                return status;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return ConstantsStatus.Failed;
            }

        }
    }
}
