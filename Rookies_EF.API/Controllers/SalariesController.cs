using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rookies_EF.API.Dtos.RequestDtos;
using Rookies_EF.API.Dtos.ResponseDtos;
using Rookies_EF.API.Services;
using Rookies_EF.Common;

namespace Rookies_EF.API.Controllers
{
    [Route("salaries")]
    [ApiController]
    public class SalariesController : ControllerBase
    {
        private readonly ISalariesService _salariesService;
        public SalariesController(ISalariesService salariesService)
        {
            _salariesService = salariesService;
        }
        [HttpGet]
        public async Task<ApiReponse> GetSalaries()
        {
            try
            {
                var salaries = await _salariesService.GetAllAsync();
                if (salaries.Count() == 0)
                {
                    return new ApiReponse
                    {
                        Message = "Salaries list is empty!"
                    };
                }
                else
                {
                    return new ApiReponse
                    {
                        Data = salaries,
                        Message = "Get Salaries list successfully!"
                    };
                }
            }
            catch (Exception ex)
            {

                return new ApiReponse
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.Status500InternalServerError
                }; ;
            }

        }
        [HttpPost]
        public async Task<ApiReponse> Add([FromBody] RequestSalariesDto requestSalariesDto)
        {
            try
            {
                var addStatus = await _salariesService.AddAsync(requestSalariesDto);
                if (addStatus == ConstantsStatus.Success)
                {
                    return new ApiReponse
                    {
                        Message = "Add a new salary successfully!"
                    };
                }
                else
                {
                    return new ApiReponse
                    {
                        StatusCode = StatusCodes.Status500InternalServerError,
                        Message = "Add a new salary failed!"
                    };
                }
            }
            catch (Exception ex)
            {

                return new ApiReponse
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.Status500InternalServerError
                }; ;
            }

        }
        [HttpDelete("{id}")]
        public async Task<ApiReponse> Delete(Guid id)
        {
            try
            {
                var addStatus = await _salariesService.DeleteAsync(id);
                if (addStatus == ConstantsStatus.Success)
                {
                    return new ApiReponse
                    {
                        Message = "Delete salary successfully!"
                    };
                }
                else
                {
                    return new ApiReponse
                    {
                        StatusCode = StatusCodes.Status500InternalServerError,
                        Message = "Delete salary failed!"
                    };
                }
            }
            catch (Exception ex)
            {

                return new ApiReponse
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.Status500InternalServerError
                }; ;
            }

        }
        [HttpPut("{id}")]
        public async Task<ApiReponse> Update(Guid id, [FromBody] RequestSalariesDto requestSalariesDto)
        {
            try
            {
                var addStatus = await _salariesService.UpdateAsync(id, requestSalariesDto);
                if (addStatus == ConstantsStatus.Success)
                {
                    return new ApiReponse
                    {
                        Message = "Update salary successfully!"
                    };
                }
                else
                {
                    return new ApiReponse
                    {
                        StatusCode = StatusCodes.Status500InternalServerError,
                        Message = "Update salary failed!"
                    };
                }
            }
            catch (Exception ex)
            {

                return new ApiReponse
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.Status500InternalServerError
                }; ;
            }

        }
    }
}
