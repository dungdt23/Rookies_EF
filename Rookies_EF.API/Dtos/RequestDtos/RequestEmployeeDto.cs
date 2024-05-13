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
        public int DepartmentId { get; set; }
    }
}
