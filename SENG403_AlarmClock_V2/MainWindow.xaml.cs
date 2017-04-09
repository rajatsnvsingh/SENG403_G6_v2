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
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SENG403_AlarmClock_V2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static public double snoozeTime = 5;
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        public static DateTime currentTime;

        private Stream fileStream;
        private BinaryFormatter formatter;

        
        public MainWindow()
        {
            InitializeComponent();

            //time display elements
            hourLabel.Content = DateTime.Now.ToString("hh : mm");
            minuteLabel.Content = DateTime.Now.ToString(": ss");
            AMPM_thing.Content = DateTime.Now.ToString("tt");
            DayDate.Content = DateTime.Now.ToString("dddd, MMMM dd, yyyy");
            currentTime = DateTime.Now;
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1000);
            dispatcherTimer.Start();

            formatter = new BinaryFormatter();
            if(File.Exists("alarmFile.bin"))
            {
                //AlarmList_Panel.Children.Clear();
                loadAlarmFile();
            }
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            currentTime = currentTime.AddSeconds(1);
            hourLabel.Content = currentTime.ToString("hh : mm");
            minuteLabel.Content = currentTime.ToString(": ss");
            AMPM_thing.Content = currentTime.ToString("tt");
            DayDate.Content = currentTime.ToString("dddd, MMMM dd, yyyy");
            int windowsOpen = 0;
            foreach (AlarmUserControl u in AlarmList_Panel.Children)
            {
                
                if (windowsOpen > 0)
                {
                    missedAlarmLabel.Visibility = Visibility.Visible;
                }
                else if (currentTime.CompareTo(u.alarm.notifyTime) > 0)
                {
                    Console.WriteLine(u.alarm.notifyTime);
                    u.alarm.play();
                    windowsOpen++;
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
            Alarm newAlarm = new Alarm(@"..\..\Sounds\missileAlert.wav", snoozeTime);
            AlarmUserControl alarmControl = new AlarmUserControl(AlarmList_Panel, newAlarm);
            new NewAlarmWindow(alarmControl).ShowDialog();
            AlarmList_Panel.Children.Add(alarmControl);


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



        /// <summary>
        /// if file for alarm objects exists, load serialized objects
        /// </summary>
        private void loadAlarmFile()
        {
            fileStream = new FileStream("alarmFile.bin", FileMode.Open, FileAccess.Read, FileShare.Read);

            //AlarmList_Panel.Children.Clear();

            while (fileStream.Position != fileStream.Length)
            {
                Alarm loadedAlarm = (Alarm) formatter.Deserialize(fileStream);
                AlarmUserControl alarmControl = new AlarmUserControl(AlarmList_Panel, loadedAlarm);
                AlarmList_Panel.Children.Add(alarmControl);
            }

            fileStream.Close();
        }
    }
}
