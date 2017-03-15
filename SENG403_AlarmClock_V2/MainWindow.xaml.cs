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

namespace SENG403_AlarmClock_V2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double snoozeTime = 5;
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        public static DateTime currentTime;
        
        public MainWindow()
        {
            InitializeComponent();
            hourLabel.Content = DateTime.Now.ToString("hh : mm");
            minuteLabel.Content = DateTime.Now.ToString(": ss");
            AMPM_thing.Content = DateTime.Now.ToString("tt");
            DayDate.Content = DateTime.Now.ToString("dddd, MMMM dd, yyyy");
            currentTime = DateTime.Now;
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1000);
            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            currentTime = currentTime.AddSeconds(1);
            hourLabel.Content = currentTime.ToString("hh : mm");
            minuteLabel.Content = currentTime.ToString(": ss");
            AMPM_thing.Content = currentTime.ToString("tt");
            DayDate.Content = currentTime.ToString("dddd, MMMM dd, yyyy");
            foreach (AlarmUserControl u in AlarmList_Panel.Children)
            {
                if (currentTime.CompareTo(u.alarm.GetNotificationTime()) > 0)
                {
                    Console.WriteLine(u.alarm.GetNotificationTime());
                    u.alarm.play();
                }
            }
           
        }

        /// <summary>
        /// Opens Window for New Alarm settings, and adds new alarm UserControl to StackPanel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddAlarmButton_Click(object sender, RoutedEventArgs e)
        {
            Alarm newAlarm = new Alarm("pack://application:,,,/Sounds/missileAlert.wav", snoozeTime);
            AlarmUserControl alarmControl = new AlarmUserControl(AlarmList_Panel, newAlarm);
            AlarmList_Panel.Children.Add(alarmControl);
            new NewAlarmWindow(alarmControl).ShowDialog();

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            new GlobalSettings(this).ShowDialog();
        }

        private void AlarmSideBarButton_Click(object sender, RoutedEventArgs e)
        {
            if (AlarmList_Panel.Visibility == Visibility.Visible) {
                AlarmList_Panel.Visibility = Visibility.Collapsed;
                AddAlarmButton.Visibility = Visibility.Collapsed;
                SettingsButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                AlarmList_Panel.Visibility = Visibility.Visible;
                AddAlarmButton.Visibility = Visibility.Visible;
                SettingsButton.Visibility = Visibility.Visible;
            }
        }

        private void ClockSpeedSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            dispatcherTimer.Stop();
            int updatedTimerInterval = (int)(1000.0 / Math.Pow(2, ClockSpeedSlider.Value));
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, updatedTimerInterval);
            dispatcherTimer.Start();
        }

        private void addDayButton_Click(object sender, RoutedEventArgs e)
        {
            currentTime = currentTime.AddDays(1);
        }

        public double GetSnoozeTime()
        {
            return snoozeTime;
        }
    }
}
