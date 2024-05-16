using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rookies_EF.API.Dtos.RequestDtos;
using Rookies_EF.API.Dtos.ResponseDtos;
using Rookies_EF.API.Services;
using Rookies_EF.Common;

namespace Rookies_EF.API.Controllers
{
    [Route("employees")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        [HttpGet]
        public async Task<ApiReponse> GetEmployees()
        {
            try
            {
                var employees = await _employeeService.GetAllAsync();
                if (employees.Count() == 0)
                {
                    return new ApiReponse
                    {
                        Message = "Employees list is empty!"
                    };
                }
                else
                {
                    return new ApiReponse
                    {
                        Data = employees,
                        Message = "Get employees list successfully!"
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
        public async Task<ApiReponse> Add([FromBody] RequestEmployeeDto requestEmployeeDto)
        {
            try
            {
                var addStatus = await _employeeService.AddAsync(requestEmployeeDto);
                if (addStatus == ConstantsStatus.Success)
                {
                    return new ApiReponse
                    {
                        Message = "Add a new employee successfully!"
                    };
                }
                else
                {
                    return new ApiReponse
                    {
                        StatusCode = StatusCodes.Status500InternalServerError,
                        Message = "Add a new employee failed!"
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
                var addStatus = await _employeeService.DeleteAsync(id);
                if (addStatus == ConstantsStatus.Success)
                {
                    return new ApiReponse
                    {
                        Message = "Delete employee successfully!"
                    };
                }
                else
                {
                    return new ApiReponse
                    {
                        StatusCode = StatusCodes.Status500InternalServerError,
                        Message = "Delete employee failed!"
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
        public async Task<ApiReponse> Update(Guid id, [FromBody] RequestEmployeeDto requestEmployeeDto)
        {
            try
            {
                var addStatus = await _employeeService.UpdateAsync(id, requestEmployeeDto);
                if (addStatus == ConstantsStatus.Success)
                {
                    return new ApiReponse
                    {
                        Message = "Update employee successfully!"
                    };
                }
                else
                {
                    return new ApiReponse
                    {
                        StatusCode = StatusCodes.Status500InternalServerError,
                        Message = "Update employee failed!"
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
        [HttpGet("employee-department")]
        public async Task<ApiReponse> GetEmployeesWithDepartmentName()
        {
            try
            {
                var employees = await _employeeService.GetEmployeesWithDepartmentName();
                if (employees.Count() == 0)
                {
                    return new ApiReponse
                    {
                        Message = "Employees list is empty!"
                    };
                }
                else
                {
                    return new ApiReponse
                    {
                        Data = employees,
                        Message = "Get employees list successfully!"
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
        [HttpGet("employee-project")]
        public async Task<ApiReponse> GetEmployeesWithProjectName()
        {
            try
            {
                var employees = await _employeeService.GetEmployeesWithProjectName();
                if (employees.Count() == 0)
                {
                    return new ApiReponse
                    {
                        Message = "Employees list is empty!"
                    };
                }
                else
                {
                    return new ApiReponse
                    {
                        Data = employees,
                        Message = "Get employees list successfully!"
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
        [HttpGet("details")]
        public async Task<ApiReponse> GetEmployeesBaseOnSalaryAndJoinDate()
        {
            try
            {
                var employees = await _employeeService.GetEmployeesBaseOnSalaryAndJoinDate();
                if (employees.Count() == 0)
                {
                    return new ApiReponse
                    {
                        Message = "Employees list is empty!"
                    };
                }
                else
                {
                    return new ApiReponse
                    {
                        Data = employees,
                        Message = "Get employees list successfully!"
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
