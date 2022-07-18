using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DW.ELA.Controller;
using DW.ELA.Interfaces;
using DW.ELA.Interfaces.Settings;
using DW.ELA.Plugin.Inara;
using DW.ELA.Plugin.Inara.Model;
using DW.ELA.UnitTests.Utility;
using DW.ELA.Utility;
using NUnit.Framework;

namespace DW.ELA.UnitTests.INARA;

[TestFixture]
public class InaraIntegrationTests
{
    private readonly IReadOnlyCollection<string> ignoredErrors = new HashSet<string>
    {
        "Everything was alright, the near-neutral status just wasn't stored.",
        "There is a newer inventory state recorded already.",
        "This ship was not found but was added automatically."
    };

    [Test]
    [Explicit]
    public async Task IntegrationTestUploadToInara()
    {
        var logEventSource = new JournalBurstPlayer(CreateSavedGameDirProvider().Directory, 5);
        var logCounter = new JournalEventTypeCounter();
        var stateRecorder = new PlayerStateRecorder();

        var inaraRestClient = new ThrottlingRestClient.Factory().CreateRestClient("https://inara.cz/inapi/v1/");
        var inaraConverter = new InaraEventConverter(stateRecorder);
        var inaraApiFacade = new InaraApiFacade(inaraRestClient, TestCredentials.UserName, TestCredentials.Inara.ApiKey, null);

        // Populate the state recorder to avoid missing ships/starsystems data
        foreach (var ev in logEventSource.Events)
            stateRecorder.OnNext(ev);

        var convertedEvents = logEventSource
            .Events
            .Where(e => e.Timestamp.AddMonths(1) > DateTime.UtcNow)
            .SelectMany(inaraConverter.Convert)
            .ToArray();

        if (!convertedEvents.Any())
            Assert.Inconclusive("No recent enough events to convert");

        var results = await inaraApiFacade.ApiCall(convertedEvents);
        results = results
            .Where(e => e.EventStatus != 200)
            .Where(e => !ignoredErrors.Contains(e.EventStatusText))
            .ToList();

        CollectionAssert.IsEmpty(results);
        Assert.Pass("Uploaded {0} events", convertedEvents.Length);
    }
    
    private static ILogDirectoryNameProvider CreateSavedGameDirProvider()
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