using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroserviceCourse.Api.Customers.Interfaces;
using MicroserviceCourse.Api.Customers.Models;
using MicroserviceCourse.Api.Customers.Db;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceCourse.Api.Customers.Providers
{
    public class CustomersProvider : ICustomersProvider
    {
        private readonly CustomerDbContext _context;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public CustomersProvider(CustomerDbContext context, ILogger<CustomersProvider> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
            seedData();
        }
        public async Task<(bool isSuccess, Models.Customer customer, string errorMessage)> GetCustomerAsync(int id)
        {
            try
            {
                var customer = await _context.Customers.FindAsync(id);

                if (customer != null)
                {
                    var result = _mapper.Map<Db.Customer, Models.Customer>(customer);
                    return (true, result, "");
                }

                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, null, ex.ToString());
            }
        }

        public async Task<(bool isSuccess, IEnumerable<Models.Customer> customers, string errorMessage)> GetCustomersAsync()
        {
            try
            {
                var customers = await _context.Customers.ToListAsync();

                if (customers != null || customers.Any())
                {
                    var result = _mapper.Map<IEnumerable<Db.Customer>, IEnumerable<Models.Customer>>(customers);
                    return (true, result, "");
                }

                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, null, ex.ToString());
            }
        }


        public void seedData()
        {
            if (!_context.Customers.Any())
            {
                _context.Add(new Db.Customer() { id = 1, fName = "Josue", lName = "Bolanos", email = "123@gmail.com", address = "123" });
                _context.Add(new Db.Customer() { id = 2, fName = "Daniel", lName = "Bolanos", email = "123@gmail.com", address = "123" });
                _context.Add(new Db.Customer() { id = 3, fName = "John", lName = "Doe", email = "123@gmail.com", address = "123" });
                _context.SaveChanges();
            }
        }
    }
}
