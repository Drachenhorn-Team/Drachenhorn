using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DSACharacterSheet.DataObjects.Common
{
    [Serializable]
    public class SocialInformation : INotifyPropertyChanged
    {
        #region Properties

        [XmlIgnore]
        private string _class;
        [XmlAttribute("Class")]
        public string Class
        {
            get { return _class; }
            set
            {
                if (_class == value)
                    return;
                _class = value;
                OnPropertyChanged("Class");
            }
        }

        [XmlIgnore]
        private string _title;
        [XmlAttribute("Title")]
        public string Title
        {
            get { return _title; }
            set
            {
                if (_title == value)
                    return;
                _title = value;
                OnPropertyChanged("Title");
            }
        }

        [XmlIgnore]
        private ushort _socialClass;
        [XmlAttribute("SocialClass")]
        public ushort SocialClass
        {
            get { return _socialClass; }
            set
            {
                if (_socialClass == value)
                    return;
                _socialClass = value;
                OnPropertyChanged("SocialClass");
            }
        }

        [XmlIgnore]
        private string _background;
        [XmlAttribute("Background")]
        public string Background
        {
            get { return _background; }
            set
            {
                if (_background == value)
                    return;
                _background = value;
                OnPropertyChanged("Background");
            }
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
