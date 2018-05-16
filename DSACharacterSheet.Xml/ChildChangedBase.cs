using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using DSACharacterSheet.Xml.Interfaces;
using DSACharacterSheet.Xml.Template;

namespace DSACharacterSheet.Xml
{
    public abstract class ChildChangedBase : BindableBase, INotifyChildChanged
    {
        #region OnChildChanged

        public event PropertyChangedEventHandler ChildChanged;

        protected virtual void OnChildChanged([CallerMemberName]string propertyName = null)
        {
            ChildChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnChildChanged(object sender, EventArgs args)
        {
            OnChildChanged(null);
        }

        #endregion OnChildChanged

        #region c'tor

        protected ChildChangedBase()
        {
            SetValuesChanged();
        }

        #endregion c'tor

        #region Helper

        protected void SetValuesChanged()
        {
            foreach (var property in this.GetType().GetProperties())
            {
                if (property.GetIndexParameters().Length != 0) continue;

                var val = property.GetValue(this);

                if (val is INotifyChildChanged)
                    ((INotifyChildChanged)val).ChildChanged += OnChildChanged;

                if (val is INotifyPropertyChanged)
                    ((INotifyPropertyChanged)val).PropertyChanged += OnChildChanged;

                if (val is INotifyCollectionChanged)
                    ((INotifyCollectionChanged)val).CollectionChanged += ChildCollectionChanged;
            }

            this.PropertyChanged += SelfPropertyChanged;
        }

        private void ChildCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            if (args.Action == NotifyCollectionChangedAction.Add)
                foreach (var item in args.NewItems)
                {
                    ((INotifyPropertyChanged)item).PropertyChanged += OnChildChanged;
                }
            else if (args.Action == NotifyCollectionChangedAction.Remove)
                foreach (var item in args.OldItems)
                {
                    ((INotifyPropertyChanged)item).PropertyChanged -= OnChildChanged;
                }
        }

        private void SelfPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            var property = this.GetType().GetProperty(args.PropertyName).GetValue(this);

            if (property is INotifyChildChanged)
                ((INotifyChildChanged)property).ChildChanged += OnChildChanged;

            if (property is INotifyPropertyChanged)
                ((INotifyPropertyChanged)property).PropertyChanged += OnChildChanged;

            if (property is INotifyCollectionChanged)
                ((INotifyCollectionChanged)property).CollectionChanged += ChildCollectionChanged;

        }

        #endregion Helper
    }
}
