using Rookies_EF.API.Dtos.RequestDtos;
using Rookies_EF.API.Dtos.ResponseDtos;

namespace Rookies_EF.API.Services
{
    public interface ISalariesService
    {
        Task<IEnumerable<ResponseSalariesDto>> GetAllAsync();
        Task<int> AddAsync(RequestSalariesDto requestSalaryDto);
        Task<int> UpdateAsync(Guid id, RequestSalariesDto requestSalaryDto);
        Task<RequestSalariesDto?> GetByIdAsync(Guid id);
    }
}
