using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PoS.Entities;
using PoS.Data.Repositories;

namespace PoS.Services
{
    public class ProductsService
    {
        ProductsRepository productsRepository;
        
        public ProductsService()
        {
            productsRepository = new ProductsRepository();
        }
        
        public Product Find(string barcode)
        {
            return productsRepository.GetAll().SingleOrDefault(p => p.Barcode == barcode);
        }
    }
}
