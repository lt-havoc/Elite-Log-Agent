using DW.ELA.Interfaces;
using Newtonsoft.Json;

namespace DW.ELA.Interfaces.Events;

public class PowerplayDefect : JournalEvent
{
    [JsonProperty]
    public required string FromPower { get; set; }

    [JsonProperty]
    public required string ToPower { get; set; }
}