﻿using Rookies_EF.API.Dtos.RequestDtos;
using Rookies_EF.API.Dtos.ResponseDtos;
using Rookies_EF.Infrastructure.QueryDtos;
using Rookies_EFCore.Infrastructure.Models;

namespace Rookies_EF.API.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<ResponseEmployeeDto>> GetAllAsync();
        Task<int> AddAsync(RequestEmployeeDto requestEmployeeDto);
        Task<int> UpdateAsync(Guid id, RequestEmployeeDto requestEmployeeDto);
        Task<int> DeleteAsync(Guid id);
        Task<ResponseEmployeeDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<EmployeeDepartment>> GetEmployeesWithDepartmentName();
        Task<IEnumerable<EmployeeProject>> GetEmployeesWithProjectName();
        Task<IEnumerable<EmployeeDetails>> GetEmployeesBaseOnSalaryAndJoinDate();
    }
}
