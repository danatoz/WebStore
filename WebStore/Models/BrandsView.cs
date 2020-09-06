using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Models
{
    public class BrandsView : INamedEntity, IOrderEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set;; }
    }
}
