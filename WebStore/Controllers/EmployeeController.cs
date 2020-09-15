using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore.Infrastructure.Interfaces;
using WebStore.Models;

namespace WebStore.Controllers
{
    [Route("users")]
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IEmployeesService _employeesService;

        public EmployeeController(IEmployeesService employeesService)
        {
            _employeesService = employeesService;
        }/// <summary>
         /// Редактирование сотрудников и создание новых сотрудников
         /// </summary>
         /// <param name="id"></param>
         /// <returns></returns>
        [HttpGet]
        [Route("edit/{id?}")]
        [Authorize(Roles = "Admins")]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue)
                return View(new EmployeeView());
            var model = _employeesService.GetById(id.Value);
            if (model == null)
                return NotFound();
            return View(model);
        }
        [HttpPost]
        [Route("edit/{id?}")]
        [Authorize(Roles = "Admins")]
        public IActionResult Edit(EmployeeView model)
        {
            if(model.Age < 18 || model.Age > 100)
                ModelState.AddModelError("Age", "Ошибка возраста!");
            if (!ModelState.IsValid)
                return View(model);
            if (model.Id > 0)
            {
                var dbItem = _employeesService.GetById(model.Id);

                if (ReferenceEquals(dbItem, null))
                    return NotFound();

                dbItem.FirstName = model.FirstName;
                dbItem.SurName = model.SurName;
                dbItem.Age = model.Age;
                dbItem.Patronymic = dbItem.Patronymic;
                dbItem.Position = dbItem.Position;
            }
            else
            {
                _employeesService.AddNew(model);
            }
            _employeesService.Commit();

            return RedirectToAction(nameof(Employees));
        }
        [Route("all")]
        [AllowAnonymous]
        public IActionResult Employees()
        {
            return View(_employeesService.GetAll());
        }/// <summary>
         /// подробная информация о сотруднике
         /// </summary>
         /// <param name="id"></param>
         /// <returns></returns>
        [Route("{id}")]
        public IActionResult EmployeeDetails(int id)
        {
            var employee = _employeesService.GetById(id);
            if (_employeesService == null)
                return NotFound();
            return View(employee);
        }
        [Route("delete/{id}")]
        [Authorize(Roles = "Admins")]
        public IActionResult Delete(int id)
        {
            _employeesService.Delete(id);
            return RedirectToAction(nameof(Employees));
        }
    }
}