using LinkDev.IKEA.DAL.Preisitance.Data;
using LinkDev.IKEA.DAL.Preisitance.Repositories.Departments;
using LinkDev.IKEA.DAL.Preisitance.Repositories.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Preisitance.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplictaionDbContext _dbContext;

        public IEmployeeReposiory EmployeeReposiory => new EmployeeRepository(_dbContext);
        public IDepartmentRepository DepartmentRepository
        {
            get 
            {
                return new DepartmentRepository(_dbContext);
            }
        }



        public UnitOfWork(ApplictaionDbContext dbContext) // Ask CLR for Creating Object from Class "ApplictaionDbContext" - That Happens [ Implicitly ]
        {

            _dbContext = dbContext;
            //EmployeeReposiory = new EmployeeRepository(_dbContext);
            //DepartmentRepository = new DepartmentRepository(_dbContext);
        }




        public int Complete()
        {
            return _dbContext.SaveChanges();
        }

        public void Dispose()
        { 
            _dbContext.Dispose();
        }
    }
}
