﻿using Rookies_EFCore.Infrastructure.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Rookies_EF.API.Dtos.ResponseDtos
{
    public class ResponseSalariesDto
    {
        public int Id { get; set; }
        public int Salary { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
