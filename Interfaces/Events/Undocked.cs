using Newtonsoft.Json;

namespace DW.ELA.Interfaces.Events;

public class Undocked : JournalEvent
{
    [JsonProperty("StationName")]
    public required string StationName { get; set; }

    [JsonProperty("StationType")]
    public required string StationType { get; set; }

    [JsonProperty("MarketID")]
    public long MarketId { get; set; }
}