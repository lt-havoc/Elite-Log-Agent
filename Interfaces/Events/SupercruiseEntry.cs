using DW.ELA.Interfaces;
using Newtonsoft.Json;

namespace DW.ELA.Interfaces.Events;

public class SupercruiseEntry : JournalEvent
{
    [JsonProperty("StarSystem")]
    public required string StarSystem { get; set; }

    [JsonProperty("SystemAddress")]
    public long? SystemAddress { get; set; }
}