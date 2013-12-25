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
            return cursomersRepository.GetFirst();
        }

        public Customer Find(string taxpayerID)
        {
            return cursomersRepository.Find(taxpayerID);
        }
    }
}
