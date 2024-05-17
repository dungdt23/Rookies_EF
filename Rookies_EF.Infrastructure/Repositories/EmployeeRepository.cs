using Microsoft.Data.SqlClient;
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
        public async Task<List<Employee>> GetByDepartmentIdAsync(Guid departmentId)
        {
            return await _context.Employees
                .Where(x => x.DepartmentId == departmentId && !x.IsDeleted).ToListAsync();
        }

        public async Task<List<Project_Employee>> GetByProjectIdAsync(Guid projectId)
        {
            return await _context.ProjectEmployees
                .Where(x => x.ProjectId == projectId && !x.IsDeleted)
                .ToListAsync();
        }

        public async Task<IEnumerable<EmployeeDepartment>> GetEmployeesWithDepartmentName()
        {

            //USING QUERY LINQ

            //var employeeDepartments = await (from employee in _context.Employees
            //                                 join department in _context.Departments
            //                                 on employee.DepartmentId equals department.Id
            //                                 select new EmployeeDepartment { EmployeeName = employee.Name, DepartmentName = department.Name })
            //                                 .ToListAsync();


            //USING RAW SQL QUERY

            var sql = "select e.Id, e.Name, e.JoinedDate, e.SalaryId, e.DepartmentId, e.CreatedAt, e.UpdatedAt, e.DeletedAt, e.IsDeleted from Employees e inner join Departments d\r\n" +
                "on e.DepartmentId = d.Id\r\n" +
                "where e.IsDeleted = 0 and d.IsDeleted = 0";
            var employees = await _context.Employees
                        .FromSqlRaw(sql)
                        .Include(x => x.Department)
                        .ToListAsync();
            var employeeDepartments = employees
                        .Select(e => new EmployeeDepartment
                        {
                            EmployeeName = e.Name,
                            DepartmentName = e.Department.Name,
                        })
                        .ToList();
            return employeeDepartments;
        }

        public async Task<IEnumerable<EmployeeProject>> GetEmployeesWithProjectName()
        {
            //USING QUERY LINQ

            //var query = from e in _context.Employees
            //            join pe in _context.ProjectEmployees on e.Id equals pe.EmployeeId
            //            where (e.IsDeleted == false && pe.IsDeleted == false)
            //            select new EmployeeProject
            //            {
            //                EmployeeName = e.Name,
            //                ProjectNames = e.Projects.Select(x => x.Name).ToList(),
            //            };
            ////due to inner join with linking table, we retrieve duplicated records, so need to distinct it
            //var distinctEmployeeProjects = RemoveDuplicated(await query.ToListAsync());


            //USING RAW SQL QUERY

            var sql = "select e.Id, e.Name, e.JoinedDate, e.SalaryId, e.DepartmentId, e.CreatedAt, e.UpdatedAt, e.DeletedAt, e.IsDeleted from Employees e inner join ProjectEmployees pe\r\n" +
                "on e.Id = pe.EmployeeId\r\n" +
                "where e.IsDeleted = 0 and pe.IsDeleted = 0\r\n" +
                "group by e.Name, e.Id, e.JoinedDate, e.SalaryId, e.DepartmentId, e.CreatedAt, e.UpdatedAt, e.DeletedAt, e.IsDeleted";
            var employees = await _context.Employees
                .FromSqlRaw(sql)
                .Include(x => x.Projects.Where(x => !x.IsDeleted))
                .ToListAsync();
            var employeeProjects = employees
                .Select(e => new EmployeeProject
                {
                    EmployeeName = e.Name,
                    ProjectNames = e.Projects.Select(x => x.Name).ToList(),
                })
                .ToList();
            return employeeProjects;
        }

        //This function is redundant if using raw query
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
            try
            {
                DateTime requiredDate = DateTime.Parse(ConstantsEmployeeDetail.Date);
                var date = new SqlParameter("date", requiredDate);
                var salary = new SqlParameter("salary", ConstantsEmployeeDetail.Salary);

                //USING QUERY LINQ

                //var query = from employee in _context.Employees
                //            join salaries in _context.Salaries on employee.Id equals salaries.EmployeeId
                //            where salaries.Salary > ConstantsEmployeeDetail.Salary &&
                //            employee.JoinedDate > requiredDate && !employee.IsDeleted
                //            select new EmployeeDetails
                //            {
                //                EmployeeName = employee.Name,
                //                Salary = salaries.Salary,
                //                JoinedDate = employee.JoinedDate
                //            };


                //USING RAW SQL QUERY

                var employees = await _context.Employees
                    .FromSqlRaw(@"select e.Id, e.Name, e.JoinedDate, e.SalaryId, e.DepartmentId, e.CreatedAt, e.UpdatedAt, e.DeletedAt, e.IsDeleted
                  from Employees e 
                  inner join Salaries s on e.SalaryId = s.Id
                  where s.Salary > @salary and e.JoinedDate > @date and e.IsDeleted = 0",
                                  salary, date) // Truyền tham số vào câu truy vấn
                    .Include(x => x.Salary)
                    .ToListAsync();
                var employeeDetails = employees
                                   .Select(e => new EmployeeDetails
                                   {
                                       EmployeeName = e.Name,
                                       Salary = e.Salary.Salary,
                                       JoinedDate = e.JoinedDate
                                   })
                                   .ToList();
                return employeeDetails;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
