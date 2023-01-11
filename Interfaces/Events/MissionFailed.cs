using DW.ELA.Interfaces;
using Newtonsoft.Json;

namespace DW.ELA.Interfaces.Events;

public class MissionFailed : JournalEvent
{
    [JsonProperty("Name")]
    public required string Name { get; set; }

    [JsonProperty("MissionID")]
    public long MissionId { get; set; }
}