using LinkDev.IKEA.DAL.Entities.Department;
using LinkDev.IKEA.DAL.Preisitance.Data;
using LinkDev.IKEA.DAL.Preisitance.Repositories._Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Preisitance.Repositories.Departments
{
    public class DepartmentRepository : GenaricRepository<Department> , IDepartmentRepository
    {
        public DepartmentRepository(ApplictaionDbContext dbContext):base(dbContext) // Ask CLR for object from ApplicationDbContext Implicilty
        {
            
        }
        


        /// //private readonly ApplictaionDbContext _dbContext;
        /// 
        /// //public DepartmentRepository(ApplictaionDbContext dbContext) // Ask CLR for object from ApplicationDbContext Implicilty
        /// //{
        /// //    _dbContext = dbContext;
        /// //}
        /// 
        /// //public IEnumerable<Department> GetAll(bool withAsNoTracking = true)
        /// //{
        /// 
        /// //    if (withAsNoTracking) 
        /// //         return _dbContext.Departments.AsNoTracking().ToList();
        /// 
        /// //    return _dbContext.Departments.ToList(); ;
        /// //}
        /// //public IQueryable<Department> GetAllAsIQueryable()
        /// //{
        /// //    return _dbContext.Departments;
        /// //}
        /// //public Department? Get(int id)
        /// //{ 
        /// //    return  _dbContext.Departments.Find(id);
        /// //    //return _dbContext.Find<Department>(id);
        /// 
        /// //   ///     var department = _dbContext.Departments.Local.FirstOrDefault(D => D.Id == id);
        /// //   /// if (department == null)
        /// //   ///     _dbContext.Departments.FirstOrDefault(D => D.Id == id);
        /// //   /// return department;
        /// 
        /// //}
        /// //public int Add(Department entity)
        /// //{
        /// //    // 4 ways to add 
        /// //    _dbContext.Departments.Add(entity); 
        /// //    return _dbContext.SaveChanges();
        /// //}
        /// 
        /// //public int Update(Department entity)
        /// //{
        /// //    _dbContext.Departments.Update(entity);
        /// //    return _dbContext.SaveChanges();
        /// //}
        /// 
        /// //public int Delete(Department entity)
        /// //{
        /// //    _dbContext.Departments.Remove(entity);
        /// //    return _dbContext.SaveChanges();
        /// //}


    }
}
