using Newtonsoft.Json;

namespace DW.ELA.Interfaces.Events;

public class Influence
{
    [JsonProperty("SystemAddress")]
    public long SystemAddress { get; set; }

    [JsonProperty("Trend")]
    public required string Trend { get; set; }

    [JsonProperty("Influence")]
    public required string InfluenceValue { get; set; }
}