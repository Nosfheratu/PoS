using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoS.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Barcode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal VAT { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public IList<Purchase> Purchases { get; set; }
    }
}
