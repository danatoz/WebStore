using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Infrastructure.Interfaces;
using WebStore.Models;

namespace WebStore.Controllers
{
    [Route("phone")]
    public class PhoneController : Controller
    {
        private readonly IPhoneService _phoneService;
        public PhoneController(IPhoneService phoneService)
        {
            _phoneService = phoneService;
        }
        [Route("all")]
        public IActionResult PhoneList()
        {
            return View(_phoneService.GetAll());
        }
        [Route("phone/{id}")]
        public IActionResult PhoneDetails(int id)
        {
            var phone = _phoneService.GetById(id);
            if (_phoneService == null)
                return NotFound();
            return View(phone);
        }

        [HttpGet]
        [Route("edit/{id?}")]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue)
                return View(new PhoneView());
            var model = _phoneService.GetById(id.Value);
            if (model == null)
                return NotFound();
            return View(model);
        }
        [HttpPost]
        [Route("edit/{id?}")]
        public IActionResult Edit(PhoneView model)
        {
            if (model.Id > 0)
            {
                var dbItem = _phoneService.GetById(model.Id);

                if (ReferenceEquals(dbItem, null))
                    return NotFound();
                dbItem.Manufacturer = model.Manufacturer;
                dbItem.Model = model.Model;
                dbItem.Price = model.Price;
            }
            else
            {
                _phoneService.AddNew(model);
            }
            _phoneService.Commit();

            return RedirectToAction(nameof(PhoneList));
        }
        [Route("delete/{id}")]
        public IActionResult Delete(int id)
        {
            _phoneService.Delete(id);
            return RedirectToAction(nameof(PhoneList));
        }
    }
}