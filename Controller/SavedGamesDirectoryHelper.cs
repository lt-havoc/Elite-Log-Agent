namespace DW.ELA.Controller;

using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using DW.ELA.Interfaces;

public class SavedGamesDirectoryHelper : ILogDirectoryNameProvider
{
    private readonly Lazy<string> lazyDirectoryValue;
    private readonly ISettingsProvider settingsProvider;

    public SavedGamesDirectoryHelper(ISettingsProvider settingsProvider)
    {
        this.settingsProvider = settingsProvider;
        lazyDirectoryValue = new Lazy<string>(GetSavedGamesDirectory);
    }

    public string Directory => lazyDirectoryValue.Value;

    private string GetSavedGamesDirectory()
    {
        var settingsDir = ExpandEnvVars(settingsProvider.Settings.SaveGameDirectory);
        if (!string.IsNullOrEmpty(settingsDir))
            return settingsDir;

        return GetDefaultDirectory();
    }

    private static string GetDefaultDirectory()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            return GetWindowsDefault();

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            return GetLinuxDefault();

        throw new DirectoryNotFoundException($"Failed to find the saved games directory. Unknown platform {RuntimeInformation.OSDescription}");
    }

    private static string GetLinuxDefault()
    {
        var home = ExpandEnvVars("~");
        var user = ExpandEnvVars("$USER");
        var potentialDirs = new[]
        {
            $"{home}/.steam/steam/steamapps/compatdata/359320/pfx/drive_c/users/steamuser",
            $"{home}/.local/share/Steam/steamapps/compatdata/359320/pfx/drive_c/users/steamuser",
            $"{home}/Games/elite-dangerous/drive_c/users/{user}", // lutris
            $"{home}/.wine/drive_c/users/{user}"
        };

        foreach (var dir in potentialDirs)
        {
            if (!System.IO.Directory.Exists(dir))
                continue;

            return $"{dir}/Saved Games/Frontier Developments/Elite Dangerous";
        }

        throw new DirectoryNotFoundException("Failed to find the saved games directory.");
    }

    private static string? ExpandEnvVars(string? name)
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

    private static string GetWindowsDefault()
    {
        int result = UnsafeNativeMethods.SHGetKnownFolderPath(new Guid("4C5C32FF-BB9D-43B0-B5B4-2D72E54EAAA4"), 0, new IntPtr(0), out var path);
        if (result >= 0)
        {
            try
            {
                return Marshal.PtrToStringUni(path) + @"\Frontier Developments\Elite Dangerous";
            }
            finally { Marshal.FreeCoTaskMem(path); }
        }
        else
        {
            throw new ExternalException("Failed to find the saved games directory.", result);
        }
    }

    private static class UnsafeNativeMethods
    {
        [DllImport("Shell32.dll")]
        public static extern int SHGetKnownFolderPath([MarshalAs(UnmanagedType.LPStruct)] Guid rfid, uint dwFlags, IntPtr hToken, out IntPtr ppszPath);
    }
}