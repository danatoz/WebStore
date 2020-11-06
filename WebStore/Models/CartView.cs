using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Models
{
    public class CartView
    {
        public Dictionary<ProductView, int> Items { get; set; }
        public int ItemsCount => Items?.Sum(x => x.Value) ?? 0;
    }
}
