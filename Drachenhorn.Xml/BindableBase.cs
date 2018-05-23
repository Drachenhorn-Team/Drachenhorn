﻿using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Drachenhorn.Xml
{
    public abstract class BindableBase : INotifyPropertyChanged
    {
        #region OnPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion OnPropertyChanged
    }
}