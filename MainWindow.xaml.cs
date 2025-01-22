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
using System.Timers;


namespace PomodoroTimerApp
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Timer timer;
        private int timeLeft = 25 * 60; // 25 minutes in seconds

        public MainWindow()
        {
            InitializeComponent();
            timer = new Timer(1000); // 1 second interval
            timer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                if (timeLeft > 0)
                {
                    timeLeft--;
                    TimerDisplay.Text = $"{timeLeft / 60:D2}:{timeLeft % 60:D2}";
                }
                else
                {
                    timer.Stop();
                    MessageBox.Show("Time's up!");
                }
            });
        }

        private void StartButton_Click(object sender, RoutedEventArgs e) => timer.Start();

        private void PauseButton_Click(object sender, RoutedEventArgs e) => timer.Stop();

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            timeLeft = 25 * 60;
            TimerDisplay.Text = "25:00";
        }
    }

}
