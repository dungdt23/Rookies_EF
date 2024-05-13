using Rookies_EFCore.Infrastructure.Models;
using System.ComponentModel.DataAnnotations;

namespace Rookies_EF.API.Dtos.ResponseDtos
{
    public class ResponseEmployeeDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime JoinedDate { get; set; }
        public ResponseDepartmentDto? Department { get; set; }
        public ICollection<ResponseProjectDto> Projects { get; set; }
    }
}
