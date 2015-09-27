using BookLocations.Helpers;
using BookLocations.Models;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BookLocations.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {

            HomeAboutModel myHomeAboutModel = new HomeAboutModel();
            myHomeAboutModel.Message = "Your application descript page from a model!";


            return View(myHomeAboutModel);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            ViewBag.OurStuffHere = "asdf"; 

            return View();
        }

        public ActionResult  BookLocation(string Author, string Title)
        {
            var places = OclcHelper.GetLocations(Author,Title);

            return View(places);
        }

        public ActionResult CSV()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CSV(HttpPostedFileBase file)
        {
            List<CSVResult> Results = new List<CSVResult>();

            // Verify that the user selected a file
            if (file != null && file.ContentLength > 0)
            {

                using (TextFieldParser parser = new TextFieldParser(file.InputStream))
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(",");
                    parser.HasFieldsEnclosedInQuotes = true;
                    
                    //Skip first line
                    parser.ReadLine();

                    while (!parser.EndOfData)
                    {
                        //Column 5: Title
                        //Column 6: Author

                        CSVResult currentResult = new CSVResult();

                        string[] fields = parser.ReadFields();
                        currentResult.Title = fields[4];
                        currentResult.Author = fields[5];

                        Results.Add(currentResult);

                        if (Results.Count > 150) break;
                    }
                }

                //Do parallels awesomeness here
                Parallel.ForEach(Results, r =>
                {
                    try
                    {
                        r.Locations = OclcHelper.GetLocations(r.Author, r.Title);
                    }
                    catch (Exception ie)
                    {
                        //Handle the errors
                        r.ErrorMessage = ie.ToString();
                    }
                });





                return View(Results);
            }

            // redirect back to the index action to show the form once again
            ViewBag.Message = "Could not read file";
            return RedirectToAction("CSV");
        }
    }
}