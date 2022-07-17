using System;
using Newtonsoft.Json;

namespace DW.ELA.Plugin.EDDN.Model;

internal partial class OutfittingMessage
{
    [JsonProperty("marketId")]
    public long MarketId { get; set; }

    [JsonProperty("modules")]
    public required string[] Modules { get; set; }

    [JsonProperty("stationName")]
    public required string StationName { get; set; }

    [JsonProperty("systemName")]
    public required string SystemName { get; set; }

    [JsonProperty("timestamp")]
    public DateTime Timestamp { get; set; }
}