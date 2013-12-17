using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Simple.Data;
using PoS.Entities;

namespace PoS.Services
{
    public class UsersService
    {
        dynamic db;

        public UsersService()
        {
            db = Database.OpenFile(GetDBPath());
        }

        private string GetDBPath()
        {
            return @"C:\Users\Fernando Serapio\Documents\visual studio 2012\Projects\PoS\DB\PoSDB.sqlite";
        }

        public List<User> GetAll()
        {
            return this.db.Users.All();
        }

        public bool Login(int userId, string password)
        {
            User user = db.Users.FindById(userId);

            return user.ValidatePassword(password);
        }
    }
}
