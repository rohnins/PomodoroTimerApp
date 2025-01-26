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
using System.Windows.Threading;
using Microsoft.VisualBasic;


namespace PomodoroTimerApp
{
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer;
        private int timeLeft = 25 * 60; // 25 minutes in seconds

        public MainWindow()
        {
            InitializeComponent();
            InitializeTimer();
        }

        private void ShowNotification(string message)
        {
            NotificationText.Text = message;
            // Optional: Clear the notification after a few seconds
            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(3) };
            timer.Tick += (s, e) => { NotificationText.Text = ""; timer.Stop(); };
            timer.Start();
        }

        private void InitializeTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1); // 1 second interval
            timer.Tick += Timer_Tick;
        }

        private int streakCount = 0;
        private DateTime lastCompletionDate = DateTime.MinValue;

        private void UpdateStreak()
        {
            DateTime today = DateTime.Today;
            if (lastCompletionDate != today)
            {
                if ((today - lastCompletionDate).Days == 1)
                {
                    streakCount++;
                }
                else
                {
                    streakCount = 1; // Reset streak if not consecutive
                }
                lastCompletionDate = today;
                StreakCounter.Text = $"Streak: {streakCount} days";
            }
        }

        private void UpdateBackgroundColor()
        {
            double progress = (double)timeLeft / (25 * 60); // Progress from 1 (start) to 0 (end)
            GradientStop1.Color = Color.FromRgb(
                (byte)(205 + (255 - 205) * (1 - progress)), // Red component
                (byte)(16 + (255 - 16) * (1 - progress)),   // Green component
                (byte)(20 + (255 - 20) * (1 - progress))    // Blue component
            );
            GradientStop2.Color = Colors.White;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (timeLeft > 0)
            {
                timeLeft--;
                TimerDisplay.Text = $"{timeLeft / 60:D2}:{timeLeft % 60:D2}";
                UpdateBackgroundColor(); // Updates the background
            }
            else
            {
                timer.Stop();
                UpdateStreak();
                ShowNotification("Time's up!");
            }
        }

        private void StartButton_Click(object sender, RoutedEventArgs e) => timer.Start();

        private void PauseButton_Click(object sender, RoutedEventArgs e) => timer.Stop();

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            timeLeft = 25 * 60;
            TimerDisplay.Text = "25:00";
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            var inputDialog = new InputDialog();
            if (inputDialog.ShowDialog() == true)
            {
                if (int.TryParse(inputDialog.InputText, out int duration) && duration > 0)
                {
                    timeLeft = duration * 60;
                    TimerDisplay.Text = $"{timeLeft / 60:D2}:{timeLeft % 60:D2}";
                }
                else
                {
                    ShowNotification("Please enter a valid number of minutes.");
                }
            }
        }



    }

}
