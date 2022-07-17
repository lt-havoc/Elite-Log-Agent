using Newtonsoft.Json;

namespace DW.ELA.Interfaces.Events;

public class StoredShips : JournalEvent
{
    [JsonProperty("StationName")]
    public required string StationName { get; set; }

    [JsonProperty("MarketID")]
    public long MarketId { get; set; }

    [JsonProperty("StarSystem")]
    public required string StarSystem { get; set; }

    [JsonProperty("ShipsHere")]
    public required ShipStoredLocal[] ShipsHere { get; set; }

    [JsonProperty("ShipsRemote")]
    public required ShipStoredRemote[] ShipsRemote { get; set; }
}