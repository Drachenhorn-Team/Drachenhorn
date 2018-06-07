using Drachenhorn.Core.Objects;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Fluent;
using Xceed.Wpf.Toolkit;
using Color = System.Windows.Media.Color;

namespace Drachenhorn.Desktop.Views
{
    /// <summary>
    /// Interaktionslogik für CoatOfArmsPainterView.xaml
    /// </summary>
    public partial class CoatOfArmsPainterView : RibbonWindow, INotifyPropertyChanged
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

        public CoatOfArmsPainterView()
        {
            this.DataContext = this;

            InitializeComponent();
            
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

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            Canvas.Strokes.Clear();
        }

        public LimitedList<Stroke> UndoneStrokes = new LimitedList<Stroke>(50);

        private void UndoButton_Click(object sender, RoutedEventArgs e)
        {
            if (Strokes == null || Strokes.Count == 0)
                return;

            UndoneStrokes.Add(Strokes.ElementAt(Strokes.Count - 1));
            Strokes.RemoveAt(Strokes.Count - 1);
        }

        private void RedoButton_Click(object sender, RoutedEventArgs e)
        {
            if (UndoneStrokes.Count == 0)
                return;

            Strokes.Add(UndoneStrokes.Last());
            UndoneStrokes.Remove(UndoneStrokes.Last());
        }

        private void LinkSizeToggleButton_OnChecked(object sender, RoutedEventArgs e)
        {
            if (Canvas?.DefaultDrawingAttributes != null && Canvas?.DefaultDrawingAttributes != null)
                Canvas.DefaultDrawingAttributes.Height = Canvas.DefaultDrawingAttributes.Width;
        }

        private void HeightSlider_OnPreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (LinkSizeToggleButton.IsChecked == true)
                Canvas.DefaultDrawingAttributes.Width = Canvas.DefaultDrawingAttributes.Height;
        }

        private void WidthSlider_OnPreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (LinkSizeToggleButton.IsChecked == true)
                Canvas.DefaultDrawingAttributes.Height = Canvas.DefaultDrawingAttributes.Width;
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