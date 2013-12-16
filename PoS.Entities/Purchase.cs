using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoS.Entities
{
    public class Purchase
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string TransactionNumber { get; set; }
        public decimal Total { get; set; }
        public decimal Discount { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
