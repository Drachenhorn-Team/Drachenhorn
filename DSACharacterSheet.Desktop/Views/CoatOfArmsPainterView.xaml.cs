using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit;

namespace DSACharacterSheet.Desktop.Views
{
    /// <summary>
    /// Interaktionslogik für CoatOfArmsPainterView.xaml
    /// </summary>
    public partial class CoatOfArmsPainterView : Window
    {
        private StrokeCollection _strokes;
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

        public CoatOfArmsPainterView(StrokeCollection strokes)
        {
            this.DataContext = this;
            Strokes = strokes;

            InitializeComponent();

            InkCanvasScaleTransform.ScaleX = 3;
            InkCanvasScaleTransform.ScaleY = 3;

            ClrPcker_Brush.SelectedColor = Canvas.DefaultDrawingAttributes.Color;
            ClrPcker_Brush.StandardColors = new ObservableCollection<ColorItem>()
            {
                new ColorItem(Color.FromRgb(220, 20,  60), "Crimson"),
                new ColorItem(Color.FromRgb(67,  110, 238), "Royal Blue"),
                new ColorItem(Color.FromRgb(0,   201, 87), "Emerald Green"),
                new ColorItem(Color.FromRgb(255, 215, 0), "Gold"),
                new ColorItem(Color.FromRgb(192, 192, 192), "Silver"),
                new ColorItem(Color.FromRgb(41,  36,  33), "Ivory Black"),
            };
        }

        private void ClrPcker_Brush_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            Canvas.DefaultDrawingAttributes.Color = (Color)ClrPcker_Brush.SelectedColor;
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




        #region OnPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion OnPropertyChanged
    }
}
