using LinkDev.IKEA.BLL.Common.Services.Attachments;
using LinkDev.IKEA.BLL.Model.Employees;
using LinkDev.IKEA.DAL.Entities.Employees;
using LinkDev.IKEA.DAL.Preisitance.Repositories.Employees;
using LinkDev.IKEA.DAL.Preisitance.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.Services.Employees
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAttachmentService _attachmentService;

        //private readonly IEmployeeReposiory _employeeReposiory;

        public EmployeeService(IUnitOfWork unitOfWork,
            IAttachmentService attachmentService
            )// ASK CLR for Creating Object from Class Implemnting the Interface "IUnitOfWork"------------------------------- Cansel ==> "IEmployeeReposiory"
        {
            _unitOfWork = unitOfWork;
            _attachmentService = attachmentService;
            //_employeeReposiory = employeeReposiory;
        }


        public async Task<IEnumerable<EmployeeDto>> GetEmployeesAsync(string search)
        {
            return await _unitOfWork.EmployeeReposiory
                .GetIQueryable()
                .Where(E =>!E.IsDeleted && (string.IsNullOrEmpty(search) || E.Name.ToLower().Contains(search.ToLower()) ) )
                .Include(E=>E.Department)
                .Select(employee => new EmployeeDto()
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
                      Gender = employee.Gender.ToString(),
                      EmployeeType =employee.EmployeeType.ToString(),
                      Department=employee.Department.Name ,// اللقطه دي هيعملك ليزي لودينج 
                      Image = employee.Image,



                }) .ToListAsync();
        }

        public async Task<EmployeeDetailsDto?> GetEmployeeByIdAsync(int id)
        {
            var employee = await  _unitOfWork.EmployeeReposiory.GetAsync(id);
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
                    EmployeeType = employee.EmployeeType,
                    Department=employee.Department?.Name??"",
                    Image = employee.Image, 

                };
            return null;
        }

        public async Task<int> CreateEmployeeAsync(CreatedEmployeeDto employeeDto)
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
                DepartmentId = employeeDto.DepartmentId, 
                //Image= employeeDto.Image,   
                CreatedBy=1,
                LastModifiedBy=1,
                LastModifiedOn=DateTime.UtcNow,


            };

            if (employeeDto.Image != null)
                employee.Image =await  _attachmentService.UploadFileAsync(employeeDto.Image, "images");

            // ADD 
            // Update 
            // Delete 

             _unitOfWork.EmployeeReposiory.Add(employee);
            return await _unitOfWork.CompleteAsync();
        }
        public async  Task<int> UpdateEmployeeAsync(UpdatedEmployeeDto employeeDto)
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
                DepartmentId = employeeDto.DepartmentId,
                CreatedBy = 1,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.UtcNow,


            };

             _unitOfWork.EmployeeReposiory.Update(employee);   
            return await _unitOfWork.CompleteAsync();  
        }

        public async  Task<bool > DeleteEmployeeAsync(int id)
        {
            var employeeRepo= _unitOfWork.EmployeeReposiory;
            var employee = await  /*_employeeReposiory*/employeeRepo.GetAsync(id);
            if (employee is { })
                employeeRepo.Delete(employee);
            

            return await _unitOfWork.CompleteAsync()>0;
        } 

       
    }
}
