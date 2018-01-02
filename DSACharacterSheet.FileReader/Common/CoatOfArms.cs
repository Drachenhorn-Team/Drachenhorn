using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Windows.Ink;
using System.IO;
using System.Windows.Media.Imaging;

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
                byte[] result;
                using (var ms = new MemoryStream())
                {
                    Strokes.Save(ms, true);
                    ms.Position = 0;
                    result = ms.ToArray();
                }

                return result;
            }
            set
            {
                Strokes = new StrokeCollection(new MemoryStream(value));
            }
        }
        [XmlIgnore]
        public string Base64String
        {
            get
            {
                var arr = StrokeStream;

                bitm


            Dim gifData As Byte() = Nothing
            Using ink2 As New Microsoft.Ink.Ink()
                ink2.Load(inkData)
                gifData = ink2.Save(Microsoft.Ink.PersistenceFormat.Gif)
            End Using
            File.WriteAllBytes("c://strokes.gif", gifData)


                return Encoding.ASCII.GetString(StrokeStream);
            }
        }
    }
}
