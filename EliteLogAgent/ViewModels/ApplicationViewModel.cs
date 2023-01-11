using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using DW.ELA.Interfaces;
using DW.ELA.Utility;

namespace EliteLogAgent.ViewModels;

public class ApplicationViewModel : ViewModelBase
{
    private readonly IPathManager pathManager;

    public ApplicationViewModel(IPathManager pathManager)
    {
        this.pathManager = pathManager;
    }

    public void BrowseLogs() => PlatformHelper.OpenUri(pathManager.LogDirectory);
    public void ReportIssue() => PlatformHelper.OpenUri("https://github.com/rfvgyhn/Elite-Log-Agent/issues");
    public void OpenChangelog() => PlatformHelper.OpenUri("https://github.com/rfvgyhn/Elite-Log-Agent/CHANGELOG.md");

    public void Exit()
    {
        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime lifetime)
            lifetime.Shutdown();
    }
}