using DW.ELA.Interfaces;
using Newtonsoft.Json;

namespace DW.ELA.Interfaces.Events;

public class SaaSignalsFound : JournalEvent
{
    [JsonProperty("BodyName")]
    public required string BodyName { get; set; }

    [JsonProperty("SystemAddress")]
    public long? SystemAddress { get; set; }

    [JsonProperty("BodyID")]
    public long? BodyId { get; set; }

    [JsonProperty("Signals")]
    public required Signal[] Signals { get; set; }

    public class Signal
    {
        [JsonProperty("Type")]
        public required string Type { get; set; }

        [JsonProperty("Count")]
        public long Count { get; set; }

        [JsonProperty("Type_Localised", NullValueHandling = NullValueHandling.Ignore)]
        public string? TypeLocalised { get; set; }
    }
}