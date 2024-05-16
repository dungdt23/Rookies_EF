using Rookies_EFCore.Infrastructure.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Rookies_EF.API.Dtos.ResponseDtos
{
    public class ResponseProjectDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
