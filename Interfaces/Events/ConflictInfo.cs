using Newtonsoft.Json;

namespace DW.ELA.Interfaces.Events;

public class ConflictInfo
{
    [JsonProperty]
    public required string WarType { get; set; }

    [JsonProperty]
    public required string Status { get; set; }

    [JsonProperty]
    public required FactionConflictInfo Faction1 { get; set; }

    [JsonProperty]
    public required FactionConflictInfo Faction2 { get; set; }
}