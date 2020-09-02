using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Infrastructure.Interfaces;
using WebStore.Models;

namespace WebStore.Infrastructure.Services
{
    public class InMemoryEmployeesService : IEmployeesService
    {
        private readonly List<EmployeeView> _employees = new List<EmployeeView>
        {
            new EmployeeView
            {
                Id = 1,
                FirstName = "Иван" ,
                SurName = "Иванов" ,
                Patronymic = "Иванович" ,
                Age = 45,
                Position = "Директор"
            },
            new EmployeeView
            {
                Id = 2,
                FirstName = "Владислав" ,
                SurName = "Петров" ,
                Patronymic = "Иванович" ,
                Age = 22,
                Position = "Заместитель директора"
            }
        };

        public void AddNew(EmployeeView employeeView)
        {
            employeeView.Id = _employees.Max(e => e.Id) + 1;
            _employees.Add(employeeView);
        }

        public void Commit()
        {
            //TODO
        }

        public void Delete(int id)
        {
            var employee = GetById(id);
            if(employee is null)
                return;
            _employees.Remove(employee);
        }

        public IEnumerable<EmployeeView> GetAll()
        {
            return _employees;
        }

        public EmployeeView GetById(int id)
        {
            return _employees.FirstOrDefault(e => e.Id == id);
        }
    }
}
