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
            var entry = new Entry()
            {
                Date = DateTime.Today,
                //ActivityId = 2,
            };
            ViewBag.ActivitiesSelectListItems = new SelectList(
                                                                Data.Data.Activities, "Id", "Name");

            return View(entry);
        }
        [HttpPost]
        public ActionResult Add(Entry entry)
        {
            // If there aren't any "Duration" field validation errors
            // then make sure that the duration is greater than "0".
            if (ModelState.IsValidField("Duration") && entry.Duration <= 0)
            {
                ModelState.AddModelError("Duration",
                    "The Duration field value must be greater than '0'.");
            }

            if (ModelState.IsValid)
            {
                _entriesRepository.AddEntry(entry);

                // TODO Display the Entries list page
                return RedirectToAction("index");
            }
            //entry.ActivityId = 2;  /*this reprsent model property*/
            ViewBag.ActivitiesSelectListItems = new SelectList(
                                                                Data.Data.Activities, "Id", "Name");
            return View(entry); //  but the value of AcivityId in model state = 1 ( this return to choise of the user ) 
        }
        

        //[ActionName("Add"),HttpPost]
        //public ActionResult AddPost( DateTime? date , int? activityId , double? duration , Entry.IntensityLevel? intensity , bool? exclude , string notes)
        //{
        //    return View();
        //    //string date = Request.Form["date"];
        //    /*
             
        //     DateTime dateValue;
        //    DateTime.TryParse(date, out dateValue);

        //    Int32 activityIdValue;
        //    Int32.TryParse( activityId , out activityIdValue);

        //    Int32 durationValue;
        //    Int32.TryParse(duration, out durationValue);

        //    Boolean excludeValue;
        //    Boolean.TryParse( exclude , out excludeValue );

        //    ViewBag.Date = dateValue;
        //    ViewBag.ActivityId = activityIdValue;
        //    ViewBag.Duration = durationValue;
        //    ViewBag.Intensity = intensity;
        //    ViewBag.Exclude = excludeValue;
        //    ViewBag.Notes = notes;  
        //                                         */

        //    /* ViewBag.Date = ModelState["date"].Value.AttemptedValue;
        //     ViewBag.ActivityId = ModelState["activityId"].Value.AttemptedValue;
        //     ViewBag.Duration = ModelState["duration"].Value.AttemptedValue;
        //     ViewBag.Intensity = ModelState["intensity"].Value.AttemptedValue;
        //     ViewBag.Exclude = ModelState["exclude"].Value.AttemptedValue;
        //     ViewBag.Notes = ModelState["notes"].Value.AttemptedValue; */
              
        //}  
        /*
        [ HttpPost]
        public ActionResult Add( DateTime? date , int? activityId, double? duration , Entry.IntensityLevel? intensity , bool? exclude, string notes )
        {   
            
             //DateTime dataValue ;
             //DateTime.TryParse(date, out dateValue);

             // string date = Request.Form["Date"];  this a method for retrieve the data post in the form 

            ViewBag.Date = date;
            ViewBag.ActivityId = activityId;
            ViewBag.Duration = duration;
            ViewBag.Intensity = intensity;
            ViewBag.Exclude = exclude;
            ViewBag.Notes = notes;


            return View();
        } */

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