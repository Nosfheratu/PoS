using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Simple.Data;
using PoS.Entities;

namespace PoS.Data.Repositories
{
    public class WorkshiftsRepository
    {
        dynamic db;

        public WorkshiftsRepository()
        {
            db = Database.OpenFile(GetDBPath());
        }

        private string GetDBPath()
        {
            return @"C:\Users\Fernando Serapio\Documents\visual studio 2012\Projects\PoS\DB\PoSDB.sqlite";
        }

        public Workshift Add(Workshift workshift)
        {
            this.db.Workshifts.Insert(workshift);
            return workshift;
        }

        public List<Workshift> GetAll()
        {
            return this.db.Workshifts.All();
        }

        public void Update(Workshift lastWorkshift)
        {
            this.db.Workshifts.Update(lastWorkshift);
        }
    }
}
