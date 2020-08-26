using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class PhoneController : Controller
    {
        public static IEnumerable<PhoneView> _phoneViews
        {
            get
            {
                return new List<PhoneView>
                {
                    new PhoneView
                    {
                        Id = 1,
                        Manufacturer = "IPhone",
                        Model = "12",
                        Price = 100000000,
                    },
                    new PhoneView
                    {
                        Id = 2,
                        Manufacturer = "Xiaomi",
                        Model = "Redmi 10",
                        Price = 10000,
                    },
                    new PhoneView
                    {
                        Id = 3,
                        Manufacturer = "Samsung",
                        Model = "S20",
                        Price = 100000,
                    }
                };
            }
        }
        public IActionResult PhoneList()
        {
            return View(_phoneViews);
        }
        public IActionResult PhoneDetails(int id)
        {
            var phone = _phoneViews.FirstOrDefault(p => p.Id == id);
            if (_phoneViews == null)
                return NotFound();
            return View(phone);
        }
    }
}