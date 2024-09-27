using LinkDev.IKEA.DAL.Preisitance.Repositories.Departments;
using LinkDev.IKEA.DAL.Preisitance.Repositories.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Preisitance.UnitOfWork
{
    public interface IUnitOfWork:IDisposable
    {
        public IEmployeeReposiory EmployeeReposiory { get;  }
        public IDepartmentRepository DepartmentRepository { get;  }


        int Complete();
    }
}
