using DW.ELA.Interfaces;
using Newtonsoft.Json;

namespace DW.ELA.Interfaces.Events;

public class Shipyard : JournalEvent
{
    [JsonProperty("MarketID")]
    public long MarketId { get; set; }

    [JsonProperty("StationName")]
    public required string StationName { get; set; }

    [JsonProperty("StarSystem")]
    public required string StarSystem { get; set; }

    [JsonProperty("Horizons")]
    public bool? Horizons { get; set; }

    [JsonProperty("AllowCobraMkIV")]
    public bool? AllowCobraMkIv { get; set; }

    [JsonProperty("PriceList")]
    public ShipyardPrice[]? Prices { get; set; }
}