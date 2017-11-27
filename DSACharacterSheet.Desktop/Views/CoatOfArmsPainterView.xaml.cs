using System;
using System.Collections.Generic;
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

            InkCanvasScaleTransform.ScaleX = 2;
            InkCanvasScaleTransform.ScaleY = 2;

            ClrPcker_Brush.SelectedColor = Canvas.DefaultDrawingAttributes.Color;
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

        #endregion BrushType


        #region BrushStrength

        private void BrushStrength_Checked_1(object sender, RoutedEventArgs e)
        {
            if (Canvas != null)
            {
                Canvas.DefaultDrawingAttributes.Height = 1;
                Canvas.DefaultDrawingAttributes.Width = 1;
            }
        }

        private void BrushStrength_Checked_2(object sender, RoutedEventArgs e)
        {
            if (Canvas != null)
            {
                Canvas.DefaultDrawingAttributes.Height = 2;
                Canvas.DefaultDrawingAttributes.Width = 2;
            }
        }

        private void BrushStrength_Checked_3(object sender, RoutedEventArgs e)
        {
            if (Canvas != null)
            {
                Canvas.DefaultDrawingAttributes.Height = 4;
                Canvas.DefaultDrawingAttributes.Width = 4;
            }
        }

        private void BrushStrength_Checked_4(object sender, RoutedEventArgs e)
        {
            if (Canvas != null)
            {
                Canvas.DefaultDrawingAttributes.Height = 6;
                Canvas.DefaultDrawingAttributes.Width = 6;
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
