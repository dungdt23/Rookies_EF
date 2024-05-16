using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rookies_EF.API.Dtos.RequestDtos;
using Rookies_EF.API.Dtos.ResponseDtos;
using Rookies_EF.API.Services;
using Rookies_EF.Common;

namespace Rookies_EF.API.Controllers
{
    [Route("projects")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }
        [HttpGet]
        public async Task<ApiReponse> GetProjects()
        {
            try
            {
                var projects = await _projectService.GetAllAsync();
                if (projects.Count() == 0)
                {
                    return new ApiReponse
                    {
                        Message = "Projects list is empty!"
                    };
                }
                else
                {
                    return new ApiReponse
                    {
                        Data = projects,
                        Message = "Get projects list successfully!"
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
        public async Task<ApiReponse> Add([FromBody] RequestProjectDto requestProjectDto)
        {
            try
            {
                var addStatus = await _projectService.AddAsync(requestProjectDto);
                if (addStatus == ConstantsStatus.Success)
                {
                    return new ApiReponse
                    {
                        Message = "Add a new project successfully!"
                    };
                }
                else
                {
                    return new ApiReponse
                    {
                        StatusCode = StatusCodes.Status500InternalServerError,
                        Message = "Add a new project failed!"
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
                var addStatus = await _projectService.DeleteAsync(id);
                if (addStatus == ConstantsStatus.Success)
                {
                    return new ApiReponse
                    {
                        Message = "Delete project successfully!"
                    };
                }
                else
                {
                    return new ApiReponse
                    {
                        StatusCode = StatusCodes.Status500InternalServerError,
                        Message = "Delete project failed!"
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
        public async Task<ApiReponse> Update(Guid id, [FromBody] RequestProjectDto requestProjectDto)
        {
            try
            {
                var addStatus = await _projectService.UpdateAsync(id, requestProjectDto);
                if (addStatus == ConstantsStatus.Success)
                {
                    return new ApiReponse
                    {
                        Message = "Update project successfully!"
                    };
                }
                else
                {
                    return new ApiReponse
                    {
                        StatusCode = StatusCodes.Status500InternalServerError,
                        Message = "Update project failed!"
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
