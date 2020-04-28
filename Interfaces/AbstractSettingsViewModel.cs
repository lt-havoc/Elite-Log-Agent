#nullable enable

namespace DW.ELA.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using DW.ELA.Interfaces.Settings;

    public abstract class AbstractSettingsViewModel : INotifyPropertyChanged
    {
        public AbstractSettingsViewModel(string id)
        {
            Id = id;
        }
    
        /// <summary>
        /// Gets or sets reference to temporary instance of Settings existing in settings form
        /// </summary>
        public GlobalSettings? GlobalSettings { get; set; }
        public string Id { get; }
    
        public virtual void SaveSettings()
        {
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected TRet RaiseAndSetIfChanged<TRet>(
            ref TRet backingField,
            TRet newValue,
            [CallerMemberName] string? propertyName = null)
        {
            if (propertyName == null)
                throw new ArgumentNullException(nameof(propertyName));
            
            if (EqualityComparer<TRet>.Default.Equals(backingField, newValue))
            {
                return newValue;
            }

            backingField = newValue;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            return newValue;
        }
    }
}
