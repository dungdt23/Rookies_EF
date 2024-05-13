using Microsoft.EntityFrameworkCore;
using Rookies_EF.Common;
using Rookies_EF.Common.GenericRepository;
using Rookies_EF.Infrastructure.QueryDtos;
using Rookies_EFCore.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Rookies_EF.Infrastructure.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly RookiesEFDBContext _context;
        public EmployeeRepository(RookiesEFDBContext context) : base(context)
        {
            _context = context;
        }
        public override async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _context.Employees
                .Where(x => !x.IsDeleted)
                .Include(x => x.Projects.Where(x => !x.IsDeleted))
                .Include(x => x.Department)
                .ToListAsync();
        }
        public async Task<List<Employee>> GetByDepartmentIdAsync(int departmentId)
        {
            return await _context.Employees
                .Where(x => x.DepartmentId == departmentId && !x.IsDeleted).ToListAsync();
        }

        public async Task<List<Project_Employee>> GetByProjectIdAsync(int projectId)
        {
            return await _context.ProjectEmployees
                .Where(x => x.ProjectId == projectId && !x.IsDeleted)
                .ToListAsync();
        }

        public async Task<IEnumerable<EmployeeDepartment>> GetEmployeesWithDepartmentName()
        {
            var employeeDepartments = await (from employee in _context.Employees
                                             join department in _context.Departments
                                             on employee.DepartmentId equals department.Id
                                             select new EmployeeDepartment { EmployeeName = employee.Name, DepartmentName = department.Name })
                                             .ToListAsync();
            return employeeDepartments;
        }

        public async Task<IEnumerable<EmployeeProject>> GetEmployeesWithProjectName()
        {
            var query = from e in _context.Employees
                        join pe in _context.ProjectEmployees on e.Id equals pe.EmployeeId
                        where (e.IsDeleted == false && pe.IsDeleted == false)
                        select new EmployeeProject
                        {
                            EmployeeName = e.Name,
                            ProjectNames = e.Projects.Select(x => x.Name).ToList(),
                        };
            //due to inner join with linking table, we retrieve duplicated records, so need to distinct it
            var distinctEmployeeProjects = RemoveDuplicated(await query.ToListAsync());
            return distinctEmployeeProjects;
        }
        private IEnumerable<EmployeeProject> RemoveDuplicated(IEnumerable<EmployeeProject> employeeProjects)
        {
            //declare a list to contain distinct record
            List<EmployeeProject> distinctEmployeeProjectst = new List<EmployeeProject>();
            foreach (var employeeProject in employeeProjects)
            {
                if (distinctEmployeeProjectst.FirstOrDefault(x => x.EmployeeName.Equals(employeeProject.EmployeeName)) == null)
                {
                    distinctEmployeeProjectst.Add(employeeProject);
                }
            }
            return distinctEmployeeProjectst.AsEnumerable();
        }
        public async Task<IEnumerable<EmployeeDetails>> GetEmployeesBaseOnSalaryAndJoinDate()
        {
            var query = from employee in _context.Employees
                        join salaries in _context.Salaries on employee.Id equals salaries.EmployeeId
                        where salaries.Salary > 100 && employee.JoinedDate > new DateTime(2024, 01, 01) && !employee.IsDeleted
                        select new EmployeeDetails
                        {
                            EmployeeName = employee.Name,
                            Salary = salaries.Salary,
                            JoinedDate = employee.JoinedDate
                        };
            return await query.ToListAsync();
        }
    }
}
