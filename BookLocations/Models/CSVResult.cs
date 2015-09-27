using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookLocations.Models
{
    public class CSVResult
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public IEnumerable<Graph> Locations { get; set; }
        public string ErrorMessage { get; set; }

        public CSVResult()
        {
            Locations = new List<Graph>();
        }
    }
}