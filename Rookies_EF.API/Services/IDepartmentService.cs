using Rookies_EF.API.Dtos.RequestDtos;
using Rookies_EF.API.Dtos.ResponseDtos;

namespace Rookies_EF.API.Services
{
    public interface IDepartmentService
    {
        Task<IEnumerable<ResponseDepartmentDto>> GetAllAsync();
        Task<int> AddAsync(RequestDepartmentDto requestDepartmentDto);
        Task<int> UpdateAsync(Guid id, RequestDepartmentDto requestDepartmentDto);
        Task<int> DeleteAsync(Guid id);
        Task<ResponseDepartmentDto?> GetByIdAsync(Guid id);
    }
}
