using DW.ELA.Interfaces;
using Newtonsoft.Json;

namespace DW.ELA.Interfaces.Events;

public class EngineerProgress : JournalEvent
{
    [JsonProperty("Engineers")]
    public required EngineerProgressRecord[] Engineers { get; set; }
}