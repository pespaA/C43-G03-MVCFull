using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.BLL.Models;
using IKEA.DAL.Models.Departments;
using IKEA.DAL.Presistance.Repositories.Departments;
using Microsoft.EntityFrameworkCore;

namespace IKEA.BLL.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository) 
        {
            _departmentRepository = departmentRepository;
        }
        public IEnumerable<DepartmentToReturnDto> GetAllDepartments()
        {
            var departments = _departmentRepository.GetAllAsQuerable()
                .Select(department => new DepartmentToReturnDto
            {
                Id = department.Id,
                Code = department.Code,
                Name = department.Name,
                CreationDate = department.CreationDate,
            }).AsNoTracking().ToList();
            return departments;
            //////////////////////////////////
            
        }
        public DepartmentDetailsToReturnDto? GetDepartmentById(int id)
        {
            var department = _departmentRepository.GetById(id);
            if (department is { })
            {
                return new DepartmentDetailsToReturnDto()
                {
                    Id = department.Id,
                    Code = department.Code,
                    Name = department.Name,
                    Description = department.Description,
                    CreationDate = department.CreationDate,
                    CreatedBy = department.CreatedBy,
                    CreatedOn = department.CreatedOn,
                    LastModificationBy = department.LastModificationBy,
                    LastModificationOn = department.LastModificationOn,
                };
            }
            return null;
        }
        public int CreateDepartment(CreatedDepartmentDto departmentDto)
        {
            var CreatedDerpartment = new Department() 
            {
                Code = departmentDto.Code,
                Name = departmentDto.Name,
                Description = departmentDto.Description,
                CreationDate = departmentDto.CreationDate,
                CreatedBy =1,
                LastModificationBy=1,
                LastModificationOn = DateTime.UtcNow,
            };
            return _departmentRepository.Add(CreatedDerpartment);
        }
        public int UpdateDepartment(UpdateDepartmentDto departmentDto)
        {
            var existingDepartment = _departmentRepository.GetById(departmentDto.Id);
            if (existingDepartment == null)
            {
                return 0;
            }
            existingDepartment.Code = departmentDto.Code;
            existingDepartment.Name = departmentDto.Name;
            existingDepartment.Description = departmentDto.Description;
            existingDepartment.CreationDate = departmentDto.CreationDate;
            existingDepartment.LastModificationBy = 1;
            existingDepartment.LastModificationOn = DateTime.UtcNow;
            return _departmentRepository.Update(existingDepartment);
            
        }
        public bool DeleteDepartment(int id)
        {
            var Department = _departmentRepository.GetById(id);
            if (Department is not null)
            {
                return _departmentRepository.Delete(Department)>0;
            }
            return false;
        }
    }
}
