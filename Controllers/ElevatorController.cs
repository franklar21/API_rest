using System.Collections.Generic;
using System.Linq;
using infofetcher.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace infofetcher.Controllers {
    [Route ("api/elevators")]
    [ApiController]
    public class ElevatorController : ControllerBase {
        private readonly frankContext _context;

        public ElevatorController (frankContext context) {
            _context = context;

        }

        // get all elevators.
        [HttpGet]
        public ActionResult<List<Elevators>> GetAll () {
            return _context.Elevators.ToList ();
        }

        // Get the current status of the specified elevator
        [HttpGet ("{id}", Name = "GetElevators")]
        public string GetById (long id) {
            var item = _context.Elevators.Find (id);
            var _Status = item.Status;
            var _Id =  _context.Elevators.Find (id).Id;
            if (item == null) {
                return "Please enter a valid id.";
            }
            return "The elevator #" + _Id + " is currently " + _Status + ".";
        }

        // Get all elevators that are not currently active.
        [HttpGet ("Status", Name = "GetNotActiveElevators")]
        public ActionResult<List<Elevators>> Get (string Status) {
            var _result = _context.Elevators.Where(s=>s.Status!="Active");
            return _result.ToList();
        }
        
        // Change the Status for a specified elevatos
        [HttpPut ("{id}", Name = "PutElevatorStatus")]
        public string Update (long id, [FromBody] JObject body) {

            var elevator = _context.Elevators.Find (id);
            if (elevator == null) {
                return "Enter a valid elevator id.";
            }

            var previous_Status = elevator.Status;
            var Status = (string) body.SelectToken ("Status");
            if (Status == "Active" || Status == "Inactive" || Status == "Alarm" || Status == "Intervention") {
                elevator.Status = Status;
                _context.Elevators.Update (elevator);
                _context.SaveChanges ();
                return "The elevator #" + elevator.Id + " has changed Status from " + previous_Status + ", to " + Status + ".";
            } else {
                return "Invalid tatus: Must be Active, Inactive, Alarm or Intervention";
            }
        }
    }
}