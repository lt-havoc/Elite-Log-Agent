namespace EliteLogAgent.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using DW.ELA.Interfaces;

    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel(IPluginManager pluginManager, ISettingsProvider settingsProvider)
        {
            SettingsControls = pluginManager.LoadedPlugins.Select(p => new KeyValuePair<string, AbstractSettingsViewModel>(p.PluginName, p.GetPluginSettingsViewModel(settingsProvider.Settings)));
            selectedPlugin = SettingsControls.First().Value;
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

        public void SelectPlugin(string pluginId) => SelectedPlugin = SettingsControls.Single(kvp => kvp.Key == pluginId).Value;
    }
}