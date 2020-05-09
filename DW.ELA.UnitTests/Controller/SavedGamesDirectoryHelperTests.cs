namespace DW.ELA.UnitTests.Controller
{
    using System;
    using DW.ELA.Controller;
    using Interfaces;
    using Interfaces.Settings;
    using NUnit.Framework;

    // TODO: run on Windows only
    [Explicit]
    public class SavedGamesDirectoryHelperTests
    {
        [Test]
        public void ShouldFindSavesDirectory() => Assert.IsNotEmpty(new SavedGamesDirectoryHelper(new SettingsProviderStub()).Directory);
        
        private class SettingsProviderStub : ISettingsProvider
        {
#pragma warning disable 67
            public event EventHandler SettingsChanged;
#pragma warning restore
            public GlobalSettings Settings { get; set; } = GlobalSettings.Default;
        }
    }

    
}
