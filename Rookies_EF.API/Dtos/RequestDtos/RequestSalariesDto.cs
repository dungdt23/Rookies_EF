using System.ComponentModel.DataAnnotations;

namespace Rookies_EF.API.Dtos.RequestDtos
{
    public class RequestSalariesDto
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Salary must be greater than 0.")]
        public int Salary { get; set; }
        [Required]
        public Guid EmployeeId { get; set; }
    }
}
