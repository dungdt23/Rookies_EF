using Rookies_EF.API.Dtos.RequestDtos;
using Rookies_EF.API.Dtos.ResponseDtos;

namespace Rookies_EF.API.Services
{
    public interface IProjectService
    {
        Task<IEnumerable<ResponseProjectDto>> GetAllAsync();
        Task<int> AddAsync(RequestProjectDto requestProjectDto);
        Task<int> UpdateAsync(int id, RequestProjectDto requestProjectDto);
        Task<int> DeleteAsync(int id);
        Task<ResponseProjectDto?> GetByIdAsync(int id);
    }
}
