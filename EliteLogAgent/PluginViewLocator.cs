namespace EliteLogAgent;

using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using DW.ELA.Interfaces;
using NLog;

public class PluginViewLocator : IDataTemplate
{
    private static readonly ILogger Log = LogManager.GetCurrentClassLogger();
    private readonly IEnumerable<IPlugin> plugins;
    public bool SupportsRecycling => false;

    public PluginViewLocator(IEnumerable<IPlugin> plugins)
    {
        this.plugins = plugins;
    }

    public IControl Build(object data)
    {
        var viewModel = (AbstractPluginSettingsViewModel)data;
        var plugin = plugins.SingleOrDefault(p => p.PluginId == viewModel.Id);
        var name = data.GetType().FullName ?? "Unknown view model";

        if (plugin != null)
        {
            try
            {
                if (Activator.CreateInstance(plugin.View) is Control view)
                    return view;
            }
            catch (Exception e)
            {
                Log.Error(e, $"Couldn't create instance of view '{plugin.View}'");
            }
        }

        return new TextBlock { Text = $"Plugin view not found for {plugin?.PluginName ?? name}" };
    }

    public bool Match(object data) => data is AbstractPluginSettingsViewModel;
}