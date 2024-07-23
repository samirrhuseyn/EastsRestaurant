using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoLayer.OrderDto
{
    public class CreateOrderDto
    {
        public int MenuTableID { get; set; }
        public DateTime Date { get; set; }
    }
}
