namespace EliteLogAgent.ViewModels;

using System;
using System.Collections.Generic;
using DW.ELA.Interfaces;
using DW.ELA.Interfaces.Settings;
using NLog;

public class GeneralSettingsViewModel : AbstractSettingsViewModel
{
    private readonly IAutorunManager autorunManager;

    public GeneralSettingsViewModel(GlobalSettings settings, IAutorunManager autorunManager)
        : base(settings)
    {
        this.autorunManager = autorunManager;

        try
        {
            logLevel = LogLevel.FromString(settings.LogLevel ?? "Info");
        }
        catch (ArgumentException)
        {
            logLevel = LogLevel.Info;
        }

        runOnStartup = autorunManager.AutorunEnabled;
        logToCloud = settings.ReportErrorsToCloud;
        saveGameDir = settings.SaveGameDirectory;
        ShowRunOnStartup = autorunManager.CanManage;
    }

    public IEnumerable<LogLevel> LogLevels => LogLevel.AllLevels;
    public bool ShowRunOnStartup { get; }

    private bool runOnStartup;
    public bool RunOnStartup
    {
        get => runOnStartup;
        set
        {
            RaiseAndSetIfChanged(ref runOnStartup, value);
            autorunManager.AutorunEnabled = value;
        }
    }

    private bool logToCloud;
    public bool LogToCloud
    {
        get => logToCloud;
        set
        {
            RaiseAndSetIfChanged(ref logToCloud, value);
            GlobalSettings.ReportErrorsToCloud = value;
        }
    }

    private LogLevel logLevel;
    public LogLevel LogLevel
    {
        get => logLevel;
        set
        {
            RaiseAndSetIfChanged(ref logLevel, value);
            GlobalSettings.LogLevel = value.ToString();
        }
    }

    private string? saveGameDir;
    public string? SaveGameDir
    {
        get => saveGameDir;
        set
        {
            var dir = RaiseAndSetIfChanged(ref saveGameDir, value?.Trim());
            GlobalSettings.SaveGameDirectory = dir == "" ? null : value;
        }
    }
}