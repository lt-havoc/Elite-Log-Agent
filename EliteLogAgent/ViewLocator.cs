namespace EliteLogAgent
{
    using System;
    using Avalonia.Controls;
    using Avalonia.Controls.Templates;
    using DW.ELA.Interfaces;
    using NLog;

    public class ViewLocator : IDataTemplate
    {
        private static readonly ILogger Log = LogManager.GetCurrentClassLogger();
        public bool SupportsRecycling => false;

        public IControl Build(object data)
        {
            var name = data.GetType().FullName!.Replace("ViewModel", "View");
            var type = Type.GetType(name);

            if (type != null)
            {
                try
                {
                    if (Activator.CreateInstance(type) is Control view)
                        return view;
                }
                catch (Exception e)
                {
                    Log.Error(e, $"Couldn't create instance of view '{type}'");
                }
            }

            return new TextBlock { Text = "View Not Found: " + name };
        }

        public bool Match(object data) => data is ViewModelBase;
    }
}