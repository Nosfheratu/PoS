using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Simple.Data;
using PoS.Entities;

namespace PoS.Data.Repositories
{
    public class CustomersRepository
    {
        dynamic db;

        public CustomersRepository()
        {
            db = Database.OpenFile(GetDBPath());
        }

        private string GetDBPath()
        {
            return @"C:\Users\Fernando Serapio\Documents\visual studio 2012\Projects\PoS\DB\PoSDB.sqlite";
        }

        public List<Customer> GetAll()
        {
            return db.Customers.All();
        }

        public Customer GetFirst()
        {
            return db.Customers.All().FirstOrDefault();
        }

        public Customer Find(string taxpayerID)
        {
            return db.Customers.Find(db.Customers.TaxpayerID == taxpayerID);
        }
    }
}
