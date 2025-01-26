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

namespace PomodoroTimerApp
{
    public partial class SettingsWindow : Window
    {
        public int WorkDuration { get; private set; }

        public SettingsWindow()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(WorkDurationTextBox.Text, out int duration) && duration > 0)
            {
                WorkDuration = duration;
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Please enter a valid number of minutes.");
            }
        }
    }
}
