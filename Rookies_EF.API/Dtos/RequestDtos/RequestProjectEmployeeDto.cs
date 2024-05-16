using System.ComponentModel.DataAnnotations;

namespace Rookies_EF.API.Dtos.RequestDtos
{
    public class RequestProjectEmployeeDto
    {
        [Required]
        public Guid ProjectId { get; set; }
        [Required]
        public Guid EmployeeId { get; set; }

    }
}
