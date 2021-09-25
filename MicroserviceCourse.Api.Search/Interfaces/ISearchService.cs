using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroserviceCourse.Api.Search.Interfaces
{
    public interface ISearchService
    {
        Task<(bool isSuccess, dynamic searchResults)> SearchAsync(int customerId);
    }
}
