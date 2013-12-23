using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PoS.Entities;
using PoS.Data.Repositories;

namespace PoS.Services
{
    public class SalesService
    {
        SalesRepository salesRepository;

        public SalesService()
        {
            salesRepository = new SalesRepository();
        }

        public Sale Create(Sale sale, decimal amount, decimal discount, decimal total, User user, Customer customer)
        {
            sale.Amount = amount;
            sale.Discount = discount;
            sale.Total = total;
            sale.TransactionNumber = Guid.NewGuid().ToString();
            sale.UserId = user.Id;
            sale.CustomerId = customer.Id;

            var newSale = salesRepository.Add(sale);
            
            sale.SaleDetails.ToList().ForEach(sd =>
            {
                sd.SaleId = newSale.Id;
                salesRepository.Add(sd);
            });

            return newSale;
        }
    }
}
