using LinkDev.IKEA.BLL.Model.Department;
using LinkDev.IKEA.BLL.Model.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.Services.Employees
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetEmployeesAsync(string search);//GetAllEmployees
       Task< EmployeeDetailsDto?> GetEmployeeByIdAsync(int id);

        Task<int> CreateEmployeeAsync(CreatedEmployeeDto employeeDto);

        Task<int> UpdateEmployeeAsync(UpdatedEmployeeDto employee);

        Task<bool> DeleteEmployeeAsync(int id);

    }
}
