using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rookies_EF.Common.GenericModel;

namespace Rookies_EFCore.Infrastructure.Models
{
    public class Project : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public ICollection<Employee> Employees { get; set; }

    }
}
