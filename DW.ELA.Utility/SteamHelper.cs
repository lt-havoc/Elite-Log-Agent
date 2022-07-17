using System;
using System.Collections.Generic;
using System.Linq;

namespace DW.ELA.Utility;

public static class SteamHelper
{
    public static string? ParseAppLibraryPath(string appId, IEnumerable<string> libraryFoldersVdfLines) =>
        libraryFoldersVdfLines
            .Reverse()
            .SkipWhile(l => !l.TrimStart().StartsWith($"\"{appId}\""))
            .SkipWhile(l => !l.TrimStart().StartsWith("\"path\""))
            .FirstOrDefault()
            ?.Split(new[] { ' ', '\t' }, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .Last()
            .Trim('"');
}