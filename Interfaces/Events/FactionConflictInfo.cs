using Newtonsoft.Json;

namespace DW.ELA.Interfaces.Events;

public class FactionConflictInfo
{
    [JsonProperty]
    public required string Name { get; set; }

    [JsonProperty]
    public required string Stake { get; set; }

    [JsonProperty]
    public int? WonDays { get; set; }
}