#nullable enable

namespace DW.ELA.Controller
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using DW.ELA.Interfaces;
    using DW.ELA.Interfaces.Settings;
    using  MoreLinq;
    
    public class MultiCmdrApiKeyViewModel : AbstractSettingsViewModel
    {
        private readonly IApiKeyValidator apiKeyValidator;
        private readonly string apiSettingsLink;
        private readonly GlobalSettings settings;
        private readonly Action<GlobalSettings?, IReadOnlyDictionary<string, string>>? saveSettings;
        internal ObservableCollection<KeyValuePair<string ,string>> ApiKeys { get; }
        
        public MultiCmdrApiKeyViewModel(
            string id,
            IEnumerable<KeyValuePair<string, string>> apiKeys,
            IApiKeyValidator apiKeyValidator,
            string apiSettingsLink,
            GlobalSettings settings,
            Action<GlobalSettings?, IReadOnlyDictionary<string, string>>? saveSettings)
            : base(id)
        {
            this.apiKeyValidator = apiKeyValidator;
            this.apiSettingsLink = apiSettingsLink;
            this.settings = settings;
            this.saveSettings = saveSettings;
            ApiKeys = new ObservableCollection<KeyValuePair<string, string>>(apiKeys);
        }

        public override void SaveSettings() => saveSettings?.Invoke(GlobalSettings, ApiKeys.ToDictionary());
    }
}