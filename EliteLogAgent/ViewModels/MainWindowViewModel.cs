namespace EliteLogAgent.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using DW.ELA.Interfaces;
    using DW.ELA.Interfaces.Settings;

    public class MainWindowViewModel : ViewModelBase
    {
        private readonly ISettingsProvider settingsProvider;
        private readonly GlobalSettings currentSettings;

        public MainWindowViewModel(IPluginManager pluginManager, ISettingsProvider settingsProvider)
        {
            this.settingsProvider = settingsProvider;
            currentSettings = settingsProvider.Settings.Clone();
            SettingsControls = pluginManager.LoadedPlugins.Select(p => new KeyValuePair<string, AbstractSettingsViewModel>(p.PluginName, p.GetPluginSettingsViewModel(currentSettings)));
            selectedPlugin = SettingsControls.First().Value;
            PropertyChanging += OnPropertyChanging;
        }

        public IEnumerable<KeyValuePair<string, AbstractSettingsViewModel>> SettingsControls { get; }

        public KeyValuePair<string, AbstractSettingsViewModel> SelectedItem
        {
            set => SelectedPlugin = value.Value;
        }
        
        private AbstractSettingsViewModel selectedPlugin;
        public AbstractSettingsViewModel SelectedPlugin
        {
            get => selectedPlugin;
            set => RaiseAndSetIfChanged(ref selectedPlugin, value);
        }

        public void ApplyChanges() => settingsProvider.Settings = currentSettings;
        
        private void OnPropertyChanging(object sender, PropertyChangingEventArgs e)
        {
            if (e.PropertyName == nameof(SelectedPlugin))
                SelectedPlugin.SaveSettings();
        }
    }
}