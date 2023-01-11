using Newtonsoft.Json;

namespace DW.ELA.Interfaces.Events;

public class TopTier
{
    [JsonProperty("Name")]
    public required string Name { get; set; }

    [JsonProperty("Bonus")]
    public required string Bonus { get; set; }
}