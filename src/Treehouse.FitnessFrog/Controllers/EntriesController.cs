using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Treehouse.FitnessFrog.Data;
using Treehouse.FitnessFrog.Models;

namespace Treehouse.FitnessFrog.Controllers
{
    public class EntriesController : Controller
    {
        private EntriesRepository _entriesRepository = null;

        public EntriesController() //the EntriesController constructor is instantiating an instance of the entriesRepository  class
        {
            _entriesRepository = new EntriesRepository();  // wich we'll using throught this project to manage our data 
        }

        public ActionResult Index()
        {
            List<Entry> entries = _entriesRepository.GetEntries(); // here were making a call to the repository to get the list of available entries

            // Calculate the total activity.
            /* then we are using link to calculate the total activity by filtering out in the entries whose exclude properties are set to True and summing in the duration property values*/
            double totalActivity = entries
                .Where(e => e.Exclude == false)
                .Sum(e => e.Duration);

            // Determine the number of days that have entries.
            /* in order to calculate the average daily activity we're getting the count of the disctinct entry values, which will later */
            int numberOfActiveDays = entries
                .Select(e => e.Date)
                .Distinct()
                .Count();

            ViewBag.TotalActivity = totalActivity;
            ViewBag.AverageDailyActivity = (totalActivity / (double)numberOfActiveDays);

            return View(entries);
        }

        public ActionResult Add()
        {
            return View();
        }
        [ HttpPost]
        public ActionResult Add( DateTime? date , int? activityId, double? duration , Entry.IntensityLevel? intensity , bool? exclude, string notes )
        {   
            /*
             DateTime dataValue ;
             DateTime.TryParse(date, out dateValue);
             */
            // string date = Request.Form["Date"];  this a method for retrieve the data post in the form 
            ViewBag.Date = date;
            ViewBag.ActivityId = activityId;
            ViewBag.Duration = duration;
            ViewBag.Intensity = intensity;
            ViewBag.Exclude = exclude;
            ViewBag.Notes = notes;


            return View();
        } 

        public ActionResult Edit(int? id) //The id parameter can have a value for nulle 
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View();
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View();
        }
    }
}