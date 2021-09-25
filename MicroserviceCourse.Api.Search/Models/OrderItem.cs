using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroserviceCourse.Api.Search.Models
{ 
    public class OrderItem
    {
        public int id { get; set; }

        public int orderId { get; set; }

        public int productId { get; set; }

        public string productName { get; set; }

        public int quantity { get; set; }

        public decimal unitPrice { get; set; }
    }
}
