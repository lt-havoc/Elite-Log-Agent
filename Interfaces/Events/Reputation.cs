﻿using Newtonsoft.Json;

namespace DW.ELA.Interfaces.Events
{
    public class Reputation : JournalEvent
    {
        [JsonProperty("Empire")]
        public double Empire { get; set; }

        [JsonProperty("Federation")]
        public double Federation { get; set; }

        [JsonProperty("Alliance")]
        public double Alliance { get; set; }

        [JsonProperty("Independent")]
        public double? Independent { get; set; }
    }
}
