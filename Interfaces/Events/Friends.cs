using DW.ELA.Interfaces;
using Newtonsoft.Json;

namespace DW.ELA.Interfaces.Events;

public class Friends : JournalEvent
{
    [JsonProperty]
    public required string Status { get; set; }

    [JsonProperty]
    public required string Name { get; set; }
}