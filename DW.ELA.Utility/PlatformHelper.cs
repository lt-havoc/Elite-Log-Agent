using System;
using System.Diagnostics;

namespace DW.ELA.Utility;

public static class PlatformHelper
{
    public static void OpenUri(string uri)
    {
        if (OperatingSystem.IsLinux())
        {
            // If no associated application/json MimeType is found xdg-open opens retrun error
            // but it tries to open it anyway using the console editor (nano, vim, other..)
            ShellExec($"xdg-open {uri}");
        }
        else
        {
            using var process = Process.Start(new ProcessStartInfo
            {
                FileName = OperatingSystem.IsWindows() ? uri : "open",
                Arguments = OperatingSystem.IsMacOS() ? $"-e {uri}" : "",
                CreateNoWindow = true,
                UseShellExecute = OperatingSystem.IsWindows()
            });
        }
    }

    private static void ShellExec(string cmd, bool waitForExit = false)
    {
        var escapedArgs = cmd.Replace("\"", "\\\"");

        using var process = Process.Start(new ProcessStartInfo
        {
            FileName = "/bin/sh",
            Arguments = $"-c \"{escapedArgs}\"",
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true,
            WindowStyle = ProcessWindowStyle.Hidden
        });
        if (waitForExit)
            process?.WaitForExit();
    }
}