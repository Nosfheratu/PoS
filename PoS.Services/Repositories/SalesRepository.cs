﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Simple.Data;
using PoS.Entities;

namespace PoS.Data.Repositories
{
    public class SalesRepository
    {
        dynamic db;

        public SalesRepository()
        {
            db = Database.OpenFile(GetDBPath());
        }

        private string GetDBPath()
        {
            return @"C:\Users\Fernando Serapio\Documents\visual studio 2012\Projects\PoS\DB\PoSDB.sqlite";
        }

        public Sale Add(Sale sale)
        {
            var newSale = this.db.Sales.Upsert(sale);

            return newSale;

            //System.Diagnostics.Debug.Print(sale.Id.ToString());

            //return sale;
        }

        public SaleDetail Add(SaleDetail saleItem)
        {
            return this.db.SalesDetails.Insert(saleItem);
        }
    }
}
