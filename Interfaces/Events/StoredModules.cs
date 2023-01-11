using DW.ELA.Interfaces;
using Newtonsoft.Json;

namespace DW.ELA.Interfaces.Events;

public class StoredModules : JournalEvent
{
    [JsonProperty("MarketID")]
    public long MarketId { get; set; }

    [JsonProperty("StationName")]
    public required string StationName { get; set; }

    [JsonProperty("StarSystem")]
    public required string StarSystem { get; set; }

    [JsonProperty("Items")]
    public required StoredItem[] Items { get; set; }
}