using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rookies_EF.API.Dtos.RequestDtos;
using Rookies_EF.API.Dtos.ResponseDtos;
using Rookies_EF.API.Services;
using Rookies_EF.Common;

namespace Rookies_EF.API.Controllers
{
    [Route("departments")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }
        [HttpGet]
        public async Task<ApiReponse> GetDepartments()
        {
            var departments = await _departmentService.GetAllAsync();
            if (departments.Count() == 0)
            {
                return new ApiReponse
                {
                    Message = "Department list is empty!"
                };
            }
            else
            {
                return new ApiReponse
                {
                    Data = departments,
                    Message = "Get department list successfully!"
                };
            }
        }
        [HttpPost]
        public async Task<ApiReponse> Add([FromBody] RequestDepartmentDto requestDepartmentDto)
        {
            var addStatus = await _departmentService.AddAsync(requestDepartmentDto);
            if (addStatus == ConstantsStatus.Success)
            {
                return new ApiReponse
                {
                    Message = "Add a new department successfully!"
                };
            }
            else
            {
                return new ApiReponse
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "Add a new department failed!"
                };
            }
        }
        [HttpDelete("{id}")]
        public async Task<ApiReponse> Delete(int id)
        {
            var addStatus = await _departmentService.DeleteAsync(id);
            if (addStatus == ConstantsStatus.Success)
            {
                return new ApiReponse
                {
                    Message = "Delete department successfully!"
                };
            }
            else
            {
                return new ApiReponse
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "Delete department failed!"
                };
            }
        }
        [HttpPut("{id}")]
        public async Task<ApiReponse> Update(int id, [FromBody] RequestDepartmentDto requestDepartmentDto)
        {
            var addStatus = await _departmentService.UpdateAsync(id, requestDepartmentDto);
            if (addStatus == ConstantsStatus.Success)
            {
                return new ApiReponse
                {
                    Message = "Update department successfully!"
                };
            }
            else
            {
                return new ApiReponse
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "Update department failed!"
                };
            }
        }
    }
}
