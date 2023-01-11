using DW.ELA.Interfaces;
using Newtonsoft.Json;

namespace DW.ELA.Interfaces.Events;

public class Commander : JournalEvent
{
    [JsonProperty("Name")]
    public required string Name { get; set; }

    [JsonProperty("FID")]
    public required string FrontierId { get; set; }
}