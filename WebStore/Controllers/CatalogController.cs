using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain;
using WebStore.Infrastructure.Interfaces;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IProductService _productService;

        public CatalogController(IProductService productService)
        {
            _productService = productService;
        }
        public IActionResult Shop(int? categoriId, int? brandId)
        {
            var products = _productService.GetProducts(new ProductFilter
            {
                BrandId = brandId,
                CategoryId = categoriId
            });
            var model = new CatalogView()
            {
                BrandId = brandId,
                CategoryId = categoriId,
                Products = products.Select(p => new ProductView()
                {
                    Id = p.Id,
                    ImageUrl = p.ImageUrl,
                    Name = p.Name,
                    Order = p.Order,
                    Price = p.Price
                }).OrderBy(p => p.Order).ToList()
            };
            return View(model);
        }
        public IActionResult ProductDetails()
        {
            return View();
        }
    }
}