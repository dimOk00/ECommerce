using ECommerce.API.Search.Intefaces;
using ECommerce.API.Search.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.API.Search.Controllers
{
    [ApiController]
    [Route("api/search")]
    public class SearchController : ControllerBase
    {
        private ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpPost]
        public async Task<IActionResult> SearchAsync(ShearchTerm term)
        {
            var (isSuccess, SearchResults) = await _searchService.SearchAsync(term.CustomerId);
            if (isSuccess)
            {
                return Ok(SearchResults);
            }

            return NotFound();
        }
    }
}
