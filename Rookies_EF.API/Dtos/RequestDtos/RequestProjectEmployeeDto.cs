using System.ComponentModel.DataAnnotations;

namespace Rookies_EF.API.Dtos.RequestDtos
{
    public class RequestProjectEmployeeDto
    {
        [Required]
        public int ProjectId { get; set; }
        [Required]
        public int EmployeeId { get; set; }

    }
}
