namespace DW.ELA.Plugin.EDDN;

using Interfaces;
using Interfaces.Settings;

public class EddnSettingsViewModel : AbstractPluginSettingsViewModel
{
    public EddnSettingsViewModel(string id) : base(id, GlobalSettings.Default) { }
}