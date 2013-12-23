using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoS.Entities
{
    public class Sale
    {
        public int Id { get; set; }

        public DateTime Date { get { return DateTime.Now; } }
        
        public string TransactionNumber { get; set; }
        
        public decimal Total { get; set; }

        public decimal Amount { get; set; }
        
        public decimal Discount { get; set; }

        public IList<SaleDetail> SaleDetails { get; set; }

        public int UserId { get; set; }
        
        public User User { get; set; }

        public int CustomerId { get; set; }
        
        public Customer Customer { get; set; }

        public Sale()
        {
            this.SaleDetails = new List<SaleDetail>();
        }
    }
}
