using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Simple.Data;
using PoS.Entities;

namespace PoS.Data.Repositories
{
    public class ProductsRepository
    {
        dynamic db;

        public ProductsRepository()
        {
            db = Database.OpenFile(GetDBPath());
        }

        private string GetDBPath()
        {
            return @"C:\Users\Fernando Serapio\Documents\visual studio 2012\Projects\PoS\DB\PoSDB.sqlite";
        }

        public List<Product> GetAll()
        {
            return db.Products.All();
        }
    }
}
