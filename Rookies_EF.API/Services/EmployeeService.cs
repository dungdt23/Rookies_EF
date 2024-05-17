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
        private readonly IGenericRepository<Project> _projectRepository;
        private readonly IGenericRepository<Department> _departmentRepository;
        private readonly IMapper _mapper;
        public EmployeeService(IEmployeeRepository employeeRepository,
            ISalariesRepository salariesRepository,
            IProjectEmployeeRepository projectEmployeeRepository,
            IGenericRepository<Project> projectRepository,
            IGenericRepository<Department> departmentRepository,
            IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _salariesRepository = salariesRepository;
            _projectEmployeeRepository = projectEmployeeRepository;
            _projectRepository = projectRepository;
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }
        public async Task<int> AddAsync(RequestEmployeeDto requestEmployeeDto)
        {
            //check if department is existed
            var department = await _departmentRepository.GetByIdAsync(requestEmployeeDto.DepartmentId);
            if (department == null) throw new Exception("Department is not found! Add failed!");
            Guid employeeId = Guid.NewGuid();
            var employee = _mapper.Map<Employee>(requestEmployeeDto);
            employee.Id = employeeId;
            var status = await _employeeRepository.AddAsync(employee);
            // handle adding salary for employee
            var requestSalary = new RequestSalariesDto { EmployeeId = employeeId, Salary = requestEmployeeDto.Salary };
            var salary = _mapper.Map<Salaries>(requestSalary);
            await _salariesRepository.AddAsync(salary);
            // update salaryId for employee
            // RequestEmployeeDto only contains amount of salary for user to easily input, so record will be lack of salaryId
            employee.SalaryId = salary.Id;
            await _employeeRepository.UpdateAsync(employee);
            // handle adding project for employee
            if (_projectRepository.GetByIdAsync(requestEmployeeDto.ProjectId) != null)
            {
                var requestProjectEmployee = new RequestProjectEmployeeDto { EmployeeId = employeeId, ProjectId = requestEmployeeDto.ProjectId };
                var projectEmployee = _mapper.Map<Project_Employee>(requestProjectEmployee);
                status = await _projectEmployeeRepository.AddAsync(projectEmployee);
            }
            else
            {
                throw new Exception("Add a new employee successfully! But project id is not found. You need to update later in Project-Employee");
            }
            return status;
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

        public async Task<int> UpdateAsync(Guid id, UpdateEmployeeDto updateEmployeeDto)
        {
            var department = await _departmentRepository.GetByIdAsync(updateEmployeeDto.DepartmentId);
            if (department == null) throw new Exception("Department is not found! Update failed!");
            var existedEmployee = await _employeeRepository.GetByIdAsync(id);
            if (existedEmployee != null)
            {
                existedEmployee.Name = updateEmployeeDto.Name;
                existedEmployee.JoinedDate = updateEmployeeDto.JoinedDate;
                existedEmployee.DepartmentId = updateEmployeeDto.DepartmentId;
                return await _employeeRepository.UpdateAsync(existedEmployee);
            }
            else
            {
                throw new Exception("Employee is  not found! Update failed!");
            }
        }
    }
}
