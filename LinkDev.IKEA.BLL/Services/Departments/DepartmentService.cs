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
        public async Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync()
        {
            var departmentRepo = _unitOfWork.DepartmentRepository;
            var departments = await departmentRepo
                            .GetIQueryable()
                            .Where(D => !D.IsDeleted)
                            .Select(department => new DepartmentDto()
            {
                Id = department.Id,
                Code = department.Code,
                Name = department.Name,
                //Description = department.Description,
                CreationDate = department.CreationDate,
            }).AsNoTracking().ToListAsync();

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

        public async Task<DepartmentDetailsDto?> GetDepartmentByIdAsync(int id)
        {
            var department =await _unitOfWork.DepartmentRepository.GetAsync(id);
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



        public async Task<int> CreateDepartmentAsync(CreatedDepartmentDto departmentDto)
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

            return await _unitOfWork.CompleteAsync();  
        }

        public async Task<int> UpdateDepartmentAsync(UpdatedDepartmentDto departmentDto)
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
            return await _unitOfWork.CompleteAsync();  
        }

        public async Task< bool> DeleteDepartmentAsync(int id)
        {

            var departmentRepo= _unitOfWork.DepartmentRepository;   
            var department = await departmentRepo.GetAsync(id);

            if (department is { })
                 departmentRepo.Delete(department) ;
            return await _unitOfWork.CompleteAsync() >0;
        }

        

        
    }
}
