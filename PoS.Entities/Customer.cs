﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoS.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        
        public string FullName { get; set; }

        public string TaxpayerID { get; set; }
        
        public string Address { get; set; }
        
        public string State { get; set; }
        
        public string City { get; set; }
        
        public string ZipCode { get; set; }

        public IList<Sale> Sales { get; set; }

        public Customer()
        {
            this.Sales = new List<Sale>();
        }
    }
}
