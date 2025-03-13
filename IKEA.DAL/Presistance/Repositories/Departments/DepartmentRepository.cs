using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.DAL.Models.Departments;
using IKEA.DAL.Presistance.Data;
using IKEA.DAL.Presistance.Repositories._Generic;
using Microsoft.EntityFrameworkCore;

namespace IKEA.DAL.Presistance.Repositories.Departments
{
    public class DepartmentRepository : GenericRepository<Department>,IDepartmentRepository
    {
        public DepartmentRepository(ApplicationDbContext dbContext):base(dbContext)
        {
            // Ask CLR For object From ApplicationDbContext Implicitly
        }

        public IEnumerable<Department> GetSpecificDepartments()
        {
            throw new NotImplementedException();
        }
    }
}
