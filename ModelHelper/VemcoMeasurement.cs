using System;
using MongoDB.Bson.Serialization.Attributes;

namespace ModelHelper
{
    [BsonIgnoreExtraElements]
    public class VemcoMeasurement
    {
        public int sensorId { get; set; }
        public DateTime fromTstmp { get; set; }
        public DateTime toTstmp { get; set; }
        public int @in { get; set; }
        public int @out { get; set; }
    }
}
