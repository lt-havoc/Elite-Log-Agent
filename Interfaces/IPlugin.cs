using System;
using DW.ELA.Interfaces.Settings;

namespace DW.ELA.Interfaces;

public interface IPlugin
{
    string PluginName { get; }

    string PluginId { get; }

    /// <summary>
    /// Get an observer for the logs event stream
    /// </summary>
    /// <returns>log event observer</returns>
    IObserver<JournalEvent> GetLogObserver();

    /// <summary>
    /// Gets a view model which changes plugin settings
    /// Method is provided with a reference to settings item which can be changed
    /// View model MUST NOT change any global state - only the passed GlobalSettings instance
    /// </summary>
    /// <param name="settings">Instance of temporary settings object held in setup session</param>
    /// <returns>Plugin settings view model</returns>
#nullable enable
    AbstractSettingsViewModel GetPluginSettingsViewModel(GlobalSettings settings);

    /// <summary>
    /// Gets the type of the view associated with the view model
    /// </summary>
    /// <returns>Plugin settings view's type</returns>
    Type View { get; }
#nullable restore
    /// <summary>
    /// Callback to signal settings have changed and it's time to update
    /// </summary>
    /// <param name="sender">sender object</param>
    /// <param name="e">event data</param>
    void OnSettingsChanged(object sender, EventArgs e);

    /// <summary>
    /// Explicitly request to flush queue - on shutdown
    /// </summary>
    [Obsolete]
    void FlushQueue();
}