// X00122026
// Graham Lalor

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CA1.Models;

namespace CA1.Controllers
{
    [RoutePrefix("BPressure")]
    public class PatientController : ApiController
    {
        static private List<Patient> patients = new List<Patient>()
        {
            new Patient{ID = 1, readings = new List<BloodPressure>(){ new BloodPressure { Systolic = 100, Diastolic = 80 }, new BloodPressure { Systolic = 200, Diastolic = 110 }, new BloodPressure { Systolic = 80, Diastolic = 50 }}},
            new Patient{ID = 2, readings = new List<BloodPressure>(){ new BloodPressure { Systolic = 120, Diastolic = 85 }, new BloodPressure { Systolic = 125, Diastolic = 99 }, new BloodPressure { Systolic = 110, Diastolic = 85 }}},
            new Patient{ID = 3, readings = new List<BloodPressure>(){ new BloodPressure { Systolic = 145, Diastolic = 80 }, new BloodPressure { Systolic = 1460, Diastolic = 99 }, new BloodPressure { Systolic = 80, Diastolic = 50 }}}
        };

        [Route("bloodPressure/{systolic:max(200)}/{diastolic:max(200)}")]
        [HttpGet]
        public IHttpActionResult GetBloodPressure(int systolic, int diastolic)
        {
            if (ModelState.IsValid)
            {
                BloodPressure bp = new BloodPressure { Systolic = systolic, Diastolic = diastolic };
                return Ok(bp.calculateCategory()); // Returning catagory
            }
            else
            {
                return NotFound();
            }
        }

        [Route("bloodPressure/id/{id:int}")]
        [HttpGet]
        public IHttpActionResult GetBloodPressureById(int id)
        {
            if (ModelState.IsValid)
            {
                var patient = patients.FirstOrDefault(p => p.ID == id);
                if(patient != null)             // Test if patient was found
                {
                    return Ok(patient.readings);
                }
                return BadRequest();
            }
            else
            {
                return NotFound();
            }
        }

        [Route("bloodPressure/avgBP/id/{id:int}")]
        [HttpGet]
        public IHttpActionResult GetAvgBloodPressureById(int id)
        {
            if (ModelState.IsValid)
            {
                var patient = patients.FirstOrDefault(p => p.ID == id);
                if (patient != null)             // Test if patient was found
                {
                    int sumSystolic = 0;
                    int sumDiastolic = 0;
                    BloodPressure bp;
                    foreach (var i in patient.readings)
                    {
                        sumSystolic += i.Systolic;
                        sumDiastolic += i.Diastolic;
                    }
                    sumSystolic = sumSystolic / patient.readings.Count;         // Calculate average blood systolic
                    sumDiastolic = sumDiastolic / patient.readings.Count;       // Calculate average blood diastolic
                    bp = new BloodPressure { Systolic = sumSystolic, Diastolic = sumDiastolic };
                    return Ok(bp);
                }
                return BadRequest();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
