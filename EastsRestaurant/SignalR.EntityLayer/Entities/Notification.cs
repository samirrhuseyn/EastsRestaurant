﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities
{
    public class Notification
    {
        public int NotificationID { get; set; }
        public string NotificationDescription { get; set; }
        public string Date { get; set; }
        public bool Status { get; set; }
        public string NotificationTypeColor { get; set; }
        public string NotificationTypeIcon { get; set; }
    }
}
