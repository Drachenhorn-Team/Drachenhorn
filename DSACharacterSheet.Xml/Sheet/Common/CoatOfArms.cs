using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.DrawingCore;
using System.IO;
using System.Xml.Serialization;
using DSACharacterSheet.Xml.Objects;

namespace DSACharacterSheet.Xml.Sheet.Common
{
    [Serializable]
    public class CoatOfArms : BindableBase
    {
        //[XmlIgnore]
        //private Bitmap _image;
        //[XmlElement("Image")]
        //public Bitmap Image
        //{
        //    get { return _image; }
        //    set
        //    {
        //        if (_image == value)
        //            return;
        //        _image = value;
        //        OnPropertyChanged();
        //    }
        //}

        [XmlIgnore]
        private string _base64String;

        [XmlAttribute("Image")]
        public string Base64String
        {
            get { return _base64String; }
            set
            {
                if (_base64String == value)
                    return;
                _base64String = value;
                OnPropertyChanged();
            }
        }
        //[XmlIgnore]
        //public string Base64String
        //{
        //    get
        //    {
        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            Image.Save(ms, System.DrawingCore.Imaging.ImageFormat.Bmp);
        //            return Convert.ToBase64String(ms.ToArray());
        //        }
        //    }
        //}

        //[XmlElement("Strokes")]
        //public byte[] StrokeStream
        //{
        //    get
        //    {
        //        byte[] result;
        //        using (var ms = new MemoryStream())
        //        {
        //            Strokes.Save(ms, true);
        //            ms.Position = 0;
        //            result = ms.ToArray();
        //        }

        //        return result;
        //    }
        //    set
        //    {
        //        Strokes = new StrokeCollection(new MemoryStream(value));
        //    }
        //}

        //[XmlIgnore]
        //public string Base64String
        //{
        //    get
        //    {
        //        Bitmap bitmap = new Bitmap(WIDTH, HEIGHT);

        //        using (Graphics gfx = Graphics.FromImage(bitmap))
        //        {
        //            //use a brush to fill the image rectangle, because the default color of new bitmaps is black.
        //            SolidBrush oBrush = new SolidBrush(System.DrawingCore.Color.White);
        //            gfx.FillRectangle(oBrush, 0, 0, bitmap.Width, bitmap.Height);

        //            gfx.SmoothingMode = System.DrawingCore.Drawing2D.SmoothingMode.AntiAlias;

        //            //draw the signature
        //            RenderStrokes(gfx, Strokes);
        //        }

        //        string result = "";

        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            bitmap.Save(ms, System.DrawingCore.Imaging.ImageFormat.Bmp);
        //            result = Convert.ToBase64String(ms.ToArray());
        //        }

        //        return result;
        //    }
        //}

        //private void RenderStrokes(Graphics g, IEnumerable<Stroke> customStrokes)
        //{
        //    foreach (var stroke in customStrokes)
        //    {
        //        var color = stroke.Color;
        //        Pen pen = new Pen(Color.FromArgb(color.A, color.R, color.G, color.B), (float) stroke.Width);
        //            if (stroke.Count <= 0)
        //                continue;

        //            if (stroke.Count == 1)
        //                g.DrawRectangle(pen, new Rectangle(stroke[0].X, stroke[0].Y, 1, 1));
        //            else
        //                g.DrawLines(pen, stroke.GetDrawingCorePoints().ToArray());
        //    }
        //}
    }
}