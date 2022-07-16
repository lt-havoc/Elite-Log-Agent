#nullable enable

namespace DW.ELA.Interfaces;

using DW.ELA.Interfaces.Settings;

public abstract class AbstractSettingsViewModel : ViewModelBase
{
    public AbstractSettingsViewModel(GlobalSettings globalSettings)
    {
        GlobalSettings = globalSettings;
    }

    /// <summary>
    /// Gets or sets reference to temporary instance of Settings existing in settings form
    /// </summary>
    public GlobalSettings GlobalSettings { get; set; }

    public virtual void SaveSettings()
    {
    }
}