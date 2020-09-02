using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Models;

namespace WebStore.Infrastructure.Interfaces
{
    public interface IPhoneService
    {
        IEnumerable<PhoneView> GetAll();
        PhoneView GetById(int id);
        void Commit();
        void AddNew(PhoneView phoneView);
        void Delete(int id);

    }
}
