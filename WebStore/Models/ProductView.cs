using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Models
{
    public class ProductView : INamedEntity, IOrderEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public string BrandName { get; set; }
    }
}
