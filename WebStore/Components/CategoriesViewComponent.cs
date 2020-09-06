using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Infrastructure.Interfaces;
using WebStore.Models;

namespace WebStore.Components
{
    [ViewComponent(Name = "Cats")]
    public class CategoriesViewComponent : ViewComponent
    {
        private readonly IProductService _productService;

        public CategoriesViewComponent(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var Categories = GetCategories();
            return View(Categories);
        }

        private List<CategoryView> GetCategories()
        {
            var categories = _productService.GetCategories();
            //получаем и заполняем родительские категории
            var parentSections = categories.Where(p => !p.ParentId.HasValue).ToArray();
            var parentCategories = new List<CategoryView>();
            foreach (var parentCategory in parentSections)
            {
                parentCategories.Add(new CategoryView()
                {
                    Id = parentCategory.Id,
                    Name = parentCategory.Name,
                    Order = parentCategory.Order,
                    ParentCategory = null
                });
            }

            //получаем и заполняем дочерние категории
            foreach (var CategoryModel in parentCategories)
            {
                var childCategories = categories.Where(c => c.ParentId == CategoryModel.Id);
                foreach (var childCategory in childCategories)
                {
                    CategoryModel.ChildCategories.Add(new CategoryView()
                    {
                        Id = childCategory.Id,
                        Name = childCategory.Name,
                        Order = childCategory.Order,
                        ParentCategory = CategoryModel
                    });
                }

                CategoryModel.ChildCategories = CategoryModel.ChildCategories.OrderBy(c => c.Order).ToList();
            }

            parentCategories = parentCategories.OrderBy(c => c.Order).ToList();
            return parentCategories;
        }
    }
}
