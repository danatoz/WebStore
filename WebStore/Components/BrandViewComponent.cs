using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Infrastructure.Interfaces;
using WebStore.Models;

namespace WebStore.Components
{
    [ViewComponent(Name = "Brands")]
    public class BrandViewComponent : ViewComponent
    {
        private readonly IProductService _productService;

        public BrandViewComponent(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var brands = GetBrands();
            return View(brands);
        }

        private IEnumerable<BrandsView> GetBrands()
        {
            var dbBrands = _productService.GetBrands();
            return dbBrands.Select(b => new BrandsView
            {
                Id = b.Id,
                Name = b.Name,
                Order = b.Order,
                ProductCount = 0
            }).OrderBy(b => b.Order).ToList();
        }
    }
}
