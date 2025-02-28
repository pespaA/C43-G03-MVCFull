﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Models
{
    public class DepartmentToReturnDto
    {
        public int Id { get; set; }
        //public bool IsDeleted { get; set; }
        //public int CreatedBy { get; set; }
        //public DateTime CreatedOn { get; set; }
        //public int LastModificationBy { get; set; }
        //public DateTime LastModificationOn { get; set; }
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        [Display (Name="Date Of Creation")]
        public DateOnly CreationDate { get; set; }
    }
}
