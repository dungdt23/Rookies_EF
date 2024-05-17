using System.ComponentModel.DataAnnotations;

namespace Rookies_EF.API.Dtos.RequestDtos
{
    public class UpdateEmployeeDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime JoinedDate { get; set; }
        [Required]
        public Guid DepartmentId { get; set; }
    }
}
