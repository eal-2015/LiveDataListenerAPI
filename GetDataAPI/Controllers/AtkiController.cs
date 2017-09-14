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
        public void Post([FromBody]JObject value)
        {
       
            StreamWriter sw = new StreamWriter("atkimeasurementlog.txt", true);
            try
            {
                TrafficMeasurement temp = new TrafficMeasurement();

                temp.datetime = (DateTime)value["data"]["datetime"];
                temp.lane = (int)value["data"]["lane"];
                temp.speed = (int)value["data"]["speed"];
                temp.length = (int)value["data"]["length"];
                temp.gap = (int)value["data"]["gap"];
                temp.@class = (int)value["data"]["class"];
                temp.display = (int)value["data"]["display"];
                temp.flash = (int)value["data"]["flash"];
                temp.stationid = (int)value["data"]["stationID"];


                sw.WriteLine(DateTime.Now + "Object: " + temp.datetime);
  
                sw.WriteLine(DateTime.Now + "Post called. Value is: " + (value == null ? "null" : "not null"));
                IMongoCollection<TrafficMeasurement> collection = conn.ConnectToMeasurement("Trafik_DB", "LiveMeasurements");
                sw.WriteLine(DateTime.Now + "DB called");

                collection.InsertOne(temp);

                sw.WriteLine(DateTime.Now + "value inserted");
            }
            catch (Exception ex)
            {
                sw.WriteLine(DateTime.Now + "Exception: " + ex.ToString());
            }
            finally
            {
                sw.Flush();
                sw.Close();
            }
        }
    }
}
