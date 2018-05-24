using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Drachenhorn.Xml.Interfaces;
using Drachenhorn.Xml.Template;

namespace Drachenhorn.Xml
{
    public abstract class ChildChangedBase : BindableBase, INotifyChildChanged
    {
        #region OnChildChanged

        /// <summary>
        /// Occurs when [child changed].
        /// </summary>
        public event PropertyChangedEventHandler ChildChanged;

        /// <summary>
        /// Called when [child changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected virtual void OnChildChanged([CallerMemberName]string propertyName = null)
        {
            ChildChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Called when [child changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
        protected virtual void OnChildChanged(object sender, PropertyChangedEventArgs args)
        {
            OnChildChanged(args.PropertyName);
        }

        #endregion OnChildChanged

        #region c'tor

        /// <summary>
        /// Initializes a new instance of the <see cref="ChildChangedBase"/> class.
        /// </summary>
        protected ChildChangedBase()
        {
            SetValuesChanged();
        }

        #endregion c'tor

        #region Helper

        /// <summary>
        /// Sets the ValueChanged.
        /// </summary>
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

        /// <summary>
        /// Sets the Childs CollectionChanged.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="NotifyCollectionChangedEventArgs"/> instance containing the event data.</param>
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

        /// <summary>
        /// Sets Self PropertyChanged.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
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
