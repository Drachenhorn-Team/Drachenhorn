using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using DSACharacterSheet.DataObjects.Calculation;

namespace DSACharacterSheet.DataObjects.Skills
{
    [Serializable]
    public class BaseValue : INotifyPropertyChanged
    {
        #region Properties

        [XmlIgnore]
        private ushort _modifier;
        [XmlAttribute("Modifier")]
        public ushort Modifier
        {
            get { return _modifier; }
            set
            {
                if (_modifier == value)
                    return;
                _modifier = value;
                OnPropertyChanged(null);
            }
        }

        [XmlIgnore]
        private Formula _formula = new Formula();
        [XmlElement("Formula")]
        public Formula Formula
        {
            get { return _formula; }
            set
            {
                if (_formula == value)
                    return;
                _formula = value;
                OnPropertyChanged("null");
            }
        }

        [XmlIgnore]
        public ushort Value
        {
            //TODO: Implementieren
            get { return 0; }
        }

        #endregion Properties


        #region OnPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion OnPropertyChanged
    }
}
