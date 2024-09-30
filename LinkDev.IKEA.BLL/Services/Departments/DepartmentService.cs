using LinkDev.IKEA.BLL.Model.Department;
using LinkDev.IKEA.DAL.Entities.Departments;
using LinkDev.IKEA.DAL.Preisitance.Repositories.Departments;
using LinkDev.IKEA.DAL.Preisitance.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.Services.Departments
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        //private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IUnitOfWork unitOfWork)// ASK CLR for Creating Object from Class Implemnting the Interface "IUnitOfWork"--------------------------------Cancel  "IDepartmentRepositry"
        {
            _unitOfWork = unitOfWork;
            //_departmentRepository = departmentRepository;
        }
        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            var departments = _unitOfWork.DepartmentRepository
                            .GetIQueryable()
                            .Where(D => !D.IsDeleted)
                            .Select(department => new DepartmentDto()
            {
                Id = department.Id,
                Code = department.Code,
                Name = department.Name,
                //Description = department.Description,
                CreationDate = department.CreationDate,
            }).AsNoTracking().ToList();

            return departments;
            ///var departments= _departmentRepositry.GetAll();
            ///foreach (var department in departments)
            ///{
            ///    //yield return (DepartmentToReturnDto)department;
            ///    yield return new DepartmentToReturnDto
            ///    {
            ///        Id = department.Id,
            ///        Code = department.Code,
            ///        Name = department.Name,
            ///        Description = department.Description,
            ///        CreationDate = department.CreationDate,
            ///    };
            ///}
        }

        public DepartmentDetailsDto? GetDepartmentById(int id)
        {
            var department = _unitOfWork.DepartmentRepository.Get(id);
            if (department is { })
                return new DepartmentDetailsDto()
                {
                    Id = department.Id,
                    Code = department.Code,
                    Name = department.Name,
                    Description = department.Description,
                    CreationDate = department.CreationDate,
                    CreatedBy = department.CreatedBy,
                    CreatedOn = department.CreatedOn,
                    LastModifiedBy = department.LastModifiedBy,
                    LastModifiedOn = department.LastModifiedOn,
                };
            return null;
        }
        public int CreateDepartment(CreatedDepartmentDto departmentDto)
        {
            // Buisness Logic 
            var department = new Department()
            {
                Code = departmentDto.Code,
                Name = departmentDto.Name,
                Description = departmentDto.Description,
                CreationDate = departmentDto.CreationDate,
                CreatedBy = 1,
                //CreatedOn=DateTime.UtcNow, // Has Defualt Value 
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.UtcNow,
            };

            _unitOfWork.DepartmentRepository.Add(department);

            return _unitOfWork.CompleteAsync();  
        }

        public int UpdateDepartment(UpdatedDepartmentDto departmentDto)
        {
            // Buisness Logic 
            var department = new Department()
            {
                Id =departmentDto.Id,
                Code = departmentDto.Code,
                Name = departmentDto.Name,
                Description = departmentDto.Description,
                CreationDate = departmentDto.CreationDate,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.UtcNow,
            };

             _unitOfWork.DepartmentRepository.Update(department);
            return _unitOfWork.CompleteAsync();  
        }

        public bool DeleteDepartment(int id)
        {

            var departmentRepo= _unitOfWork.DepartmentRepository;   
            var department = departmentRepo.Get(id);

            if (department is { })
                 departmentRepo.Delete(department) ;
            return _unitOfWork.CompleteAsync() >0;
        }

        

        
    }
}
