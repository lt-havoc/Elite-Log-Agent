using Newtonsoft.Json;

namespace DW.ELA.Interfaces.Events;

public class Market : JournalEvent
{
    [JsonProperty("MarketID")]
    public long MarketId { get; set; }

    [JsonProperty("StationName")]
    public required string StationName { get; set; }

    [JsonProperty("StarSystem")]
    public required string StarSystem { get; set; }

    [JsonProperty("Items")]
    public MarketItem[]? Items { get; set; }
}