using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroserviceCourse.Api.Orders.Models
{
    public class OrderItem
    {
        public int id { get; set; }

        public int orderId { get; set; }

        public int productId { get; set; }

        public int quantity { get; set; }

        public decimal unitPrice { get; set; }
    }
}
