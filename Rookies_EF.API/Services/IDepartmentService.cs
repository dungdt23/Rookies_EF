using Rookies_EF.API.Dtos.RequestDtos;
using Rookies_EF.API.Dtos.ResponseDtos;

namespace Rookies_EF.API.Services
{
    public interface IDepartmentService
    {
        Task<IEnumerable<ResponseDepartmentDto>> GetAllAsync();
        Task<int> AddAsync(RequestDepartmentDto requestDepartmentDto);
        Task<int> UpdateAsync(int id, RequestDepartmentDto requestDepartmentDto);
        Task<int> DeleteAsync(int id);
        Task<ResponseDepartmentDto?> GetByIdAsync(int id);
    }
}
