﻿using Newtonsoft.Json;

namespace DW.ELA.Interfaces.Events
{
    public class FactionEffect
    {
        [JsonProperty("Faction")]
        public string Faction { get; set; }

        [JsonProperty("Effects")]
        public FactionInfluenceEffect[] Effects { get; set; }

        [JsonProperty("Influence")]
        public Influence[] Influence { get; set; }

        [JsonProperty("Reputation")]
        public string Reputation { get; set; }

        [JsonProperty("ReputationTrend")]
        public string ReputationTrend { get; set; }
    }
}
