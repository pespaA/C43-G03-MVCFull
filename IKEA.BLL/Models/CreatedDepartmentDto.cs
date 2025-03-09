﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Models
{
    public class CreatedDepartmentDto
    {
        [Required(ErrorMessage = "Name IS Required !!!")]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "Code IS Required !!!")]
        public string Code { get; set; } = null!;
        public string? Description { get; set; }
        [Display(Name="Date Of Creation")]
        public DateOnly CreationDate { get; set; }
    }
}
