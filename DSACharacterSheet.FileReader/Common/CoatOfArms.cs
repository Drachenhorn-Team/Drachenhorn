using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Windows.Ink;
using System.IO;

namespace DSACharacterSheet.FileReader.Common
{
    [Serializable]
    public class CoatOfArms : BindableBase
    {
        [XmlIgnore]
        private StrokeCollection _strokes = new StrokeCollection();
        [XmlIgnore]
        public StrokeCollection Strokes
        {
            get { return _strokes; }
            private set
            {
                if (_strokes == value)
                    return;
                _strokes = value;
                OnPropertyChanged();
            }
        }
        [XmlElement("Strokes")]
        public byte[] StrokeStream
        {
            get
            {
                var ms = new MemoryStream();
                Strokes.Save(ms, true);
                ms.Position = 0;
                return ms.ToArray();
            }
            set
            {
                Strokes = new StrokeCollection(new MemoryStream(value));
            }
        }
    }
}
