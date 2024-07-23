using Microsoft.EntityFrameworkCore;
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
    public class EfNotificationDal : GenericRepository<Notification>, INotificationDal
    {
        public EfNotificationDal(SignalRContext context) : base(context)
        {
        }

       

        public List<Notification> GetAllNotificationsByFalseWIthTake()
        {
            using var context = new SignalRContext();
            return context.Notifications.Where(x => x.Status == false).OrderByDescending(x => x.Date).Take(3).ToList();
        }

        public List<Notification> GetNotificationAllList()
        {
            using var context = new SignalRContext();
            return context.Notifications.OrderByDescending(x => x.Date).ToList();
        }

        public int NotificationCountByStatusFalse()
        {
            using var context = new SignalRContext();
            var value = context.Notifications.Where(x => x.Status == false).Count();
            return value;
        }
    }
}
