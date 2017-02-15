using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoomTemperatureTracker
{
    public class Temperature
    {
        [JsonProperty(PropertyName ="id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "temperaturevalue")]
        public string TemperatureValue { get; set; }

        [JsonProperty(PropertyName = "humidityvalue")]
        public string HumidityValue { get; set; }

    }
}
