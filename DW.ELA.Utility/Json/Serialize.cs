using System;
using Newtonsoft.Json;

namespace DW.ELA.Utility.Json;

public static class Serialize
{
    public static string ToJson<T>(this T self) => JsonConvert.SerializeObject(self, Converter.Settings);

#pragma warning disable CS8603
    public static T FromJson<T>(string json) => JsonConvert.DeserializeObject<T>(json, Converter.Settings);
#pragma warning restore CS8603

#pragma warning disable CS8603
    public static object FromJson(Type type, string json) => JsonConvert.DeserializeObject(json, type, Converter.Settings);
#pragma warning restore CS8603
}