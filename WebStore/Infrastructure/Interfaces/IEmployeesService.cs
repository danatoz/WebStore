using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Models;

namespace WebStore.Infrastructure.Interfaces
{
    public interface IEmployeesService
    {
        IEnumerable<EmployeeView> GetAll();
        EmployeeView GetById(int id);
        void Commit();
        void AddNew(EmployeeView employeeView);
        void Delete(int id);
    }
}
