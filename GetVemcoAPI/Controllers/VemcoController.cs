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
        public void Post([FromBody]VemcoMeasurement value)
        {
            StreamWriter sw = new StreamWriter("vemcoinsertlog.txt", true);
            try
            {
                sw.WriteLine("Post called. Value is: " + (value == null ? "null" : "not null"));
                IMongoCollection<VemcoMeasurement> collection = conn.ConnectToVemco("Trafik_DB", "LiveVemcoMeasurements");
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

        // POST api/values
        [HttpPost]
        [ActionName("station")]
        public void PostStation([FromBody]VemcoStation value)
        {
            StreamWriter sw = new StreamWriter("vemcostationlog.txt", true);
            try
            {
                sw.WriteLine("Post called. Value is: " + (value == null ? "null" : "not null"));
                IMongoCollection<VemcoStation> collection = conn.ConnectToVemcoStations("Trafik_DB", "VemcoStations");
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
