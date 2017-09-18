using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using ModelHelper;
using System.IO;
using System.Text;

namespace GetDataAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AtkiController : Controller
    {
        Mongo conn = new Mongo(); // connect to db

        // POST api/values
        [HttpPost]
        [ActionName("insert")]
        public void Post([FromBody]ATKIResponse value)
        {
            StringBuilder sb = new StringBuilder(); // for building log
            try
            {
                sb.AppendLine($"{DateTime.Now} - Post called. {(value == null ? "Data is null" : value.data.ToString())}"); // to log
                IMongoCollection<TrafficMeasurement> collection = conn.ConnectToMeasurement("Trafik_DB", "LiveMeasurements");
                sb.AppendLine($"{DateTime.Now} - DB called"); // to log

                collection.InsertOne(value.data);

                sb.AppendLine($"{DateTime.Now} - value inserted"); // to log
            }
            catch (Exception ex)
            {
                sb.AppendLine($"{DateTime.Now} - Exception: {ex.ToString()}"); // to log
            }
            finally
            {
                System.IO.File.AppendAllText("AtkiMeasurementLog.txt", sb.ToString()); // write log entries to file
            }
            // cleaning the log file
            var lines = System.IO.File.ReadAllLines("AtkiMeasurementLog.txt"); // get al lines in the log
            IEnumerable<string> newlines = null; // for holding the new log
            int linenumbers = lines.Count(); // count them
            int maxLines = 20000; // max line number

            if (linenumbers > maxLines) // if log is too big
            {
                newlines = lines.Skip(linenumbers - maxLines); // read all lines but skip the first X
            }
            if (newlines != null) // if we changed the log
            {
                System.IO.File.WriteAllLines("AtkiMeasurementLog.txt", newlines); // overwrite old log
            }
        }
    }
}
