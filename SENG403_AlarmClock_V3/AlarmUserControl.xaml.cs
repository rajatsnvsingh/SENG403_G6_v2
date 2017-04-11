using System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace SENG403_AlarmClock_V3
{
    public enum State { IDLE, EDIT, NOTIFY };

    public sealed partial class AlarmUserControl : UserControl
    {
        private MainPage mainPage;
        public Alarm alarm { get; set; }
        public State currentState = State.IDLE;

        public AlarmUserControl(MainPage page, Alarm alarm)
        {
            this.InitializeComponent();
            mainPage = page;
            this.alarm = alarm;
        }

        private void EnableDisableAlarm_Button_Click(object sender, RoutedEventArgs e)
        {
            if (!alarm.initialized) return;
            if (EnableDisableAlarm_Button.Content.Equals("Enable"))
                enable();
            else
                disable();
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

        internal void updateDisplay()
        {
            if (!alarm.initialized)
            {
                AlarmTypeLabel.Text = "";
                AlarmTimeLabel.Text = "Alarm Not Set";
                return;
            }
            if (alarm.repeatIntervalDays == 7)
            {
                if (alarm.defaultAlarmTime.DayOfWeek == DayOfWeek.Monday) AlarmTypeLabel.Text = "Monday";
                if (alarm.defaultAlarmTime.DayOfWeek == DayOfWeek.Tuesday) AlarmTypeLabel.Text = "Tuesday";
                if (alarm.defaultAlarmTime.DayOfWeek == DayOfWeek.Wednesday) AlarmTypeLabel.Text = "Wednesday";
                if (alarm.defaultAlarmTime.DayOfWeek == DayOfWeek.Thursday) AlarmTypeLabel.Text = "Thursday";
                if (alarm.defaultAlarmTime.DayOfWeek == DayOfWeek.Friday) AlarmTypeLabel.Text = "Friday";
                if (alarm.defaultAlarmTime.DayOfWeek == DayOfWeek.Saturday) AlarmTypeLabel.Text = "Saturday";
                if (alarm.defaultAlarmTime.DayOfWeek == DayOfWeek.Sunday) AlarmTypeLabel.Text = "Sunday";
                AlarmTimeLabel.Text = alarm.defaultAlarmTime.TimeOfDay.ToString();
            }
            else if (alarm.repeatIntervalDays == 1)
            {
                AlarmTypeLabel.Text = "Daily";
                AlarmTimeLabel.Text = alarm.defaultAlarmTime.TimeOfDay.ToString();
            }
            else if (alarm.repeatIntervalDays == -1)
            {
                AlarmTypeLabel.Text = "No Repeat";
                AlarmTimeLabel.Text = alarm.defaultAlarmTime.ToString();
            }
            AlarmLabel.Text = alarm.label;
        }

        internal void setOneTimeAlarm(DateTime dateTime, string label)
        {
            alarm.setOneTimeAlarm(dateTime);
            if (label.Equals("")) alarm.label = "Unlabelled alarm";
            else alarm.label = label;
            updateDisplay();
        }

        internal void setWeeklyAlarm(DayOfWeek day, TimeSpan ts, string label)
        {
            alarm.setWeeklyAlarm(day, ts);
            if (label.Equals("")) alarm.label = "Unlabelled alarm";
            else alarm.label = label;
            updateDisplay();
        }

        internal void setDailyAlarm(TimeSpan ts, string label)
        {
            alarm.setDailyAlarm(ts);
            if (label.Equals("")) alarm.label = "Unlabelled alarm";
            else alarm.label = label;
            updateDisplay();
        }

        internal void requestAlarmWithCheck(DateTime currentTime)
        {
            if (!alarm.enabled || alarm.currentState != AlarmState.NONE) return;
            if (alarm.notifyTime.CompareTo(currentTime) <= 0)
            {
                if (!MainPage.ALARM_NOTIFICATION_OPEN)
                {
                    alarm.playAlarmSound();
                    MainPage.ALARM_NOTIFICATION_OPEN = true;
                    mainPage.openAlarmNotificationWindow(alarm.label);
                    alarm.currentState = AlarmState.FIRST_TO_GO_OFF;
                }
                else
                {
                    EnableDisableAlarm_Button.Visibility = Visibility.Collapsed;
                    EditAlarm_Button.Visibility = Visibility.Collapsed;
                    DismissAlarmButton.Visibility = Visibility.Visible;
                    SnoozeAlarmButton.Visibility = Visibility.Visible;
                    alarm.currentState = AlarmState.SIDE_NOTIFICATION;
                }
            }
        }

        public void showWarningMessage()
        {
            WarningMessage.Visibility = Visibility.Visible;
        }

        private void DismissAlarmButtonClick(object sender, RoutedEventArgs e)
        {
            EnableDisableAlarm_Button.Visibility = Visibility.Visible;
            EditAlarm_Button.Visibility = Visibility.Visible;
            DismissAlarmButton.Visibility = Visibility.Collapsed;
            SnoozeAlarmButton.Visibility = Visibility.Collapsed;
            WarningMessage.Visibility = Visibility.Collapsed;
            alarm.updateAlarmTime();
        }

        private void SnoozeButtonClick(object sender, RoutedEventArgs e)
        {
            EnableDisableAlarm_Button.Visibility = Visibility.Visible;
            EditAlarm_Button.Visibility = Visibility.Visible;
            DismissAlarmButton.Visibility = Visibility.Collapsed;
            SnoozeAlarmButton.Visibility = Visibility.Collapsed;
            WarningMessage.Visibility = Visibility.Collapsed;
            alarm.snooze();
        }

        internal void disable()
        {
            alarm.enabled = false;
            EnableDisableAlarm_Button.Content = "Enable";
        }

        internal void enable()
        {
            alarm.enabled = true;
            EnableDisableAlarm_Button.Content = "Disable";
        }
    }
}
