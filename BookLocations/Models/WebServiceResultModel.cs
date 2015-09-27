using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookLocations.Models
{
    

        public class Graph
        {
            public string id { get; set; }
            public object type { get; set; }
            public object name { get; set; }
            //public object birthDate { get; set; }
            //public object deathDate { get; set; }
            //public string inDataset { get; set; }
            //public object about { get; set; }
            //public List<object> alternateName { get; set; }
            //public List<string> author { get; set; }
            //public List<string> contributor { get; set; }
            //public List<string> creator { get; set; }
            //public List<object> description { get; set; }
            public object genre { get; set; }
            //public List<string> workExample { get; set; }
        }

        public class WorkExample
        {
            public string id { get; set; }
            public string type { get; set; }
        }

        public class Creator
        {
            public string id { get; set; }
            public string type { get; set; }
        }

        public class Contributor
        {
            public string id { get; set; }
            public string type { get; set; }
        }

        public class About
        {
            public string id { get; set; }
            public string type { get; set; }
        }

        public class Author
        {
            public string id { get; set; }
            public string type { get; set; }
        }

        public class InDataset
        {
            public string id { get; set; }
            public string type { get; set; }
        }

        public class Context
        {
            public string birthDate { get; set; }
            public string name { get; set; }
            public string deathDate { get; set; }
            public WorkExample workExample { get; set; }
            public Creator creator { get; set; }
            public string alternateName { get; set; }
            public string genre { get; set; }
            public Contributor contributor { get; set; }
            public About about { get; set; }
            public string description { get; set; }
            public Author author { get; set; }
            public InDataset inDataset { get; set; }
            public string schema { get; set; }
            public string library { get; set; }
            public string rdf { get; set; }
            public string pto { get; set; }
            public string dcte { get; set; }
        }

    public class WebServiceResultModel
    {
        public List<Graph> graph { get; set; }
        public Context context { get; set; }
    }

    
}