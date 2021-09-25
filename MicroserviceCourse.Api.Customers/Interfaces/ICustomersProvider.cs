using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroserviceCourse.Api.Customers.Models;

namespace MicroserviceCourse.Api.Customers.Interfaces
{
    public interface ICustomersProvider
    {
        Task<(bool isSuccess, IEnumerable<Customer> customers, string errorMessage)> GetCustomersAsync();
        Task<(bool isSuccess, Customer customer, string errorMessage)> GetCustomerAsync(int id);
    }
}
