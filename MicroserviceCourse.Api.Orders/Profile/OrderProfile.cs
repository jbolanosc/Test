using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace MicroserviceCourse.Api.Orders.Profile
{
    public class OrderProfile : AutoMapper.Profile
    {
        public OrderProfile()
        {
            CreateMap<Db.Order, Models.Order>();
            CreateMap<Db.OrderItem, Models.OrderItem>();
        }
    }
}
