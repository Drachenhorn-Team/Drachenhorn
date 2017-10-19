using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DSACharacterSheet.FileReader.Common
{
    [Serializable]
    public class SocialInformation : BindableBase
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
                OnPropertyChanged();
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
                OnPropertyChanged();
            }
        }

        [XmlIgnore]
        private double _socialClass;
        [XmlAttribute("SocialClass")]
        public double SocialClass
        {
            get { return _socialClass; }
            set
            {
                if (_socialClass == value)
                    return;
                _socialClass = value;
                OnPropertyChanged();
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
                OnPropertyChanged();
            }
        }

        #endregion Properties
    }
}
