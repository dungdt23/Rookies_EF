using Rookies_EFCore.Infrastructure.Models;
using System.ComponentModel.DataAnnotations;

namespace Rookies_EF.API.Dtos.RequestDtos
{
    public class RequestEmployeeDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime JoinedDate { get; set; }
        [Required]
        public Guid DepartmentId { get; set; }
        [Required]
        public Guid ProjectId { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Salary must be greater than 0.")]
        public int Salary { get; set; }
    }
}
