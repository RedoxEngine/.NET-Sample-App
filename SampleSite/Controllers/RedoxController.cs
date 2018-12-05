using SampleSite.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Http;

namespace SampleSite.Controllers
{
    public class RedoxController : ApiController
    {
        public const string AUTH_SECRET = "mydestinationsecret";
        private bool IsAuthorized()
        {
            IEnumerable<string> headerValues;
            return (Request.Headers.TryGetValues("verification-token", out headerValues) &&
                headerValues.FirstOrDefault() == AUTH_SECRET);
        }

        [Route("api/redox")]
        [HttpPost]
        public IHttpActionResult Verify([FromBody] RedoxApi.Models.VerifyDestinationBody verification)
        {
            if (verification.verification_token == AUTH_SECRET)
            {
                // reply with the challenge value to verify with Redox
                return Ok(verification.challenge);
            }
            return BadRequest();
        }

        // Only support new patient for now. Other datamodels/event types should 404.
        [Route("api/redox")]
        [HttpPost]
        public IHttpActionResult PatientAdmin_NewPatient([FromBody] RedoxApi.Models.Patientadmin.Newpatient.Newpatient patient)
        {
            // Make sure this request comes from Redox
            if (!IsAuthorized())
            {
                return BadRequest();
            }

            // Save the new patient
            try
            {
                PatientService.SaveRedoxPatient(patient);
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                return BadRequest("Validation failed");
            }
            return Ok();
        }

        // This one has the same route but gets called for a different event type!!
        [Route("api/redox")]
        [HttpPost]
        public IHttpActionResult PatientAdmin_PatientUpdate([FromBody] RedoxApi.Models.Patientadmin.Patientupdate.Patientupdate patient)
        {
            // Make sure this request comes from Redox
            if (!IsAuthorized())
            {
                return BadRequest();
            }

            // Update the new patient (no function for this yet).

            return Ok();
        }
    }
}