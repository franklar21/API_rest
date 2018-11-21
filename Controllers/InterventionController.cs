using System.Collections.Generic;
using System.Linq;
using infofetcher.Models;
using Microsoft.AspNetCore.Mvc;
using System;
namespace infofetcher.Controllers
{
    [Route ("api/interventions")]
    [ApiController]
    public class InterventionController : ControllerBase 
    {
        private readonly frankContext _context;

        public InterventionController (frankContext context) {
            _context = context;
            
        }

        // get all interventions.
        [HttpGet]
        public ActionResult<List<Interventions>> GetAll () {
            return _context.Interventions.ToList ();
        }

        [HttpPut ("inprogress/{id}", Name = "PutInterventionStatus")]
        public string UpdateInprogress (long id) {

            var intervention = _context.Interventions.Find (id);
            if (intervention == null) {
                return "Enter a valid intervention id.";
            }
            if (intervention.Status != "Pending") {
                return "Invalid";
            } else {
                intervention.Status = "In Progress";
                DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
                _context.Interventions.Update(intervention);
                _context.SaveChanges ();

                return "OK";
                //return "The intervention #" + intervention.Id + " has changed Status from " + previous_Status + ", to " + Status + ".";
            }
        }

        [HttpPut ("completed/{id}", Name = "PutInterventionStatus1")]
        public string UpdateCompleted (long id) {

            var intervention = _context.Interventions.Find (id);
            if (intervention == null) {
                return "Enter a valid intervention id.";
            }
            if (intervention.Status != "In Progress") {
                return "Invalid";
            } else {
                intervention.Status = "Completed";
                DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
                _context.Interventions.Update(intervention);
                _context.SaveChanges ();

                return "HIHAA";
                //return "The intervention #" + intervention.Id + " has changed Status from " + previous_Status + ", to " + Status + ".";
            }
        }
    }
}