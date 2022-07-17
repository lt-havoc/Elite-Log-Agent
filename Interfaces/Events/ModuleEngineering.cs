using Newtonsoft.Json;

namespace DW.ELA.Interfaces.Events;

public class ModuleEngineering
{
    [JsonProperty("Engineer")]
    public required string Engineer { get; set; }

    [JsonProperty("EngineerID")]
    public ulong EngineerId { get; set; }

    [JsonProperty("BlueprintID")]
    public ulong BlueprintId { get; set; }

    [JsonProperty("BlueprintName")]
    public required string BlueprintName { get; set; }

    [JsonProperty("Level")]
    public short Level { get; set; }

    [JsonProperty("Quality")]
    public double Quality { get; set; }

    [JsonProperty("Modifiers")]
    public required Modifier[] Modifiers { get; set; }

    [JsonProperty("ExperimentalEffect")]
    public string? ExperimentalEffect { get; set; }

    [JsonProperty("ExperimentalEffect_Localised")]
    public string? ExperimentalEffectLocalised { get; set; }
}