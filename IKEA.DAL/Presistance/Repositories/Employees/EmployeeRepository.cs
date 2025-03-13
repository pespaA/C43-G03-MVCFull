﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.DAL.Models.Employees;
using IKEA.DAL.Models.Employees;
using IKEA.DAL.Presistance.Data;
using IKEA.DAL.Presistance.Repositories._Generic;
using Microsoft.EntityFrameworkCore;

namespace IKEA.DAL.Presistance.Repositories.Employees
{
    internal class EmployeeRepository : GenericRepository<Employee>,IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext dbContext):base(dbContext)
        {
            // Ask CLR For object From ApplicationDbContext Implicitly

        }

    }
}
