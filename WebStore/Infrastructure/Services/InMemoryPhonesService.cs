using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Infrastructure.Interfaces;
using WebStore.Models;

namespace WebStore.Infrastructure.Services
{
    public class InMemoryPhonesService : IPhoneService
    {
        private readonly List<PhoneView> _phoneViews = new List<PhoneView>
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
        public void AddNew(PhoneView phoneView)
        {
            phoneView.Id = _phoneViews.Max(p => p.Id) + 1;
            _phoneViews.Add(phoneView);
        }

        public void Commit()
        {
            //TODO
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PhoneView> GetAll()
        {
            return _phoneViews;
        }

        public PhoneView GetById(int id)
        {
            return _phoneViews.FirstOrDefault(p => p.Id == id);
        }
    }
}
