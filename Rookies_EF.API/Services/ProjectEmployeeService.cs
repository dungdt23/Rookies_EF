using AutoMapper;
using Rookies_EF.API.Dtos.RequestDtos;
using Rookies_EF.API.Dtos.ResponseDtos;
using Rookies_EF.Infrastructure.Repositories;
using Rookies_EFCore.Infrastructure.Models;

namespace Rookies_EF.API.Services
{
    public class ProjectEmployeeService : IProjectEmployeeService
    {
        private readonly IProjectEmployeeRepository _projectEmployeeRepository;
        private readonly IMapper _mapper;
        public ProjectEmployeeService(IProjectEmployeeRepository projectEmployeeRepository, IMapper mapper)
        {
            _projectEmployeeRepository = projectEmployeeRepository;
            _mapper = mapper;
        }
        public async Task<int> AddAsync(RequestProjectEmployeeDto requestProjectEmployeeDto)
        {
            var projectEmployee = _mapper.Map<Project_Employee>(requestProjectEmployeeDto);
            return await _projectEmployeeRepository.AddAsync(projectEmployee);
        }

        public async Task<int> DeleteAsync(Guid projectId, Guid employeeId)
        {
            return await _projectEmployeeRepository.DeleteAsync(projectId, employeeId);
        }

        public async Task<IEnumerable<ResponseProjectEmployeeDto>> GetAllAsync()
        {
            var projectEmployees = await _projectEmployeeRepository.GetAllAsync();
            var projectEmployeeDtos = _mapper.Map<IEnumerable<ResponseProjectEmployeeDto>>(projectEmployees);
            return projectEmployeeDtos;
        }

        public async Task<int> UpdateAsync(RequestProjectEmployeeDto requestProjectEmployeeDto)
        {
            var projectEmployee = _mapper.Map<Project_Employee>(requestProjectEmployeeDto);
            return await _projectEmployeeRepository.UpdateAsync(projectEmployee);
        }
    }
}
