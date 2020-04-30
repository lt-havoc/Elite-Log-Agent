#nullable enable

namespace DW.ELA.Controller
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.InteropServices;
    using DW.ELA.Interfaces;
    using DW.ELA.Interfaces.Settings;
    
    public class MultiCmdrApiKeyViewModel : AbstractSettingsViewModel
    {
        private readonly IApiKeyValidator apiKeyValidator;
        private readonly Action<GlobalSettings?, IReadOnlyDictionary<string, string>>? saveSettings;

        public MultiCmdrApiKeyViewModel(
            string id,
            IEnumerable<KeyValuePair<string, string>> apiKeys,
            IApiKeyValidator apiKeyValidator,
            string apiSettingsLink,
            GlobalSettings settings,
            Action<GlobalSettings?, IReadOnlyDictionary<string, string>>? saveSettings)
            : base(id, settings)
        {
            this.apiKeyValidator = apiKeyValidator;
            this.saveSettings = saveSettings;
            ApiSettingsLink = apiSettingsLink;
            ApiKeys = new ObservableCollection<ApiKey>(apiKeys.Select(kvp => new ApiKey(kvp.Key, kvp.Value)));
        }

        public ObservableCollection<ApiKey> ApiKeys { get; }
        public string ApiSettingsLink { get; }
        public IEnumerable<ApiKey>? SelectedItems { get; set; }
        
        private string cmdrName = "";
        public string CmdrName
        {
            get => cmdrName;
            set => RaiseAndSetIfChanged(ref cmdrName, value);
        }
        
        private string apiKey = "";
        public string ApiKey
        {
            get => apiKey;
            set => RaiseAndSetIfChanged(ref apiKey, value);
        }

        public override void SaveSettings() => saveSettings?.Invoke(GlobalSettings, ApiKeys.ToDictionary(key => key.Commander, key => key.Key));

        public void AddKey()
        {
            if (string.IsNullOrWhiteSpace(CmdrName) || string.IsNullOrWhiteSpace(ApiKey))
                return;
            
            ApiKeys.Add(new ApiKey(CmdrName, ApiKey));
            CmdrName = "";
            ApiKey = "";
        }
        
        public void RemoveKeys()
        {
            if (SelectedItems == null)
                return;
            
            foreach (var kvp in SelectedItems)
                ApiKeys.Remove(kvp);
        }
        
        public static void OpenBrowser(string url)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                // If no associated application/json MimeType is found xdg-open opens retrun error
                // but it tries to open it anyway using the console editor (nano, vim, other..)
                ShellExec($"xdg-open {url}");
            }
            else
            {
                using var process = Process.Start(new ProcessStartInfo
                {
                    FileName = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? url : "open",
                    Arguments = RuntimeInformation.IsOSPlatform(OSPlatform.OSX) ? $"-e {url}" : "",
                    CreateNoWindow = true,
                    UseShellExecute = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
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
                process.WaitForExit();
        }
    }

    public enum ApiKeyValidity { Valid, Invalid, Unknown }
    public class ApiKey
    {
        public ApiKey(string commander, string key, ApiKeyValidity validity = ApiKeyValidity.Unknown)
        {
            Commander = commander;
            Key = key;
            Validity = validity;
        }
        public string Commander { get; }
        public string Key { get; }
        public ApiKeyValidity Validity { get; }
    }
}