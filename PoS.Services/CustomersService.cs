using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PoS.Entities;
using PoS.Data.Repositories;

namespace PoS.Services
{
    public class CustomersService
    {
        CustomersRepository cursomersRepository;

        public CustomersService()
        {
            cursomersRepository = new CustomersRepository();
        }

        public Customer GetDefaultCustomer()
        {
            return cursomersRepository.GetAll().FirstOrDefault();
        }

        public Customer Find(string taxId)
        {
            return cursomersRepository.Find(taxId);
        }
    }
}
