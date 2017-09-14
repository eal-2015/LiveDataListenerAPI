using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ModelHelper;
using MongoDB.Driver;
using System.IO;


namespace GetVemcoAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    public class VemcoController : Controller
    {
        Mongo conn = new Mongo();

        // POST api/values
        [HttpPost]
        [ActionName("insert")]
        public void Post([FromBody]VemcoResponse value)
        {
            using (StreamWriter sw = new StreamWriter("VemcoMeasurementLog.txt", true))
            {
                try
                {
                    sw.WriteLine(DateTime.Now + " - Post called. " + (value == null ? "null" : value.data.ToString()));
                    IMongoCollection<VemcoMeasurement> collection = conn.ConnectToVemco("Trafik_DB", "LiveVemcoMeasurements");
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
