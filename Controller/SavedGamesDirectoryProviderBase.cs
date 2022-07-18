using System;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using DW.ELA.Interfaces;

namespace DW.ELA.Controller;

public abstract class SavedGamesDirectoryProviderBase : ILogDirectoryNameProvider
{
    private readonly Lazy<string> lazyDirectoryValue;
    private readonly ISettingsProvider settingsProvider;

    protected SavedGamesDirectoryProviderBase(ISettingsProvider settingsProvider)
    {
        this.settingsProvider = settingsProvider;
        lazyDirectoryValue = new Lazy<string>(GetSavedGamesDirectory);
    }

    private string GetSavedGamesDirectory()
    {
        var settingsDir = ExpandEnvVars(settingsProvider.Settings.SaveGameDirectory);
        if (!string.IsNullOrEmpty(settingsDir))
            return settingsDir;

        return GetDefaultDirectory();
    }

    public string Directory => lazyDirectoryValue.Value;
    protected abstract string GetDefaultDirectory();
    
    protected static string? ExpandEnvVars(string? name)
    {
        if (string.IsNullOrEmpty(name))
            return name;

        // Platform checks needed until corefx supports platform specific vars
        // https://github.com/dotnet/corefx/issues/28890
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            name = Regex.Replace(name, @"\$(\w+)", "%$1%");
            name = Regex.Replace(name, "^~", "%HOME%");
        }

        return Environment.ExpandEnvironmentVariables(name);
    }
}