using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceCourse.Api.Orders.Db
{
    public class OrdersDbContext : DbContext
    {
        public DbSet<Order> orders { get; set; }
        public DbSet<OrderItem> orderItems { get; set; }

        public OrdersDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
