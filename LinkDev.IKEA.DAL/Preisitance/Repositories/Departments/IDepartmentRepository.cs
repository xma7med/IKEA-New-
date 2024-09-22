using LinkDev.IKEA.DAL.Entities.Department;
using LinkDev.IKEA.DAL.Preisitance.Repositories._Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Preisitance.Repositories.Departments
{
    public interface IDepartmentRepository:IGenericRepository<Department>
    {
       //Department? Get(int id); 
       // IEnumerable<Department> GetAll(bool withAsNoTracking = true);

       // IQueryable<Department> GetAllAsIQueryable();

       // int Add (Department entity);
       // int Update (Department entity);
       // int Delete (Department entity);
    }
}
