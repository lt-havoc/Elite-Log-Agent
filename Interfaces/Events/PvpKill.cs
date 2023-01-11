using DW.ELA.Interfaces;
using Newtonsoft.Json;

namespace DW.ELA.Interfaces.Events;

public class PvpKill : JournalEvent
{
    [JsonProperty("Victim")]
    public required string Victim { get; set; }

    [JsonProperty("CombatRank")]
    public long CombatRank { get; set; }
}