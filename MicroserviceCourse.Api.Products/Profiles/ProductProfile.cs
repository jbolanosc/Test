using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace MicroserviceCourse.Api.Products.Profiles
{
    public class ProductProfile : AutoMapper.Profile
    {
       public ProductProfile()
        {
            CreateMap<DB.Product, Models.Product>();
        }
    }
}
