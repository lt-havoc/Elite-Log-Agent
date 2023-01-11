using System;
using System.IO;
using DW.ELA.Controller;
using DW.ELA.Interfaces;
using DW.ELA.Interfaces.Settings;
using NUnit.Framework;

namespace DW.ELA.UnitTests.Controller;

[Platform("WIN")]
public class WindowsSavedGamesDirectoryProviderTests
{
    [Test]
    [Explicit]
    public void ShouldFindSavesDirectory() => Assert.IsNotEmpty(new WindowsSavedGamesDirectoryProvider(new SettingsProviderStub()).Directory);

    [Test]
    public void Prioritizes_Settings_Path_Over_Default()
    {
        var expectedPath = Path.Combine("test", "path");
        var settings = new SettingsProviderStub
        {
            Settings = new GlobalSettings { SaveGameDirectory = expectedPath }
        };

        var result = new WindowsSavedGamesDirectoryProvider(settings).Directory;
        
        Assert.AreEqual(expectedPath, result);
    }
    
    private class SettingsProviderStub : ISettingsProvider
    {
#pragma warning disable 67
        public event EventHandler? SettingsChanged;
#pragma warning restore
        public GlobalSettings Settings { get; set; } = GlobalSettings.Default;
    }
}