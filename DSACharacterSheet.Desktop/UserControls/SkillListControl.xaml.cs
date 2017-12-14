﻿using DSACharacterSheet.FileReader.Skills;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DSACharacterSheet.Desktop.UserControls
{
    /// <summary>
    /// Interaktionslogik für SkillListControl.xaml
    /// </summary>
    public partial class SkillListControl : UserControl
    {
        public SkillListControl()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(List.ItemsSource is IList<Skill>))
                return;

            var newItem = new Skill();

            ((IList<Skill>)List.ItemsSource).Add(newItem);
            List.SelectedItem = newItem;
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(List.ItemsSource is IList<Skill>))
                return;

            ((IList<Skill>)List.ItemsSource).Remove((Skill)List.SelectedItem);
        }
    }
}
