using AutoMapper;
using Rookies_EF.API.Dtos.RequestDtos;
using Rookies_EF.API.Dtos.ResponseDtos;
using Rookies_EF.Common.GenericRepository;
using Rookies_EF.Infrastructure.QueryDtos;
using Rookies_EF.Infrastructure.Repositories;
using Rookies_EFCore.Infrastructure.Models;

namespace Rookies_EF.API.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ISalariesRepository _salariesRepository;
        private readonly IProjectEmployeeRepository _projectEmployeeRepository;
        private readonly IMapper _mapper;
        public EmployeeService(IEmployeeRepository employeeRepository,
            ISalariesRepository salariesRepository,
            IProjectEmployeeRepository projectEmployeeRepository,
            IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _salariesRepository = salariesRepository;
            _projectEmployeeRepository = projectEmployeeRepository;
            _mapper = mapper;
        }
        public async Task<int> AddAsync(RequestEmployeeDto requestEmployeeDto)
        {
            Guid employeeId = Guid.NewGuid();
            var employee = _mapper.Map<Employee>(requestEmployeeDto);
            employee.Id = employeeId;
            // handle adding project for employee
            var requestProjectEmployee = new RequestProjectEmployeeDto { EmployeeId = employeeId, ProjectId = requestEmployeeDto.ProjectId };
            var projectEmployee = _mapper.Map<Project_Employee>(requestProjectEmployee);
            await _projectEmployeeRepository.AddAsync(projectEmployee);
            // handle adding salary for employee
            var requestSalary = new RequestSalariesDto { EmployeeId = employeeId, Salary = requestEmployeeDto.Salary };
            var salary = _mapper.Map<Salaries>(requestSalary);
            await _salariesRepository.AddAsync(salary);
            return await _employeeRepository.AddAsync(employee);
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            //handle constraint employee - salaries
            //salaries of employee will be removed if employee is removed
            var salaries = await _salariesRepository.GetByEmployeeIdAsync(id);
            if (salaries != null)
                await _salariesRepository.DeleteAsync(salaries.Id);
            return await _employeeRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ResponseEmployeeDto>> GetAllAsync()
        {
            var employees = await _employeeRepository.GetAllAsync();
            var employeeDtos = _mapper.Map<IEnumerable<ResponseEmployeeDto>>(employees);
            return employeeDtos;
        }

        public async Task<ResponseEmployeeDto?> GetByIdAsync(Guid id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            var employeeDto = _mapper.Map<ResponseEmployeeDto>(employee);
            return employeeDto;
        }

        public async Task<IEnumerable<EmployeeDetails>> GetEmployeesBaseOnSalaryAndJoinDate()
        {
            var employees = await _employeeRepository.GetEmployeesBaseOnSalaryAndJoinDate();
            return employees;
        }

        public async Task<IEnumerable<EmployeeDepartment>> GetEmployeesWithDepartmentName()
        {
            return await _employeeRepository.GetEmployeesWithDepartmentName();
        }

        public async Task<IEnumerable<EmployeeProject>> GetEmployeesWithProjectName()
        {
            return await _employeeRepository.GetEmployeesWithProjectName();
        }

        public async Task<int> UpdateAsync(Guid id, RequestEmployeeDto requestEmployeeDto)
        {
            var employee = _mapper.Map<Employee>(requestEmployeeDto);
            employee.Id = id;
            return await _employeeRepository.UpdateAsync(employee);
        }
    }
}
