using LinkDev.IKEA.BLL.Model.Employees;
using LinkDev.IKEA.DAL.Entities.Employees;
using LinkDev.IKEA.DAL.Preisitance.Repositories.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.Services.Employees
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeReposiory _employeeReposiory;

        public EmployeeService(IEmployeeReposiory employeeReposiory)// ASK CLR for Creating Object from Class Implemnting the Interface "IEmployeeReposiory"
        {
            _employeeReposiory = employeeReposiory;
        }


        public IEnumerable<EmployeeDto> GetAllEmployees()
        {
            return _employeeReposiory.GetAllAsIQueryable().Select(employee => new EmployeeDto()
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
                Gender = nameof(employee.Gender),
                EmployeeType = nameof(employee.EmployeeType),

            });
        }

        public EmployeeDetailsDto? GetEmployeeById(int id)
        {
            var employee = _employeeReposiory.Get(id);
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
                    Gender = nameof(employee.Gender),
                    EmployeeType = nameof(employee.EmployeeType),

                };
            return null;
        }

        public int CreateEmployee(CreatedEmployeeDto employeeDto)
        {
            var employee = new Employee()
            { 
                Name = employeeDto.Name,
                Age = employeeDto.Age,
                Address = employeeDto.Address,  
                IsActive = employeeDto.IsActive,
                Salary= employeeDto.Salary,
                Email = employeeDto.Email,
                PhoneNumber = employeeDto.PhoneNumber,
                HiringDate = employeeDto.HiringDate,   
                Gender = employeeDto.Gender,    
                EmployeeType = employeeDto.EmployeeType,
                CreatedBy=1,
                LastModifiedBy=1,
                LastModifiedOn=DateTime.UtcNow,


            };

            return _employeeReposiory.Add(employee);
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
                Salary = employeeDto.Salary,
                Email = employeeDto.Email,
                PhoneNumber = employeeDto.PhoneNumber,
                HiringDate = employeeDto.HiringDate,
                Gender = employeeDto.Gender,
                EmployeeType = employeeDto.EmployeeType,
                CreatedBy = 1,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.UtcNow,


            };

            return _employeeReposiory.Update(employee);    
        }

        public bool DeleteEmployee(int id)
        {
            var employee = _employeeReposiory.Get(id);
            if (employee is { })
                return _employeeReposiory.Delete(employee)>0; 
            return false;
        }

       
    }
}
