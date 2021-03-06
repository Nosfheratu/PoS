﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PoS.Entities;
using PoS.Data.Repositories;

namespace PoS.Services
{
    public class WorkshiftsService
    {
        UsersService usersService;
        WorkshiftsRepository workshiftRepository;

        public WorkshiftsService()
        {
            workshiftRepository = new WorkshiftsRepository();
            usersService = new UsersService();
        }

        public void CreateWorkshift(User user, decimal amount)
        {
            Workshift workshift = new Workshift();
            workshift.CashAmount = amount;
            workshift.Opening = workshift.Closing = DateTime.Now;
            workshift.UserId = user.Id;

            workshiftRepository.Add(workshift);
        }

        public bool OpenedTill()
        {
            var workshift = GetLastWorkshift();
            
            if (workshift == null) return false;

            return workshift.Opened;
        }

        public Workshift GetLastWorkshift()
        {
            return workshiftRepository.GetAll().LastOrDefault();
        }
        
        public void CloseLastWorkshift()
        {
            var lastWorkshift = workshiftRepository.GetAll().LastOrDefault();
            lastWorkshift.CloseTill();

            workshiftRepository.Update(lastWorkshift);
        }
    }
}
