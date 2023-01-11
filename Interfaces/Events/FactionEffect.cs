using Newtonsoft.Json;

namespace DW.ELA.Interfaces.Events;

public class FactionEffect
{
    [JsonProperty("Faction")]
    public required string Faction { get; set; }

    [JsonProperty("Effects")]
    public required FactionInfluenceEffect[] Effects { get; set; }

    [JsonProperty("Influence")]
    public required Influence[] Influence { get; set; }

    [JsonProperty("Reputation")]
    public required string Reputation { get; set; }

    [JsonProperty("ReputationTrend")]
    public required string ReputationTrend { get; set; }
}