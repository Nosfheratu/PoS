using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoS.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; }
        
        public string Password { get; set; }
        
        public UserType UserType { get; set; }

        public IList<Workshift> Workshifts { get; set; }
        
        public IList<Sale> Sales { get; set; }

        public bool ValidatePassword(string password)
        {
            return this.Password.Equals(password);
        }

        public User()
        {
            this.Workshifts = new List<Workshift>();
            this.Sales = new List<Sale>();
        }
    }

    public enum UserType
    {
        Administrator = 1,
        Salesman = 2
    }
}
