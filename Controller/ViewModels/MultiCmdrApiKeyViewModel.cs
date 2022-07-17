
namespace DW.ELA.Controller.ViewModels;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using DW.ELA.Interfaces;
using DW.ELA.Interfaces.Settings;

public class MultiCmdrApiKeyViewModel : AbstractPluginSettingsViewModel
{
    private readonly IApiKeyValidator apiKeyValidator;
    private readonly Action<GlobalSettings, IReadOnlyDictionary<string, string>>? saveSettings;
    public event EventHandler<ApiKeyAddedEventArgs>? ApiKeyAdded;


    public MultiCmdrApiKeyViewModel(
        string id,
        IEnumerable<KeyValuePair<string, string>> apiKeys,
        IApiKeyValidator apiKeyValidator,
        string apiSettingsLink,
        GlobalSettings settings,
        Action<GlobalSettings, IReadOnlyDictionary<string, string>>? saveSettings)
        : base(id, settings)
    {
        this.apiKeyValidator = apiKeyValidator;
        this.saveSettings = saveSettings;
        ApiSettingsLink = apiSettingsLink;
        ApiKeys = new ObservableCollection<ApiKeyViewModel>(apiKeys.Select(kvp => new ApiKeyViewModel(kvp.Key, kvp.Value)));
    }

    public ObservableCollection<ApiKeyViewModel> ApiKeys { get; }
    public string ApiSettingsLink { get; }
    public IEnumerable<ApiKeyViewModel>? SelectedItems { get; set; }

    private bool isValidating;
    public bool IsValidating
    {
        get => isValidating;
        set => RaiseAndSetIfChanged(ref isValidating, value);
    }

    public override void SaveSettings()
    {
        var invalidApiKeys = ApiKeys.Where(key => string.IsNullOrWhiteSpace(key.Commander) || string.IsNullOrWhiteSpace(key.Key)).ToArray();
        var apiKeys = ApiKeys.Except(invalidApiKeys).ToDictionary(key => key.Commander, key => key.Key);

        RemoveKeys(invalidApiKeys);

        saveSettings?.Invoke(GlobalSettings, apiKeys);
    }

    public void AddEmptyKey()
    {
        var key = ApiKeyViewModel.Empty();
        ApiKeys.Add(key);
        ApiKeyAdded?.Invoke(this, new ApiKeyAddedEventArgs(key));
    }

    public void RemoveSelectedKeys()
    {
        if (SelectedItems == null)
            return;

        foreach (var kvp in SelectedItems)
            ApiKeys.Remove(kvp);
    }

    public async Task Validate()
    {
        IsValidating = true;

        var tasks = ApiKeys.Select(async key =>
        {
            if (string.IsNullOrWhiteSpace(key.Commander) || string.IsNullOrWhiteSpace(key.Key))
                key.Validity = ApiKeyValidity.Invalid;
            else
            {
                key.Validity = await apiKeyValidator.ValidateKeyAsync(key.Commander, key.Key)
                    ? ApiKeyValidity.Valid
                    : ApiKeyValidity.Invalid;
            }

            return key;
        });

        await Task.WhenAll(tasks);
        IsValidating = false;
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
            process?.WaitForExit();
    }

    private void RemoveKeys(IEnumerable<ApiKeyViewModel> keys)
    {
        foreach (var key in keys)
            ApiKeys.Remove(key);
    }
}