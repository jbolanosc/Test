using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroserviceCourse.Api.Orders.Db
{
    public class Order
    {
        public int id { get; set; }

        public int customerId { get; set; }

        public DateTime orderDate { get; set; }

        public OrderItem[] items { get; set; }

        public decimal total { get; set; }
    }
}
