namespace EliteLogAgent
{
    using System;
    using System.IO;
    using DW.ELA.Interfaces;

    public class EnvironmentDirectoryNameProvider : ILogDirectoryNameProvider
    {
        public string Directory
        {
            get
            {
                string? path = Environment.GetEnvironmentVariable("ED_SAVE_GAME_DIR");

                if (path != null && System.IO.Directory.Exists(path))
                    return path;
                
                throw new DirectoryNotFoundException($"Failed to find the saved games directory at '{path}'");
            }
        }
    }
}