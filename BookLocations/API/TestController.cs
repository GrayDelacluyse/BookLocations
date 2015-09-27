using BookLocations.Helpers;
using BookLocations.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookLocations.API
{
    public class TestController : ApiController
    {
        // GET: api/locations
        [Route("api/locations/{Author}/{Title}")]
        public IEnumerable<Graph> Get(string Author, string Title)
        {
            var places = OclcHelper.GetLocations(Author, Title);

            return places;
        }
    }
}
