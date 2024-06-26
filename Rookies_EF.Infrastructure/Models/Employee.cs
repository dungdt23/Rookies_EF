﻿using Rookies_EF.Common.GenericModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookies_EFCore.Infrastructure.Models
{
    public class Employee : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime JoinedDate { get; set; }
        public int? SalaryId { get; set; }
        public Salaries? Salary { get; set; }
        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }
        public ICollection<Project> Projects { get; set; }
    }
}
