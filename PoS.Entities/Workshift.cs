﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoS.Entities
{
    public class Workshift
    {
        public int Id { get; set; }
        public DateTime Opening { get; set; }
        public DateTime Cierre { get; set; }
        public decimal CashAmount { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}