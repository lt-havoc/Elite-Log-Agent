#nullable enable

namespace DW.ELA.Interfaces
{
    using DW.ELA.Interfaces.Settings;

    public abstract class AbstractPluginSettingsViewModel : AbstractSettingsViewModel
    {
        public AbstractPluginSettingsViewModel(string id, GlobalSettings globalSettings)
            : base(globalSettings)
        {
            Id = id;
        }
        
        public string Id { get; }
    }
}
