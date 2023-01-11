using DW.ELA.Interfaces;
using Newtonsoft.Json;

namespace DW.ELA.Interfaces.Events;

public partial class Materials : JournalEvent
{
    [JsonProperty("Raw")]
    public required Material[] RawMats { get; set; }

    [JsonProperty("Manufactured")]
    public required Material[] Manufactured { get; set; }

    [JsonProperty("Encoded")]
    public required Material[] Encoded { get; set; }
}