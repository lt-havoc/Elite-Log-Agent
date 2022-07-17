using Newtonsoft.Json;

namespace DW.ELA.Plugin.EDDN.Model;

public partial class Economy
{
    public Economy(string name)
    {
        Name = name;
    }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("proportion")]
    public double Proportion { get; set; }
}