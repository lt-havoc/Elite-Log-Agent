using Newtonsoft.Json;

namespace DW.ELA.Interfaces.Events;

public class MaterialsReward
{
    [JsonProperty("Name")]
    public required string Name { get; set; }

    [JsonProperty("Name_Localised")]
    public string? NameLocalised { get; set; }

    [JsonProperty("Category")]
    public required string Category { get; set; }

    [JsonProperty("Category_Localised")]
    public string? CategoryLocalised { get; set; }

    [JsonProperty("Count")]
    public long Count { get; set; }
}