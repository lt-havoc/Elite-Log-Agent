using System.Collections.Generic;
using DW.ELA.Utility.Json;
using Newtonsoft.Json;

namespace DW.ELA.Plugin.Inara.Model;

internal struct ApiOutputBatch
{
    public ApiOutputBatch(Header header, IList<ApiOutputEvent> events)
    {
        Header = header;
        Events = events;
    }
    [JsonProperty("header")]
    public Header Header;

    [JsonProperty("events")]
    public IList<ApiOutputEvent>? Events;

    public override readonly string ToString() => Serialize.ToJson(this);
}