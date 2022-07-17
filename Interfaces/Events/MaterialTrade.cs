using Newtonsoft.Json;

namespace DW.ELA.Interfaces.Events;

public class MaterialTrade : JournalEvent
{
    [JsonProperty("MarketID")]
    public long MarketId { get; set; }

    [JsonProperty("TraderType")]
    public required string TraderType { get; set; }

    [JsonProperty("Paid")]
    public required MaterialDealLeg Paid { get; set; }

    [JsonProperty("Received")]
    public required MaterialDealLeg Received { get; set; }
}