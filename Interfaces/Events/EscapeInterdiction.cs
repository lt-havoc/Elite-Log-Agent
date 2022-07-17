using DW.ELA.Interfaces;
using Newtonsoft.Json;

namespace DW.ELA.Interfaces.Events;

public class EscapeInterdiction : JournalEvent
{
    [JsonProperty("Interdictor")]
    public required string Interdictor { get; set; }

    [JsonProperty("Interdictor_Localised")]
    public string? InterdictorLocalised { get; set; }

    [JsonProperty("IsPlayer")]
    public bool IsPlayer { get; set; }
}