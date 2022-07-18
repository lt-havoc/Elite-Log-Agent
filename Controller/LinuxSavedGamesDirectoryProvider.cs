using System.Collections.Generic;
using System.IO;
using DW.ELA.Interfaces;
using DW.ELA.Utility;

namespace DW.ELA.Controller;

public sealed class LinuxSavedGamesDirectoryProvider : SavedGamesDirectoryProviderBase
{
    private const string SteamAppId = "359320";
    
    public LinuxSavedGamesDirectoryProvider(ISettingsProvider settingsProvider)
        : base(settingsProvider)
    { }

    protected override string GetDefaultDirectory()
    {
        var home = ExpandEnvVars("~");
        var user = ExpandEnvVars("$USER");
        const string protonUserPath = $"steamapps/compatdata/{SteamAppId}/pfx/drive_c/users/steamuser";
        
        var potentialDirs = new List<string>
        {
            $"{home}/.steam/steam/{protonUserPath}",
            $"{home}/.local/share/Steam/{protonUserPath}",
            $"{home}/Games/elite-dangerous/drive_c/users/{user}", // lutris
            $"{home}/.wine/drive_c/users/{user}"
        };

        if (GetSteamLibraryFolder($"{home}/.steam/root/config") is { } steamLibFolder)
            potentialDirs.Insert(0, Path.Combine(steamLibFolder, protonUserPath));

        foreach (var dir in potentialDirs)
        {
            if (!System.IO.Directory.Exists(dir))
                continue;

            return $"{dir}/Saved Games/Frontier Developments/Elite Dangerous";
        }

        throw new DirectoryNotFoundException("Failed to find the saved games directory.");
    }

    private static string? GetSteamLibraryFolder(string rootConfigPath)
    {
        var libraryFolders = Path.Combine(rootConfigPath, "libraryfolders.vdf");

        if (!File.Exists(libraryFolders))
            return null;

        return SteamHelper.ParseAppLibraryPath(SteamAppId, File.ReadLines(libraryFolders));
    }
}