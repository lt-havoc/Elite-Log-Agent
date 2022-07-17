namespace EliteLogAgent.ViewModels;

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using DW.ELA.Interfaces;
using DW.ELA.Interfaces.Settings;
using DW.ELA.Utility;
using SettingsControl = System.Collections.Generic.KeyValuePair<string, DW.ELA.Interfaces.AbstractSettingsViewModel>;

public class MainWindowViewModel : ViewModelBase
{
    private readonly ISettingsProvider settingsProvider;
    private readonly GlobalSettings currentSettings;

    public MainWindowViewModel(IPluginManager pluginManager, ISettingsProvider settingsProvider, IAutorunManager autorunManager)
    {
        this.settingsProvider = settingsProvider;
        currentSettings = settingsProvider.Settings.Clone();
        var plugins = pluginManager.LoadedPlugins.Select(p => new SettingsControl(p.PluginName, p.GetPluginSettingsViewModel(currentSettings)));
        var generalSettings = new SettingsControl("General", new GeneralSettingsViewModel(currentSettings, autorunManager));
        SettingsControls = new ObservableCollection<SettingsControl> { generalSettings };
        foreach (var plugin in plugins)
            SettingsControls.Add(plugin);
        SelectedItem = generalSettings;
        PropertyChanging += OnPropertyChanging;
        Title = $"Elite Log Agent - Settings. Version: {AppInfo.Version}";
    }

    public ObservableCollection<SettingsControl> SettingsControls { get; }

    private SettingsControl selectedItem;
    public SettingsControl SelectedItem
    {
        get => selectedItem;
        set
        {
            RaiseAndSetIfChanged(ref selectedItem, value);
            RaiseAndSetIfChanged(ref selectedViewModel, value.Value, nameof(SelectedViewModel));
        }
    }

    private AbstractSettingsViewModel selectedViewModel = null!; // Set in constructor via SelectedItem property
    public AbstractSettingsViewModel SelectedViewModel => selectedViewModel;

    public string Title { get; }

    public void ApplyChanges()
    {
        SelectedViewModel.SaveSettings();
        settingsProvider.Settings = currentSettings;
    }


    private void OnPropertyChanging(object? sender, PropertyChangingEventArgs e)
    {
        if (e.PropertyName == nameof(SelectedViewModel))
            SelectedViewModel.SaveSettings();
    }
}