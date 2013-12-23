using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoS.Entities
{
    public class SaleDetail
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int SaleId { get; set; }

        public Sale Sale { get; set; }

        public int Qty { get; set; }
    }
}
