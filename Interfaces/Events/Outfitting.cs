using Newtonsoft.Json;

namespace DW.ELA.Interfaces.Events;

public class Outfitting : JournalEvent
{
    [JsonProperty("MarketID")]
    public long MarketId { get; set; }

    [JsonProperty("StationName")]
    public required string StationName { get; set; }

    [JsonProperty("StarSystem")]
    public required string StarSystem { get; set; }

    [JsonProperty("Horizons")]
    public bool? Horizons { get; set; }

    [JsonProperty("Items")]
    public required OutfittingItem[] Items { get; set; }
}