using System.ComponentModel.DataAnnotations;

namespace Rookies_EF.API.Dtos.RequestDtos
{
    public class RequestDepartmentDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

    }
}
