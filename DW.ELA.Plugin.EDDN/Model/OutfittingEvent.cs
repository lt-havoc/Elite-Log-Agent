using Newtonsoft.Json;

namespace DW.ELA.Plugin.EDDN.Model;

internal class OutfittingEvent : EddnEvent
{
    [JsonProperty("message")]
    public required OutfittingMessage Message { get; set; }

    public override string SchemaRef => "https://eddn.edcd.io/schemas/outfitting/2";
}