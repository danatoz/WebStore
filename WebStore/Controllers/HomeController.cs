using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class HomeController : Controller
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
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Employees()
        {
            return View(_employees);
        }
        public IActionResult EmployeeDetails(int id)
        {
            //Получаем сотродника по Id
            var employee = _employees.FirstOrDefault(t => t.Id == id);
            //Проверка на существует ли данный сотрудник
            if (employee == null)
                return NotFound();//404 NotFound
            //Возвращаем сотрудника
            return View(employee);
        }
    }
}