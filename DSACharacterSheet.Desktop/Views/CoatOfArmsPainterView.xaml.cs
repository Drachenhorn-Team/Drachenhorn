using DSACharacterSheet.Core.Objects;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Xceed.Wpf.Toolkit;
using Color = System.Windows.Media.Color;

namespace DSACharacterSheet.Desktop.Views
{
    /// <summary>
    /// Interaktionslogik für CoatOfArmsPainterView.xaml
    /// </summary>
    public partial class CoatOfArmsPainterView : Window
    {
        private ObservableCollection<Stroke> _strokes;

        public ObservableCollection<Stroke> Strokes
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

        public CoatOfArmsPainterView(string base64String)
        {
            this.DataContext = this;

            InitializeComponent();

            //if (!String.IsNullOrEmpty(base64String))
            //{
            //    byte[] binaryData = Convert.FromBase64String(base64String);

            //    BitmapImage bi = new BitmapImage();
            //    bi.BeginInit();
            //    bi.StreamSource = new MemoryStream(binaryData);
            //    bi.EndInit();

            //    Canvas.Background = new ImageBrush(bi);
            //}

            //InkCanvasScaleTransform.ScaleX = 3;
            //InkCanvasScaleTransform.ScaleY = 3;

            ClrPcker_Brush.SelectedColor = Canvas.DefaultDrawingAttributes.Color;
            ClrPcker_Brush.StandardColors = new ObservableCollection<ColorItem>()
            {
                new ColorItem(Colors.Transparent, "Transparent"),
                new ColorItem(Color.FromRgb(220, 20,  60), "Crimson"),
                new ColorItem(Color.FromRgb(67,  110, 238), "Royal Blue"),
                new ColorItem(Color.FromRgb(0,   201, 87), "Emerald Green"),
                new ColorItem(Color.FromRgb(255, 215, 0), "Gold"),
                new ColorItem(Color.FromRgb(192, 192, 192), "Silver"),
                new ColorItem(Color.FromRgb(139, 69,  19), "Brown"),
                new ColorItem(Color.FromRgb(41,  36,  33), "Ivory Black"),
            };
        }

        private void ClrPcker_Brush_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            Canvas.DefaultDrawingAttributes.Color = (Color)ClrPcker_Brush.SelectedColor;
        }

        public string GetBase64()
        {
            int margin = (int)Canvas.Margin.Left;
            int width = (int)Canvas.ActualWidth - margin;
            int height = (int)Canvas.ActualHeight - margin;
            RenderTargetBitmap renderBitmap =
                new RenderTargetBitmap(width, height, 96d, 96d, PixelFormats.Default);
            renderBitmap.Render(Canvas);
            //save the ink to a memory stream
            BitmapEncoder encoder;
            encoder = new BmpBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(renderBitmap));

            using (var ms = new MemoryStream())
            {
                encoder.Save(ms);
                return Convert.ToBase64String(ms.ToArray());
            }
        }

        #region BrushType

        private void BrushType_Checked_1(object sender, RoutedEventArgs e)
        {
            if (Canvas != null)
                Canvas.EditingMode = InkCanvasEditingMode.InkAndGesture;
        }

        private void BrushType_Checked_2(object sender, RoutedEventArgs e)
        {
            if (Canvas != null)
                Canvas.EditingMode = InkCanvasEditingMode.EraseByPoint;
        }

        private void BrushType_Checked_3(object sender, RoutedEventArgs e)
        {
            if (Canvas != null)
                Canvas.EditingMode = InkCanvasEditingMode.Select;
        }

        #endregion BrushType

        #region BrushStrength

        private void BrushStrength_Checked_1(object sender, RoutedEventArgs e)
        {
            if (Canvas != null)
            {
                Canvas.DefaultDrawingAttributes.Height = .7;
                Canvas.DefaultDrawingAttributes.Width = .7;
            }
        }

        private void BrushStrength_Checked_2(object sender, RoutedEventArgs e)
        {
            if (Canvas != null)
            {
                Canvas.DefaultDrawingAttributes.Height = 1.5;
                Canvas.DefaultDrawingAttributes.Width = 1.5;
            }
        }

        private void BrushStrength_Checked_3(object sender, RoutedEventArgs e)
        {
            if (Canvas != null)
            {
                Canvas.DefaultDrawingAttributes.Height = 3;
                Canvas.DefaultDrawingAttributes.Width = 3;
            }
        }

        private void BrushStrength_Checked_4(object sender, RoutedEventArgs e)
        {
            if (Canvas != null)
            {
                Canvas.DefaultDrawingAttributes.Height = 5.5;
                Canvas.DefaultDrawingAttributes.Width = 5.5;
            }
        }

        #endregion BrushStrength

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            Canvas.Strokes.Clear();
        }

        public LimitedList<Stroke> _undoneStrokes = new LimitedList<Stroke>(50);

        private void UndoButton_Click(object sender, RoutedEventArgs e)
        {
            if (Strokes.Count == 0)
                return;

            _undoneStrokes.Add(Strokes.ElementAt(Strokes.Count - 1));
            Strokes.RemoveAt(Strokes.Count - 1);
        }

        private void RedoButton_Click(object sender, RoutedEventArgs e)
        {
            if (_undoneStrokes.Count == 0)
                return;

            Strokes.Add(_undoneStrokes.Last());
            _undoneStrokes.Remove(_undoneStrokes.Last());
        }

        #region OnPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion OnPropertyChanged
    }
}