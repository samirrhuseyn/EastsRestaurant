using DataAccessLayer.Abstarct;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repositories;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfExpenseDal : GenericRepository<Expense>, IExpenseDal
    {
        public EfExpenseDal(SignalRContext context) : base(context)
        {
        }
    }
}
