using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rookies_EF.API.Dtos.RequestDtos;
using Rookies_EF.API.Dtos.ResponseDtos;
using Rookies_EF.API.Services;
using Rookies_EF.Common;

namespace Rookies_EF.API.Controllers
{
    [Route("project-employee")]
    [ApiController]
    public class ProjectEmployeeController : ControllerBase
    {
        private readonly IProjectEmployeeService _projectEmployeeService;
        public ProjectEmployeeController(IProjectEmployeeService projectEmployeeService)
        {
            _projectEmployeeService = projectEmployeeService;
        }
        [HttpGet]
        public async Task<ApiReponse> GetProjectEmployees()
        {
            try
            {
                var projectEmloyees = await _projectEmployeeService.GetAllAsync();
                if (projectEmloyees.Count() == 0)
                {
                    return new ApiReponse
                    {
                        Message = "Project-Employee list is empty!"
                    };
                }
                else
                {
                    return new ApiReponse
                    {
                        Data = projectEmloyees,
                        Message = "Get Project-Employee list successfully!"
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
        public async Task<ApiReponse> Add([FromBody] RequestProjectEmployeeDto requestProjectEmployeeDto)
        {
            try
            {
                var addStatus = await _projectEmployeeService.AddAsync(requestProjectEmployeeDto);
                if (addStatus == ConstantsStatus.Success)
                {
                    return new ApiReponse
                    {
                        Message = "Add a new Project-Employee successfully!"
                    };
                }
                else
                {
                    return new ApiReponse
                    {
                        StatusCode = StatusCodes.Status500InternalServerError,
                        Message = "Add a new Project-Employee failed!"
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
        [HttpDelete("{projectId}/{employeeId}")]
        public async Task<ApiReponse> Delete(Guid projectId, Guid employeeId)
        {
            try
            {
                var addStatus = await _projectEmployeeService.DeleteAsync(projectId, employeeId);
                if (addStatus == ConstantsStatus.Success)
                {
                    return new ApiReponse
                    {
                        Message = "Delete Project-Employee successfully!"
                    };
                }
                else
                {
                    return new ApiReponse
                    {
                        StatusCode = StatusCodes.Status500InternalServerError,
                        Message = "Delete Project-Employee failed!"
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
        [HttpPut]
        public async Task<ApiReponse> Update([FromBody] RequestProjectEmployeeDto requestProjectEmployeeDto)
        {
            try
            {
                var addStatus = await _projectEmployeeService.UpdateAsync(requestProjectEmployeeDto);
                if (addStatus == ConstantsStatus.Success)
                {
                    return new ApiReponse
                    {
                        Message = "Update Project-Employee successfully!"
                    };
                }
                else
                {
                    return new ApiReponse
                    {
                        StatusCode = StatusCodes.Status500InternalServerError,
                        Message = "Update Project-Employee failed!"
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
