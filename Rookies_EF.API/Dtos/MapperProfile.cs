using AutoMapper;
using Rookies_EF.API.Dtos.RequestDtos;
using Rookies_EF.API.Dtos.ResponseDtos;
using Rookies_EFCore.Infrastructure.Models;

namespace Rookies_EF.API.Dtos
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            //configure for department dto
            CreateMap<RequestDepartmentDto, Department>();
            CreateMap<Department, ResponseDepartmentDto>();
            //configure for employee dto
            CreateMap<RequestEmployeeDto, Employee>()
                .ForMember(dest => dest.Salary, opt => opt.Ignore());
            CreateMap<UpdateEmployeeDto, Employee>();
            CreateMap<Employee, ResponseEmployeeDto>();
            //configure for project dto
            CreateMap<RequestProjectDto, Project>();
            CreateMap<Project, ResponseProjectDto>();
            //configure for salaries dto
            CreateMap<RequestSalariesDto, Salaries>();
            CreateMap<Salaries, ResponseSalariesDto>();
            //configure for project-employee dto
            CreateMap<RequestProjectEmployeeDto, Project_Employee>();
            CreateMap<Project_Employee, ResponseProjectEmployeeDto>();
        }
    }
}
