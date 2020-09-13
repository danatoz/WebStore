using System.Collections.Generic;

namespace WebStore.Models
{
    public class CatalogView
    {
        public int? BrandId { get; set; }
        public int? CategoryId { get; set; }
        public IEnumerable<ProductView> Products { get; set; }
    }
}
