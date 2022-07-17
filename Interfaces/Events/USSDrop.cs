using Newtonsoft.Json;

namespace DW.ELA.Interfaces.Events;

public class USSDrop : JournalEvent
{
    [JsonProperty("USSType")]
    public required string UssType { get; set; }

    [JsonProperty("USSType_Localised")]
    public required string UssTypeLocalised { get; set; }

    [JsonProperty("USSThreat")]
    public int UssThreat { get; set; }
}