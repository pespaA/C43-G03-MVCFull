﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.BLL.Models.Employees;
using IKEA.DAL.Models.Employees;
using IKEA.DAL.Presistance.Repositories.Employees;

namespace IKEA.BLL.Services.Employees
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public int CreateEmployee(CreatedEmployeeDto employeeDto)
        {
            var employee = new Employee()
            {
                Name = employeeDto.Name,
                Age = employeeDto.Age,
                Address = employeeDto.Address,
                IsActive = employeeDto.IsActive,
                Salary = employeeDto.Salary,
                Email = employeeDto.Email,
                PhoneNumber = employeeDto.PhoneNumber,
                HiringDate = employeeDto.HiringDate,
                Gender = employeeDto.Gender,
                EmployeeType = employeeDto.EmployeeType,
                CreatedBy = 1,
                LastModificationBy = 1,
                LastModificationOn = DateTime.UtcNow,
            };
            return _employeeRepository.Add(employee);
        }

        public int UpdateEmployee(UpdatedEmployeeDto employeeDto)
        {
            var employee = new Employee()
            {
                Id = employeeDto.Id,
                Name = employeeDto.Name,
                Age = employeeDto.Age,
                Address = employeeDto.Address,
                IsActive = employeeDto.IsActive,
                Salary = Convert.ToDecimal(employeeDto.Salary),
                Email = employeeDto.Email,
                PhoneNumber = employeeDto.PhoneNumber,
                HiringDate = employeeDto.HiringDate,
                Gender = employeeDto.Gender,
                EmployeeType = employeeDto.EmployeeType,
                CreatedBy = 1,
                LastModificationBy = 1,
                LastModificationOn = DateTime.UtcNow,
            };
            return _employeeRepository.Update(employee);
        }
        public bool DeleteEmployee(int id)
        {
            var employee = _employeeRepository.GetById(id);
            if (employee is { })
                return _employeeRepository.Delete(employee)>0;
            return false;
        }

        public IEnumerable<EmployeeDto> GetAllEmployees()
        {
            return _employeeRepository.GetAllAsQuerable()
                .Where(E=>!E.IsDeleted)
                .Select(employeeDto => new EmployeeDto
                {
                    Id = employeeDto.Id,
                    Name = employeeDto.Name,
                    Age = employeeDto.Age,
                    IsActive = employeeDto.IsActive,
                    Salary = employeeDto.Salary,
                    Email = employeeDto.Email, 
                    Gender = employeeDto.Gender.ToString(),
                    EmployeeType = employeeDto.EmployeeType.ToString(),
                });
        }

        public EmployeeDetailsDto? GetEmployeeById(int id)
        {
            var employee = _employeeRepository.GetById(id);
            if (employee is { })
                return new EmployeeDetailsDto()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Age = employee.Age,
                    Address = employee.Address,
                    IsActive = employee.IsActive,
                    Salary = employee.Salary,
                    Email = employee.Email,
                    PhoneNumber = employee.PhoneNumber,
                    HiringDate = employee.HiringDate,
                    Gender = employee.Gender,
                    EmployeeType =employee.EmployeeType,
                };
            return null;
        }

        
    }
}
