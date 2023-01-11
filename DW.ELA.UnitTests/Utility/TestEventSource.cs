using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using DW.ELA.Controller;
using DW.ELA.Interfaces;
using DW.ELA.Interfaces.Settings;
using DW.ELA.LogModel;
using DW.ELA.Utility.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DW.ELA.UnitTests;

public static class TestEventSource
{
    public static IEnumerable<JournalEvent> TypedLogEvents => CannedEvents.Where(e => e.GetType() != typeof(JournalEvent));

    public static IEnumerable<JournalEvent> CannedEvents => CannedEventsRaw.Select(JournalEventConverter.Convert);

    public static IEnumerable<JournalEvent> LocalEvents => LocalEventsRaw.Select(JournalEventConverter.Convert);

    public static IEnumerable<JObject> CannedEventsRaw
    {
        get
        {
            var assembly = Assembly.GetExecutingAssembly();
            string resourceName = "DW.ELA.UnitTests.CannedEvents.json";

            using var stream = assembly.GetManifestResourceStream(resourceName) ?? throw new InvalidOperationException($"Manifest resource '{resourceName}' cannot be null");
            using var textReader = new StreamReader(stream);
            using var jsonReader = new JsonTextReader(textReader) { SupportMultipleContent = true, CloseInput = false };
            while (jsonReader.Read())
            {
                yield return Converter.Serializer.Deserialize<JObject>(jsonReader) ?? throw new Exception($"Unable to deserialize resource '{resourceName}'");
            }
        }
    }

    public static IEnumerable<JObject> LocalBetaEvents
    {
        get
        {
            foreach (string file in Directory.EnumerateFiles(CreateSavedGameDirHelper().Directory, "JournalBeta.*.log"))
            {
                using var fileReader = File.OpenRead(file);
                using var textReader = new StreamReader(fileReader);
                using var jsonReader = new JsonTextReader(textReader) { SupportMultipleContent = true, CloseInput = false };
                while (jsonReader.Read())
                {
                    yield return Converter.Serializer.Deserialize<JObject>(jsonReader) ?? throw new Exception($"Unable to deserialize file '{file}'");
                }
            }
        }
    }

    public static IEnumerable<JObject> LocalEventsRaw
    {
        get
        {
            foreach (string file in Directory.EnumerateFiles(CreateSavedGameDirHelper().Directory, "Journal.*.log"))
            {
                using var fileReader = File.OpenRead(file);
                using var textReader = new StreamReader(fileReader);
                using var jsonReader = new JsonTextReader(textReader) { SupportMultipleContent = true, CloseInput = false };
                while (jsonReader.Read())
                {
                    yield return Converter.Serializer.Deserialize<JObject>(jsonReader) ?? throw new Exception($"Unable to deserialize file '{file}'");
                }
            }
        }
    }

    public static IEnumerable<JObject> LocalStaticEvents
    {
        get
        {
            var reader = new JournalFileReader();
            foreach (string file in Directory.EnumerateFiles(CreateSavedGameDirHelper().Directory, "*.json"))
                yield return JObject.Parse(File.ReadAllText(file));
        }
    }

    private static ILogDirectoryNameProvider CreateSavedGameDirHelper()
    {
        var settingsProvider = new SettingsProviderStub();
        if (OperatingSystem.IsWindows())
            return new WindowsSavedGamesDirectoryProvider(settingsProvider);
        if (OperatingSystem.IsLinux())
            return new LinuxSavedGamesDirectoryProvider(settingsProvider);
        
        throw new InvalidOperationException($"Don't know where to find log files for platform '{RuntimeInformation.OSDescription}'");
    }

    private class SettingsProviderStub : ISettingsProvider
    {
#pragma warning disable 67
        public event EventHandler? SettingsChanged;
#pragma warning restore
        public GlobalSettings Settings { get; set; } = GlobalSettings.Default;
    }
}