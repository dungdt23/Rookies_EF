using AutoMapper;
using Rookies_EF.API.Dtos.RequestDtos;
using Rookies_EF.API.Dtos.ResponseDtos;
using Rookies_EF.Infrastructure.Repositories;
using Rookies_EFCore.Infrastructure.Models;

namespace Rookies_EF.API.Services
{
    public class SalariesService : ISalariesService
    {
        private readonly ISalariesRepository _salariesRepository;
        private readonly IMapper _mapper;
        public SalariesService(ISalariesRepository salariesRepository, IMapper mapper)
        {
            _mapper = mapper;
            _salariesRepository = salariesRepository;
        }
        public async Task<int> AddAsync(RequestSalariesDto requestSalaryDto)
        {
            var salary = _mapper.Map<Salaries>(requestSalaryDto);
            return await _salariesRepository.AddAsync(salary);
        }

        public async Task<IEnumerable<ResponseSalariesDto>> GetAllAsync()
        {
            var salaries = await _salariesRepository.GetAllAsync();
            var salaryDtos = _mapper.Map<IEnumerable<ResponseSalariesDto>>(salaries);
            return salaryDtos;
        }

        public async Task<RequestSalariesDto?> GetByIdAsync(Guid id)
        {
            var salary = await _salariesRepository.GetByIdAsync(id);
            var salaryDto = _mapper.Map<RequestSalariesDto>(salary);
            return salaryDto;
        }

        public async Task<int> UpdateAsync(Guid id, RequestSalariesDto requestSalaryDto)
        {
            var salary = _mapper.Map<Salaries>(requestSalaryDto);
            salary.Id = id;
            return await _salariesRepository.UpdateAsync(salary);
        }
    }
}
