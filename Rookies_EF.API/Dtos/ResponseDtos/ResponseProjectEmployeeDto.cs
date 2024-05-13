using System.ComponentModel.DataAnnotations;

namespace Rookies_EF.API.Dtos.ResponseDtos
{
    public class ResponseProjectEmployeeDto
    {
        public int ProjectId { get; set; }
        public int EmployeeId { get; set; }
        public bool Enable { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
