using Rookies_EF.API.Dtos.RequestDtos;
using Rookies_EF.API.Dtos.ResponseDtos;

namespace Rookies_EF.API.Services
{
    public interface ISalariesService
    {
        Task<IEnumerable<ResponseSalariesDto>> GetAllAsync();
        Task<int> AddAsync(RequestSalariesDto requestSalaryDto);
        Task<int> UpdateAsync(int id, RequestSalariesDto requestSalaryDto);
        Task<int> DeleteAsync(int id);
        Task<RequestSalariesDto?> GetByIdAsync(int id);
    }
}
