using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Ink;
using System.Xml.Serialization;
using DSACharacterSheet.FileReader.Objects;

namespace DSACharacterSheet.FileReader.Sheet.Common
{
    [Serializable]
    public class CoatOfArms : BindableBase
    {
        public const int HEIGHT = 200;
        public const int WIDTH = 300;

        [XmlIgnore]
        public int Height
        {
            get { return HEIGHT; }
        }

        [XmlIgnore]
        public int Width
        {
            get { return WIDTH; }
        }

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
                Bitmap bitmap = new Bitmap(WIDTH, HEIGHT);

                using (Graphics gfx = Graphics.FromImage(bitmap))
                {
                    //use a brush to fill the image rectangle, because the default color of new bitmaps is black.  
                    SolidBrush oBrush = new SolidBrush(Color.White);
                    gfx.FillRectangle(oBrush, 0, 0, bitmap.Width, bitmap.Height);

                    gfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                    //draw the signature   
                    RenderSignature(gfx, ToPoints(Strokes));
                }

                string result = "";

                using (MemoryStream ms = new MemoryStream())
                {
                    bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                    result = Convert.ToBase64String(ms.ToArray());
                }

                return result;
            }
        }

        private List<CustomStroke> ToPoints(StrokeCollection strokes)
        {
            var result = new List<CustomStroke>();

            foreach (var stroke in strokes)
                result.Add(new CustomStroke(stroke));

            return result;
        }

        private void RenderSignature(Graphics g, List<CustomStroke> customStrokes)
        {
            foreach (var stroke in customStrokes)
            {
                using (Pen pen = new Pen(stroke.Color, (float)stroke.Width))
                {
                    if (stroke.Count <= 0)
                        continue;

                    if (stroke.Count == 1)
                        g.DrawRectangle(pen, new Rectangle(stroke[0].X, stroke[0].Y, 1, 1));
                    else
                        g.DrawLines(pen, stroke.Points.ToArray());
                }
            }
        }
    }
}
