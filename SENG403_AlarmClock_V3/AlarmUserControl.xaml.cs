using System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace SENG403_AlarmClock_V3
{
    /// <summary>
    /// State of the AlarmUserControl
    /// TODO: get rid of these states
    /// </summary>
    public enum State { IDLE, EDIT, NOTIFY };

    /// <summary>
    /// This class handles the logic associated with its alarm (such as checking whether that alarm should go off and
    /// whether that alarm should display a notification) as well as event handling and data updating for its alarm.
    /// </summary>
    public sealed partial class AlarmUserControl : UserControl
    {
        /// <summary>
        /// The page which is the parent page of AlarmUserControl. This is required so that the AlarmUserControl can
        /// request UI change on the main page.
        /// </summary>
        private MainPage mainPage;

        public Alarm alarm { get; set; }

        public State currentState = State.IDLE;
        private Color WARNING_COLOR = Color.FromArgb(0xFF, 255, 81, 81);
        private Color DEFAULT_COLOR = Color.FromArgb(0xFF, 0xc6, 0xc6, 0xc6);


        public AlarmUserControl(MainPage page, Alarm alarm)
        {
            this.InitializeComponent();
            mainPage = page;
            this.alarm = alarm;
        }

        private void EditAlarm_Click(object sender, RoutedEventArgs e)
        {
            currentState = State.EDIT;
            mainPage.openEditPage();
        }

        private void DeleteAlarm_Click(object sender, RoutedEventArgs e)
        {
            StackPanel parent = (StackPanel)this.Parent;
            foreach(AlarmUserControl alarm in parent.Children)
            {
                if(this.Equals(alarm))
                {
                    parent.Children.Remove(this);
                    break;
                }
            }
        }

        /// <summary>
        /// Update the display of the alarm label to match alarm info.
        /// </summary>
        internal void updateDisplay()
        {
            if (!alarm.initialized)
            {
                AlarmTypeLabel.Text = "";
                AlarmTimeLabel.Text = "Alarm Not Set";
                return;
            }
            if (alarm.enabled)
                AlarmEnabledToggle.IsOn = true;
            if (alarm.alarmNotificationDaysMask == (1 << 7) - 1)
            {
                AlarmTypeLabel.Text = "Daily";
                AlarmTimeLabel.Text = alarm.defaultNotificationTime.TimeOfDay.ToString();
            } else if (alarm.alarmNotificationDaysMask == 0)
            {
                AlarmTypeLabel.Text = "No repeat";
                AlarmTimeLabel.Text = alarm.defaultNotificationTime.ToString();
            } else {
                string type = "";
                if ((alarm.alarmNotificationDaysMask & (1 << (int)DayOfWeek.Sunday)) != 0) type += "Su ";
                if ((alarm.alarmNotificationDaysMask & (1 << (int)DayOfWeek.Monday)) != 0) type += "M ";
                if ((alarm.alarmNotificationDaysMask & (1 << (int)DayOfWeek.Tuesday)) != 0) type += "Tu ";
                if ((alarm.alarmNotificationDaysMask & (1 << (int)DayOfWeek.Wednesday)) != 0) type += "W ";
                if ((alarm.alarmNotificationDaysMask & (1 << (int)DayOfWeek.Thursday)) != 0) type += "Th ";
                if ((alarm.alarmNotificationDaysMask & (1 << (int)DayOfWeek.Friday)) != 0) type += "F ";
                if ((alarm.alarmNotificationDaysMask & (1 << (int)DayOfWeek.Saturday)) != 0) type += "Sa ";
                AlarmTypeLabel.Text = type;
                AlarmTimeLabel.Text = alarm.defaultNotificationTime.TimeOfDay.ToString();
            }
            if (!String.IsNullOrEmpty(alarm.label))
                AlarmLabel.Text = alarm.label;
        }

        /// <summary>
        /// This method handles checking whether an alarm should go off and updating the UI if the alarm should go off.
        /// It listens to the timer belonging to the MainPage class.
        /// </summary>
        /// <param name="currentTime"></param>
        internal void requestAlarmWithCheck(DateTime currentTime)
        {
            if (!alarm.initialized || !alarm.enabled || alarm.currentState != AlarmState.IDLE) return;
            if (alarm.currentNotificationTime.CompareTo(currentTime) <= 0)
            {
                if (!AlarmsManager.IS_ALARM_NOTIFICATION_OPEN)
                {
                    alarm.playAlarmSound();
                    AlarmsManager.IS_ALARM_NOTIFICATION_OPEN = true;
                    mainPage.openAlarmNotificationWindow(alarm.label);
                    alarm.currentState = AlarmState.FIRST_TO_GO_OFF;
                }
                else
                {
                    bg.Background = new SolidColorBrush(WARNING_COLOR);
                    EditAlarm_Button.Visibility = Visibility.Collapsed;
                    AlarmEnabledToggle.Visibility = Visibility.Collapsed;
                    DismissAlarmButton.Visibility = Visibility.Visible;
                    SnoozeAlarmButton.Visibility = Visibility.Visible;
                    alarm.currentState = AlarmState.SIDE_NOTIFICATION;
                }
            }
        }

        private void DismissAlarmButtonClick(object sender, RoutedEventArgs e)
        {
            alarm.updateAlarmTime();
            if (alarm.oneTimeAlarm) AlarmEnabledToggle.IsOn = false;
            bg.Background = new SolidColorBrush(DEFAULT_COLOR);
            DismissAlarmButton.Visibility = Visibility.Collapsed;
            SnoozeAlarmButton.Visibility = Visibility.Collapsed;
            EditAlarm_Button.Visibility = Visibility.Visible;
            AlarmEnabledToggle.Visibility = Visibility.Visible;
        }

        private void SnoozeAlarmButtonClick(object sender, RoutedEventArgs e)
        {
            EditAlarm_Button.Visibility = Visibility.Visible;
            AlarmEnabledToggle.Visibility = Visibility.Visible;
            bg.Background = new SolidColorBrush(DEFAULT_COLOR);
            DismissAlarmButton.Visibility = Visibility.Collapsed;
            SnoozeAlarmButton.Visibility = Visibility.Collapsed;
            WarningMessage.Visibility = Visibility.Collapsed;
            alarm.snooze();
        }

        /// <summary>
        /// Disable the alarm associated with this AlarmUserControl and updating the UI accordingly
        /// </summary>
        internal void disable()
        {
            alarm.enabled = false;
            AlarmEnabledToggle.IsOn = false;
        }

        /// <summary>
        /// Enable the alarm associated with this AlarmUserControl and updating the UI accordingly
        /// </summary>
        internal void enable()
        {
            alarm.enabled = true;
            AlarmEnabledToggle.IsOn = true;
        }

        internal void updateAlarmTime()
        {
            if (alarm.oneTimeAlarm)
                AlarmEnabledToggle.IsOn = false;
            else
                alarm.updateAlarmTime();
        }

        private void AlarmEnableToggled(object sender, RoutedEventArgs e)
        {
            if (AlarmEnabledToggle.IsOn)
                alarm.enabled = true;
            else
                alarm.enabled = false;
        }
    }
}
