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

        public Sale Create(Sale sale, User user, Customer customer)
        {
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

        public int GetTicketNumber()
        {
            var lastSale = salesRepository.GetAll().LastOrDefault();

            return lastSale != null ? lastSale.Id + 1 : 1;
        }
    }
}
