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
    /// Interaction logic for AlarmUserControl.xaml
    /// </summary>
    public partial class AlarmUserControl : UserControl
    {

        private Panel _parent;
        public Alarm alarm;

        private Stream fileStream;
        private BinaryFormatter formatter;

        /// <summary>
        /// Initializes user control with alarm information 
        /// </summary>
        public AlarmUserControl(StackPanel parent, Alarm alarm)
        {
            InitializeComponent();
            _parent = parent;
            this.alarm = alarm;
            AlarmTime_label.Content = alarm.notifyTime.ToString("hh:mm tt");

            formatter = new BinaryFormatter();
        }

        /// <summary>
        /// Snoozes this alarm controls alarm.
        /// </summary>
        internal void snoozeAlarm()
        {
            alarm.snooze();
        }

        internal void dismissAlarm()
        {
            alarm.update();
            if (alarm.repeatIntervalDays == -1)
            {
                EnableDisableAlarm_Button.Background = new SolidColorBrush(Colors.Green);
                EnableDisableAlarm_Button.Content = "Enable";
            }
        }

        /// <summary>
        /// sets the alarm user control time label to given DateTime
        /// </summary>
        /// <param name="time"></param>
        public void setTimeLabel(DateTime time)
        {
            AlarmTime_label.Content = time.ToString("hh:mm tt");
        }

        /// <summary>
        /// Toggles enable/disable alarm when user presses enable disable button
        /// </summary>
        private void EnableDisableAlarm_Button_Click(object sender, RoutedEventArgs e)
        {
            if (EnableDisableAlarm_Button.Content.Equals("Enable"))
            {
                EnableDisableAlarm_Button.Background = new SolidColorBrush(Colors.Red);
                EnableDisableAlarm_Button.Content = "Disable";
                alarm.enabled = true;
            }
            else if (EnableDisableAlarm_Button.Content.Equals("Disable"))
            {
                EnableDisableAlarm_Button.Background = new SolidColorBrush(Colors.Green);
                EnableDisableAlarm_Button.Content = "Enable";
                alarm.enabled = false;
            }
        }

        /// <summary>
        /// Removes alarm when delete alarm button is clicked
        /// </summary>
        private void DeleteAlarm_Click(object sender, RoutedEventArgs e)
        {
           
            //delete from list of alarms
            foreach (AlarmUserControl u in _parent.Children)
            {
                if (this.Equals(u))
                {                    
                    _parent.Children.Remove(u);
                    break;
                }
            }

            File.Delete("alarmFile.bin");

            //open file stream to rewrite alarm objects
            fileStream = new FileStream("alarmFile.bin", FileMode.Create, FileAccess.Write, FileShare.None);

            //rewrite alarm object file based on changes to alarm list
            foreach (AlarmUserControl alarmControl in _parent.Children)
            {
                formatter.Serialize(fileStream, alarmControl.alarm);
            }

            //close stream to yield file access
            fileStream.Close();
                
        }
        /// <summary>
        /// Method to check if alarm should go off and activate notification if alarm is tripped. 
        /// </summary>
        /// <param name="currentTime"></param>
        internal void requestAlarmWithCheck(DateTime currentTime)
        {
            DateTime clone = alarm.notifyTime.AddMinutes(5);
            if (alarm.enabled && alarm.notifyTime.CompareTo(MainWindow.currentTime) <= 0 && currentTime.CompareTo(clone) < 0)
               
            {
                if (alarm.enabled) Console.WriteLine("Test" + alarm.notifyTime);
                new NotificationWindow(this).ShowDialog();
            }
        }

        /// <summary>
        /// Opens edit alarm menu when edit button is clicked
        /// </summary>
        private void EditAlarm_Click(object sender, RoutedEventArgs e)
        {
            new NewAlarmWindow(this).ShowDialog();
        }
    }
}
