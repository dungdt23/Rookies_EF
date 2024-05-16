using Rookies_EF.API.Dtos.RequestDtos;
using Rookies_EF.API.Dtos.ResponseDtos;

namespace Rookies_EF.API.Services
{
    public interface IProjectEmployeeService
    {
        Task<IEnumerable<ResponseProjectEmployeeDto>> GetAllAsync();
        Task<int> AddAsync(RequestProjectEmployeeDto requestProjectEmployeeDto);
        Task<int> UpdateAsync(RequestProjectEmployeeDto requestProjectEmployeeDto);
        Task<int> DeleteAsync(Guid projectId, Guid employeeId);
    }
}
