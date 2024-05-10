using Rookies_EF.API.Dtos.RequestDtos;
using Rookies_EF.API.Dtos.ResponseDtos;

namespace Rookies_EF.API.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<ResponseEmployeeDto>> GetAllAsync();
        Task<int> AddAsync(RequestEmployeeDto requestEmployeeDto);
        Task<int> UpdateAsync(int id, RequestEmployeeDto requestEmployeeDto);
        Task<int> DeleteAsync(int id);
        Task<ResponseEmployeeDto?> GetByIdAsync(int id);
    }
}
