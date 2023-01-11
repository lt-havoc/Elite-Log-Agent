using DW.ELA.Interfaces;
using Newtonsoft.Json;

namespace DW.ELA.Interfaces.Events;

public class MaterialCollected : JournalEvent
{
    [JsonProperty("Category")]
    public required string Category { get; set; }

    [JsonProperty("Name")]
    public required string Name { get; set; }

    [JsonProperty("Name_Localised")]
    public string? NameLocalised { get; set; }

    [JsonProperty("Count")]
    public long Count { get; set; }
}