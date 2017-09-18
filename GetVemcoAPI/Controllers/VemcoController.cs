using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ModelHelper;
using MongoDB.Driver;
using System.IO;
using System.Text;

namespace GetVemcoAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    public class VemcoController : Controller
    {
        Mongo conn = new Mongo(); // connect to db

        // POST api/values
        [HttpPost]
        [ActionName("insert")]
        public void Post([FromBody]VemcoResponse value)
        {
            StringBuilder sb = new StringBuilder(); // for building log
            try
            {
                sb.AppendLine($"{DateTime.Now} - Post called. {(value == null ? "Data is null" : value.data.ToString())}"); // to log
                IMongoCollection<VemcoMeasurement> collection = conn.ConnectToVemco("Trafik_DB", "LiveVemcoMeasurements");
                sb.AppendLine($"{DateTime.Now} - DB connected"); // to log

                collection.InsertOne(value.data);

                sb.Append($" --- {DateTime.Now} - value inserted"); // adding to previous line.. more compact log
            }
            catch (Exception ex)
            {
                sb.AppendLine($"{DateTime.Now} - Exception: {ex.ToString()}"); // to log
            }
            finally
            {
                System.IO.File.AppendAllText("VemcoMeasurementLog.txt", sb.ToString()); // write log entries to file
            }
            // cleaning the log file
            var lines = System.IO.File.ReadAllLines("VemcoMeasurementLog.txt"); // get al lines in the log
            IEnumerable<string> newlines = null; // for holding the new log
            int linenumbers = lines.Count(); // count them
            int maxLines = 10000; // max line number

            if (linenumbers > maxLines) // if log is too big
            {
                newlines = lines.Skip(linenumbers - maxLines); // read all lines but skip the first X
            }
            if (newlines != null) // if we changed the log
            {
                System.IO.File.WriteAllLines("VemcoMeasurementLog.txt", newlines); // overwrite old log
            }
        }
    }
}
