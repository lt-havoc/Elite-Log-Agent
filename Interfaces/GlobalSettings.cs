namespace DW.ELA.Interfaces.Settings;

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class GlobalSettings : ICloneable
{
    [JsonIgnore]
    public static GlobalSettings Default => new();

    [JsonProperty("pluginSettings")]
    public IDictionary<string, JObject> PluginSettings { get; set; } = new Dictionary<string, JObject>();

    [JsonProperty("logLevel")]
    public string LogLevel { get; set; } = "Info";

    [JsonProperty("saveGameDirectory")]
    public string? SaveGameDirectory { get; set; } = null;

    object ICloneable.Clone() => Clone();

    public GlobalSettings Clone() => JsonConvert.DeserializeObject<GlobalSettings>(JsonConvert.SerializeObject(this)) ?? throw new InvalidOperationException($"Cannot clone null {nameof(GlobalSettings)}");
}