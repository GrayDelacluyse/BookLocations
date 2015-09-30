using BookLocations.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace BookLocations.Helpers
{
    public class OclcHelper
    {
        public static IEnumerable<Graph> GetLocations(string Author, string Title)
        {
            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;
            string oclcSearchResult = client.DownloadString("https://www.worldcat.org/search?q=ti%3A" + Title + "+au%3A" + Author + "&qt=advanced&dblist=638&fq=x0%3Abook");


            //Get the OCLC number from the search result
            //oclc_number">1859740</d
            int oclcNumberStart = oclcSearchResult.IndexOf("oclc_number");
            if (oclcNumberStart == -1)
            {
                //Failed
                throw new Exception("Failed to find OCLC Number from search result");
                
            }
            oclcNumberStart += 13;
            int oclcNumberEnd = oclcSearchResult.IndexOf("<", oclcNumberStart);
            if (oclcNumberEnd == -1 || oclcNumberEnd <= oclcNumberStart)
            {
                throw new Exception("Failed to find OCLC Number End from search result");
            }
            int oclcNumberLength = oclcNumberEnd - oclcNumberStart;

            string oclcNumber = oclcSearchResult.Substring(oclcNumberStart, oclcNumberLength);

            //Go to Details Page
            //http://www.worldcat.org/oclc/1859740
            client.Headers.Add(HttpRequestHeader.Cookie, "owcLocRedirectPersistent=_nr.selected; owcLocRedirectSession=_nr.selected");
            string oclcDetailPageResult = client.DownloadString("http://www.worldcat.org/oclc/" + oclcNumber);

            //Get the webservice ID from the detail page
            //http://experiment.worldcat.org/entity/work/data/196883763#CreativeWork/eighteen_seventy_six" p
            string webserviceIdStartString = "http://experiment.worldcat.org/entity/work/data/";
            int webserviceIdStart = oclcDetailPageResult.IndexOf(webserviceIdStartString);
            if (webserviceIdStart == -1)
            {
                throw new Exception("Failed to find webservice ID from details page");
            }
            webserviceIdStart += webserviceIdStartString.Length;

            int webserviceIdEnd = oclcDetailPageResult.IndexOf("#", webserviceIdStart);
            if (webserviceIdEnd == -1 || webserviceIdEnd <= webserviceIdStart)
            {
                throw new Exception("Failed to find webservice ID End from details page");
            }
            int webserviceIdLength = webserviceIdEnd - webserviceIdStart;

            string webserviceId = oclcDetailPageResult.Substring(webserviceIdStart, webserviceIdLength);

            //Get the JSON details from the OCLC webservice
            //Get Linked Data Example
            //http://experiment.worldcat.org/entity/work/data/1859740.jsonld

            string webserviceResults = client.DownloadString("http://experiment.worldcat.org/entity/work/data/" + webserviceId + ".jsonld");
            webserviceResults = webserviceResults.Replace("@", "");

            WebServiceResultModel WSRM = JsonConvert.DeserializeObject<WebServiceResultModel>(webserviceResults);
            var places = WSRM.graph.Where(x => (x.type is string) && (string)x.type == "schema:Place");
            //TODO: Handle city names in foreign location

            return places.ToList();
        }
    }
}