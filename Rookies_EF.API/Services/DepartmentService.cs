using AutoMapper;
using Rookies_EF.API.Dtos.RequestDtos;
using Rookies_EF.API.Dtos.ResponseDtos;
using Rookies_EF.Common.GenericRepository;
using Rookies_EF.Infrastructure.Repositories;
using Rookies_EFCore.Infrastructure.Models;

namespace Rookies_EF.API.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IGenericRepository<Department> _departmentRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        public DepartmentService(IGenericRepository<Department> departmentRepository
            , IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }
        public async Task<int> AddAsync(RequestDepartmentDto requestDepartmentDto)
        {
            var department = _mapper.Map<Department>(requestDepartmentDto);
            return await _departmentRepository.AddAsync(department);
        }

        public async Task<int> DeleteAsync(int id)
        {
            //update employees have constraint with department
            var employees = await _employeeRepository.GetByDepartmentIdAsync(id);
            if (employees.Count() > 0)
            {
                foreach (var employee in employees)
                {
                    employee.DepartmentId = null;
                    await _employeeRepository.UpdateAsync(employee);
                }
            }
            return await _departmentRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ResponseDepartmentDto>> GetAllAsync()
        {
            var departments = await _departmentRepository.GetAllAsync();
            var departmentDtos = _mapper.Map<IEnumerable<ResponseDepartmentDto>>(departments);
            return departmentDtos;
        }

        public async Task<ResponseDepartmentDto?> GetByIdAsync(int id)
        {
            var department = await _departmentRepository.GetByIdAsync(id);
            var departmentDto = _mapper.Map<ResponseDepartmentDto>(department);
            return departmentDto;
        }

        public async Task<int> UpdateAsync(int id, RequestDepartmentDto requestDepartmentDto)
        {
            var department = _mapper.Map<Department>(requestDepartmentDto);
            department.Id = id;
            return await _departmentRepository.UpdateAsync(department);
        }
    }
}
