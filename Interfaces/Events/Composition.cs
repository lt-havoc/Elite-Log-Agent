using Newtonsoft.Json;

namespace DW.ELA.Interfaces.Events;

public class Composition
{
    [JsonProperty("Name")]
    public required string Name { get; set; }

    [JsonProperty("Percent")]
    public double Percent { get; set; }
}