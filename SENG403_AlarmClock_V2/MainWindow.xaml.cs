using System;
using System.Windows;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

namespace SENG403_AlarmClock_V2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static public double snoozeTime = 5;
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        System.Windows.Threading.DispatcherTimer dispatcherTimer2 = new System.Windows.Threading.DispatcherTimer();
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
            dispatcherTimer2.Tick += dispatcherTimer_check;
            dispatcherTimer2.Interval = new TimeSpan(0, 0, 0, 0, 1000);
            dispatcherTimer2.Start();

            formatter = new BinaryFormatter();
            if (File.Exists("alarmFile.bin"))
            {
                AlarmList_Panel.Children.Clear();
                loadAlarmFile();
            }
        }
        private void dispatcherTimer_check(object sender, EventArgs e)
        {
            
            foreach (AlarmUserControl u in AlarmList_Panel.Children)
            {
                u.requestAlarmWithCheck(currentTime);
            }

        }
        /// <summary>
        /// update the time display each second and check if any alarm should go off at that time
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            currentTime = currentTime.AddSeconds(1);
            hourLabel.Content = currentTime.ToString("hh : mm");
            minuteLabel.Content = currentTime.ToString(": ss");
            AMPM_thing.Content = currentTime.ToString("tt");
            DayDate.Content = currentTime.ToString("dddd, MMMM dd, yyyy");

           
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

            if (newAlarm.firstcreation)
            AlarmList_Panel.Children.Add(alarmControl);


        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Opens settings window when settings button is clicked
        /// </summary>
        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            new GlobalSettings(this).ShowDialog();
        }

        /// <summary>
        /// Toggles the alarm side bar visibility when hamburger menu button is pressed
        /// </summary>
        private void AlarmSideBarButton_Click(object sender, RoutedEventArgs e)
        {
            if (AlarmList_Panel.Visibility == Visibility.Visible) {
                AlarmList_Panel.Visibility = Visibility.Collapsed;
                AddAlarmButton.Visibility = Visibility.Collapsed;
                SettingsButton.Visibility = Visibility.Collapsed;
                Sidebar_Back.Visibility = Visibility.Hidden;
                SidebarH.Visibility = Visibility.Hidden;
                TimeDisplay.Margin = new Thickness(334, 237, 292, 253);
            }
            else
            {
                AlarmList_Panel.Visibility = Visibility.Visible;
                AddAlarmButton.Visibility = Visibility.Visible;
                SidebarH.Visibility = Visibility.Visible;
                SettingsButton.Visibility = Visibility.Visible;
                Sidebar_Back.Visibility = Visibility.Visible;
                TimeDisplay.Margin = new Thickness(474, 237, 152, 253);
            }
        }

        /// <summary>
        /// For debug and demo purposes. Increases/Decreases time speed when clock speed slider is moved
        /// </summary>
        private void ClockSpeedSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            dispatcherTimer.Stop();
            int updatedTimerInterval = (int)(1000.0 / Math.Pow(2, ClockSpeedSlider.Value));
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, updatedTimerInterval);
            dispatcherTimer.Start();
        }

        /// <summary>
        /// For debug and demo purposes. Increments the day when add day button is pressed in debug menu
        /// </summary>
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

            //read all alarm objects from file
            while (fileStream.Position != fileStream.Length)
            {
                Alarm loadedAlarm = (Alarm) formatter.Deserialize(fileStream);
                AlarmUserControl alarmControl = new AlarmUserControl(AlarmList_Panel, loadedAlarm);
                alarmControl.updateDisplay();
                AlarmList_Panel.Children.Add(alarmControl);
            }

            fileStream.Close();
        }

        /// <summary>
        /// Displays debug options when debug button is clicked.
        /// </summary>
        private void Debug_Click(object sender, RoutedEventArgs e)
        {
            if (Debug_Options.Visibility == Visibility.Visible)
            {
                Debug_Options.Visibility = Visibility.Hidden;
                Debug.Content = "Show Debug";
            }

            else
            {
                Debug_Options.Visibility = Visibility.Visible;
                Debug.Content = "Hide Debug";
            }
               
        }

        /// <summary>
        /// Before closing program, save the list of alarms into binary file for persistent storage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            List<Alarm> AlarmList = new List<Alarm>();
            fileStream = new FileStream("alarmFile.bin", FileMode.Create, FileAccess.Write, FileShare.None);

            //load all instantiated alarms into a list
            foreach (AlarmUserControl control in  AlarmList_Panel.Children)
            {
                AlarmList.Add(control.alarm);
            }

            //write all alarms to file before closing
            foreach(Alarm alarm in AlarmList)
            {
                formatter.Serialize(fileStream, alarm);
            }

            fileStream.Close();
        }
    }
}
