using LinkDev.IKEA.DAL.Entities.Employees;
using LinkDev.IKEA.DAL.Preisitance.Data;
using LinkDev.IKEA.DAL.Preisitance.Repositories._Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Preisitance.Repositories.Employees
{
    public  class EmployeeRepository:GenaricRepository<Employee>,IEmployeeReposiory
    {
        public EmployeeRepository(ApplictaionDbContext dbContext)
            :base(dbContext)//Now ==> will not ask the obj will came from Class UnitOfWork - //  SBefore Implementing UnitOfWorkDesignPatteern ==> ( Ask CLR for object from ApplicationDbContext Implicilty )
        {
            
        }


        /// private readonly ApplictaionDbContext _dbContext;
        ///
        /// public EmployeeRepository(ApplictaionDbContext dbContext) // Ask CLR for object from ApplicationDbContext Implicilty
        /// {
        ///     _dbContext = dbContext;
        /// }
        ///
        /// public IEnumerable<Employee> GetAll(bool withAsNoTracking = true)
        /// {
        ///
        ///     if (withAsNoTracking)
        ///         return _dbContext.Employees.AsNoTracking().ToList();
        ///
        ///     return _dbContext.Employees.ToList(); ;
        /// }
        /// public IQueryable<Employee> GetAllAsIQueryable()
        /// {
        ///     return _dbContext.Employees;
        /// }
        /// public Employee? Get(int id)
        /// {
        ///     return _dbContext.Employees.Find(id);
        ///     //return _dbContext.Find<Employee>(id);
        ///
        ///     ///     var Employee = _dbContext.Employees.Local.FirstOrDefault(D => D.Id == id);
        ///     /// if (Employee == null)
        ///     ///     _dbContext.Employees.FirstOrDefault(D => D.Id == id);
        ///     /// return Employee;
        ///
        /// }
        /// public int Add(Employee entity)
        /// {
        ///     // 4 ways to add 
        ///     _dbContext.Employees.Add(entity);
        ///     return _dbContext.SaveChanges();
        /// }
        ///
        /// public int Update(Employee entity)
        /// {
        ///     _dbContext.Employees.Update(entity);
        ///     return _dbContext.SaveChanges();
        /// }
        ///
        /// public int Delete(Employee entity)
        /// {
        ///     _dbContext.Employees.Remove(entity);
        ///     return _dbContext.SaveChanges();
        /// }


    }
}
