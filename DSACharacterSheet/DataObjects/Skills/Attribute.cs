using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DSACharacterSheet.DataObjects.Skills
{
    [Serializable]
    public class Attribute : INotifyPropertyChanged
    {
        [XmlIgnore]
        private ushort _startValue;
        [XmlAttribute("StartValue")]
        public ushort StartValue
        {
            get { return _startValue; }
            set
            {
                if (_startValue == value)
                    return;
                _startValue = value;
                OnPropertyChanged("StartValue");
            }
        }

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
                OnPropertyChanged("Modifier");
            }
        }

        [XmlIgnore]
        private ushort _currentValue;
        [XmlAttribute("CurrentValue")]
        public ushort CurrentValue
        {
            get { return _currentValue; }
            set
            {
                if (_currentValue == value)
                    return;
                _currentValue = value;
                OnPropertyChanged("CurrentValue");
            }
        }


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
