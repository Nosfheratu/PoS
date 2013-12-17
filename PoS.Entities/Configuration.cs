using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoS.Entities
{
    public class Configuration
    {
        public int Id { get; set; }
        
        public string ShopName { get; set; }
        
        public string Addess { get; set; }
        
        public string Phone { get; set; }
        
        public string TicketsPrinter { get; set; }
        
        public string ReportsPrinter { get; set; }
    }
}
