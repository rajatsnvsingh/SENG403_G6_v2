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
        public DateTime boundaryTime;
        
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

        /// <summary>
        /// turns off the alarm after it has rung
        /// </summary>
        internal void dismissAlarm()
        {
            alarm.update();
            if (alarm.oneTimeAlarm)
            {
                //EnableDisableAlarm_Button.Background = new SolidColorBrush(Colors.Green);
                //EnableDisableAlarm_Button.Content = "Enable";
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
        }

        internal void updateDisplay()
        {
            if (!alarm.oneTimeAlarm)
            {
                AlarmTime_label.Content = alarm.defaultAlarmTime.TimeOfDay.ToString();

                //update type of alarm display
                string type = "";
                if ((alarm.alarmNotificationDaysMask & (1 << (int)DayOfWeek.Sunday)) != 0) type += "Su ";
                if ((alarm.alarmNotificationDaysMask & (1 << (int)DayOfWeek.Monday)) != 0) type += "M ";
                if ((alarm.alarmNotificationDaysMask & (1 << (int)DayOfWeek.Tuesday)) != 0) type += "Tu ";
                if ((alarm.alarmNotificationDaysMask & (1 << (int)DayOfWeek.Wednesday)) != 0) type += "W ";
                if ((alarm.alarmNotificationDaysMask & (1 << (int)DayOfWeek.Thursday)) != 0) type += "Th ";
                if ((alarm.alarmNotificationDaysMask & (1 << (int)DayOfWeek.Friday)) != 0) type += "F ";
                if ((alarm.alarmNotificationDaysMask & (1 << (int)DayOfWeek.Saturday)) != 0) type += "Sa ";
                AlarmType_label.Content = type;
            }
            else
            {
                AlarmTime_label.Content = alarm.defaultAlarmTime.ToString();
                AlarmType_label.Content = "No repeat";
            }

            //update the state of toggle button
            if (alarm.enabled) EnableAlarmToggleButton.IsChecked = true;
            ReminderLabel.Content = alarm.label;
        }

        /// <summary>
        /// Method to check if alarm should go off and activate notification if alarm is tripped. 
        /// </summary>
        /// <param name="currentTime"></param>
        internal void requestAlarmWithCheck(DateTime currentTime)
        {
            //DateTime clone = alarm.notifyTime.AddMinutes(5);
            //Console.WriteLine(1 << (int)currentTime.DayOfWeek);
            //Console.WriteLine(alarm.alarmNotificationDaysMask);
            if (alarm.enabled && alarm.notifyTime.CompareTo(MainWindow.currentTime) <= 0 && 
                alarm.notifyTime.AddMinutes(10).CompareTo(MainWindow.currentTime) >= 0
                && ((1<<(int)currentTime.DayOfWeek) & alarm.alarmNotificationDaysMask) != 0)
            {
                
                new NotificationWindow(this).ShowDialog();
            }
            else if(alarm.enabled && alarm.oneTimeAlarm && 
                alarm.notifyTime.AddMinutes(10).CompareTo(MainWindow.currentTime) >= 0 &&
                alarm.notifyTime.CompareTo(MainWindow.currentTime) <= 0)
            {
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

   
        /// <summary>
        /// Set alarm to enabled via toggle button state
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnableAlarmToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            alarm.enabled = true;
        }

        /// <summary>
        /// Set alarm to disabled via toggle button state
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnableAlarmToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            alarm.enabled = false;
        }
    }
}
