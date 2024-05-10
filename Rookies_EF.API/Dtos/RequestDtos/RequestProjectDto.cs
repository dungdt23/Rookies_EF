using System.ComponentModel.DataAnnotations;

namespace Rookies_EF.API.Dtos.RequestDtos
{
    public class RequestProjectDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
