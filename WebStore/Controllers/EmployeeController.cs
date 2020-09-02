using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Infrastructure.Interfaces;
using WebStore.Models;

namespace WebStore.Controllers
{
    [Route("users")]
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
        public IActionResult Edit(EmployeeView model)
        {
            if (model.Id > 0)
            {
                var dbItem = _employeesService.GetById(model.Id);

                if (ReferenceEquals(dbItem, null))
                    return NotFound();

                dbItem.FirstName = model.FirstName;
                dbItem.SurName = model.SurName;
                dbItem.Age = model.Age;
                dbItem.Patronymic = model.Patronymic;
                dbItem.Position = model.Position;
            }
            else
            {
                _employeesService.AddNew(model);
            }
            _employeesService.Commit();

            return RedirectToAction(nameof(Employees));
        }
        [Route("all")]
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
        public IActionResult Delete(int id)
        {
            _employeesService.Delete(id);
            return RedirectToAction(nameof(Employees));
        }
    }
}