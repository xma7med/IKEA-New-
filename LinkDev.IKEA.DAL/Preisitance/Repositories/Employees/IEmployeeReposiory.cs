using LinkDev.IKEA.DAL.Entities.Employees;
using LinkDev.IKEA.DAL.Preisitance.Repositories._Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Preisitance.Repositories.Employees
{
    public interface IEmployeeReposiory : IGenericRepository<Employee>
    {
        //Employee? Get(int id);
        //IEnumerable<Employee> GetAll(bool withAsNoTracking = true);

        //IQueryable<Employee> GetAllAsIQueryable();

        //int Add(Employee entity);
        //int Update(Employee entity);
        //int Delete(Employee entity);
    }
}
