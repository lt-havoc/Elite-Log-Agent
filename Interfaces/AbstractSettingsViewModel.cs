#nullable enable

namespace DW.ELA.Interfaces
{
    using DW.ELA.Interfaces.Settings;

    public abstract class AbstractSettingsViewModel : ViewModelBase
    {
        public AbstractSettingsViewModel(string id, GlobalSettings? globalSettings)
        {
            Id = id;
            GlobalSettings = globalSettings;
        }
    
        /// <summary>
        /// Gets or sets reference to temporary instance of Settings existing in settings form
        /// </summary>
        public GlobalSettings? GlobalSettings { get; set; }
        public string Id { get; }
    
        public virtual void SaveSettings()
        {
        }
    }
}
