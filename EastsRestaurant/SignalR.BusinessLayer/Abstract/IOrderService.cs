﻿using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IOrderService : IGenericService<Order>
    {
        int TTotalOrderDal();
        int TActiveOrderCount();
        decimal TLastOrderPrice();
        decimal TTodayTotalEarning();
    }
}
