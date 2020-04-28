namespace EliteLogAgent.Deployment
{
    using System;
    using System.IO;
    using DW.ELA.Interfaces;

    public class DataPathManager : IPathManager
    {
        // TODO: set based on deployment method
        // public string SettingsDirectory => ApplicationDeployment.IsNetworkDeployed ? AppDataDirectory : LocalDirectory;
        //
        // public string LogDirectory => ApplicationDeployment.IsNetworkDeployed ? Path.Combine(AppDataDirectory, "Log") : Path.Combine(LocalDirectory, "Log");
        
        public string SettingsDirectory => LocalDirectory;

        public string LogDirectory => Path.Combine(LocalDirectory, "Log");

        private string AppDataDirectory => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "EliteLogAgent");

        private string LocalDirectory => Path.GetDirectoryName(new Uri(typeof(Program).Assembly.CodeBase!).LocalPath)!;
    }
}
