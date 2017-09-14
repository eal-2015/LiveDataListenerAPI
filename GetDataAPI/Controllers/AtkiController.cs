using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using ModelHelper;
using System.IO;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json.Linq;
using MongoDB.Bson.Serialization;


namespace GetDataAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AtkiController : Controller
    {
        Mongo conn = new Mongo();

        // POST api/values
        [HttpPost]
        [ActionName("insert")]
        public void Post([FromBody]ATKIResponse value)
        {
            using (StreamWriter sw = new StreamWriter("atkimeasurementlog.txt", true))
            {
                try
                {
                    sw.WriteLine(DateTime.Now + " - Post called. " + (value == null ? "null" : value.data.ToString()));
                    IMongoCollection<TrafficMeasurement> collection = conn.ConnectToMeasurement("Trafik_DB", "LiveMeasurements");
                    sw.WriteLine(DateTime.Now + " - DB called");

                    collection.InsertOne(value.data);

                    sw.WriteLine(DateTime.Now + " - value inserted");
                }
                catch (Exception ex)
                {
                    sw.WriteLine(DateTime.Now + " - Exception: " + ex.ToString());
                }
            }

        }
    }
}
