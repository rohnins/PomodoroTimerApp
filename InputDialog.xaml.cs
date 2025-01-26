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
    /// <summary>
    /// Interaktionslogik für InputDialog.xaml
    /// </summary>
    public partial class InputDialog : Window
    {
        public string InputText { get; private set; } // Ensure this property is defined

        public InputDialog()
        {
            InitializeComponent(); // Ensure this is called
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            InputText = InputTextBox.Text; // Ensure InputTextBox is defined in XAML
            DialogResult = true;
            Close();
        }
    }
}
