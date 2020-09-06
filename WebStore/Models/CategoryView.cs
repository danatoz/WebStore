using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entities.Base;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Models
{
    public class CategoryView : INamedEntity, IOrderEntity
    {
        public CategoryView()
        {
            ChildCategories = new List<CategoryView>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public List<CategoryView> ChildCategories { get; set; }

        public CategoryView ParentCategory { get; set; }
    }
}
