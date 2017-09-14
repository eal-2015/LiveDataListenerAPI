using System;
using MongoDB.Bson.Serialization.Attributes;

namespace ModelHelper
{
    [BsonIgnoreExtraElements]
    public class VemcoStation
    {
        public int sensorId { get; set; }
        public string ip { get; set; }
        public string mac_id { get; set; }
        public int is_entrance { get; set; }
        public string direction { get; set; }
        public int? shop_id { get; set; }
        public int? custom_shop_id { get; set; }
        public string name { get; set; }
        public string lon { get; set; }
        public string lat { get; set; }
        public string status { get; set; }
        public string source { get; set; }
        public string type { get; set; }
        public string version { get; set; }
    }
}
