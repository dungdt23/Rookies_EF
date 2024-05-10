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
            CreateMap<RequestDepartmentDto, Department>();
            CreateMap<Department, ResponseDepartmentDto>();
        }
    }
}
