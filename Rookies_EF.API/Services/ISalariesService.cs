using Rookies_EF.API.Dtos.RequestDtos;
using Rookies_EF.API.Dtos.ResponseDtos;

namespace Rookies_EF.API.Services
{
    public interface ISalariesService
    {
        Task<IEnumerable<ResponseSalariesDto>> GetAllAsync();
        Task<int> AddAsync(RequestSalariesDto requestSalariesDto);
        Task<int> UpdateAsync(int id, RequestSalariesDto requestSalariesDto);
        Task<int> DeleteAsync(int id);
        Task<RequestSalariesDto?> GetByIdAsync(int id);
    }
}
