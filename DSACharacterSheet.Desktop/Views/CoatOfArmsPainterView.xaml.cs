using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
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
        public CoatOfArmsPainterView()
        {
            InitializeComponent();
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
                Canvas.DefaultDrawingAttributes.Height = 2;
                Canvas.DefaultDrawingAttributes.Width = 2;
            }
        }

        private void BrushStrength_Checked_2(object sender, RoutedEventArgs e)
        {
            if (Canvas != null)
            {
                Canvas.DefaultDrawingAttributes.Height = 4;
                Canvas.DefaultDrawingAttributes.Width = 4;
            }
        }

        private void BrushStrength_Checked_3(object sender, RoutedEventArgs e)
        {
            if (Canvas != null)
            {
                Canvas.DefaultDrawingAttributes.Height = 8;
                Canvas.DefaultDrawingAttributes.Width = 8;
            }
        }

        private void BrushStrength_Checked_4(object sender, RoutedEventArgs e)
        {
            if (Canvas != null)
            {
                Canvas.DefaultDrawingAttributes.Height = 12;
                Canvas.DefaultDrawingAttributes.Width = 12;
            }
        }

        #endregion BrushStrength

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            Canvas.Strokes.Clear();
        }
    }
}
