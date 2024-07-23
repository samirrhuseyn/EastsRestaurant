﻿using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstarct
{
    public interface IMoneyCaseActionDal : IGenericDal<MoneyCaseAction>
    {
        List<MoneyCaseAction> GetListByThisMonth();
    }
}
