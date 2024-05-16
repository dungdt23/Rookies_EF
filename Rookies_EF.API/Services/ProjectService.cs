using AutoMapper;
using Rookies_EF.API.Dtos.RequestDtos;
using Rookies_EF.API.Dtos.ResponseDtos;
using Rookies_EF.Common.GenericRepository;
using Rookies_EF.Infrastructure.Repositories;
using Rookies_EFCore.Infrastructure.Models;

namespace Rookies_EF.API.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Project> _projectRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IProjectEmployeeRepository _projectEmployeeRepository;
        public ProjectService(IMapper mapper,
            IGenericRepository<Project> projectRepository,
            IEmployeeRepository employeeRepository,
            IProjectEmployeeRepository projectEmployeeRepository)
        {
            _mapper = mapper;
            _projectRepository = projectRepository;
            _employeeRepository = employeeRepository;
            _projectEmployeeRepository = projectEmployeeRepository;
        }
        public async Task<int> AddAsync(RequestProjectDto requestProjectDto)
        {
            var project = _mapper.Map<Project>(requestProjectDto);
            return await _projectRepository.AddAsync(project);
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            //if project is removed, remove constraint between project and employees
            var projectEmployees = await _employeeRepository.GetByProjectIdAsync(id);
            foreach (var projectEmployee in projectEmployees)
            {
                await _projectEmployeeRepository
                    .DeleteAsync(projectEmployee.ProjectId, projectEmployee.EmployeeId);
            }
            return await _projectRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ResponseProjectDto>> GetAllAsync()
        {
            var projects = await _projectRepository.GetAllAsync();
            var projectDtos = _mapper.Map<IEnumerable<ResponseProjectDto>>(projects);
            return projectDtos;
        }

        public async Task<ResponseProjectDto?> GetByIdAsync(Guid id)
        {
            var project = await _projectRepository.GetByIdAsync(id);
            var projectDto = _mapper.Map<ResponseProjectDto>(project);
            return projectDto;
        }

        public async Task<int> UpdateAsync(Guid id, RequestProjectDto requestProjectDto)
        {
            var project = _mapper.Map<Project>(requestProjectDto);
            project.Id = id;
            return await _projectRepository.UpdateAsync(project);
        }
    }
}
