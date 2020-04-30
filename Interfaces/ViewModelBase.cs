#nullable enable
namespace DW.ELA.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public abstract class ViewModelBase : INotifyPropertyChanged, INotifyPropertyChanging
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public event PropertyChangingEventHandler? PropertyChanging;
        
        protected T RaiseAndSetIfChanged<T>(
            ref T backingField,
            T newValue,
            [CallerMemberName] string? propertyName = null)
        {
            if (propertyName == null)
                throw new ArgumentNullException(nameof(propertyName));
            
            if (EqualityComparer<T>.Default.Equals(backingField, newValue))
                return newValue;
            
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
            backingField = newValue;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            return newValue;
        }
    }
}