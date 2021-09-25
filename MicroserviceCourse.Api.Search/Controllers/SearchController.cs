using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroserviceCourse.Api.Search.Interfaces;
using MicroserviceCourse.Api.Search.Models;

namespace MicroserviceCourse.Api.Search.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;
        public SearchController(ISearchService service)
        {
            _searchService = service;
        }

        [HttpPost]
        public async Task<ActionResult> searchAsync(SearchTerm search)
        {
            var result = await _searchService.SearchAsync(search.customerId);

            if (result.isSuccess)
            {
                return Ok(result.searchResults);
            }

            return NotFound();
        }
    }
}
