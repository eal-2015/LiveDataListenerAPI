using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using ModelHelper;
using System.IO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GetDataAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AtkiController : Controller
    {
        Mongo conn = new Mongo();

        // POST api/values
        [HttpPost]
        [ActionName("insert")]
        public void Post([FromBody]TrafficMeasurement value)
        {
            StreamWriter sw = new StreamWriter("atkimeasurementlog.txt", true);
            try
            {
                sw.WriteLine("Post called. Value is: " + (value == null ? "null" : "not null"));
                IMongoCollection<TrafficMeasurement> collection = conn.ConnectToMeasurement("Trafik_DB", "LiveMeasurements");
                sw.WriteLine("DB called");
                collection.InsertOne(value);
                sw.WriteLine("value inserted");
            }
            catch (Exception ex)
            {
                sw.WriteLine("Exception: " + ex.ToString());
            }
            finally
            {
                sw.Flush();
                sw.Close();
            }
        }
    }
}
