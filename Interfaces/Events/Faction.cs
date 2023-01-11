using Newtonsoft.Json;

namespace DW.ELA.Interfaces.Events;

public class Faction
{
    [JsonProperty("Name")]
    public required string Name { get; set; }

    [JsonProperty("FactionState")]
    public required string FactionState { get; set; }

    [JsonProperty("Government")]
    public required string Government { get; set; }

    [JsonProperty("Influence")]
    public double? Influence { get; set; }

    [JsonProperty("Allegiance")]
    public required string Allegiance { get; set; }

    [JsonProperty("ActiveStates")]
    public FactionStateTrend[]? ActiveStates { get; set; }

    [JsonProperty("PendingStates")]
    public FactionStateTrend[]? PendingStates { get; set; }

    [JsonProperty("RecoveringStates")]
    public FactionStateTrend[]? RecoveringStates { get; set; }

    [JsonProperty("Happiness")]
    public required string Happiness { get; set; }

    [JsonProperty("Happiness_Localised")]
    public string? HappinessLocalised { get; set; }

    [JsonProperty("MyReputation")]
    public double? MyReputation { get; set; }
}