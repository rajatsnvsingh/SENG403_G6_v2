using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SENG403_AlarmClock_V3
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public static DateTime currentTime;
        public static double snoozeTime = 1.0;

        public DispatcherTimer dispatcherTimer = new DispatcherTimer();

        //constants
        public static string DEFAULT_ALARM_SOUND = "";
        public static string ALARMS_FILE = "alarms.txt";
        public static bool ALARM_NOTIFICATION_OPEN = false;

        public MainPage()
        {
            InitializeComponent();
            currentTime = DateTime.Now;
            updateTimeDisplay(currentTime);
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1000);
            dispatcherTimer.Start();
        }

        private void updateTimeDisplay(DateTime time)
        {
            HourText.Text = time.ToString("hh:mm");
            MinuteText.Text = time.ToString(":ss");
            AMPMText.Text = time.ToString("tt");
            DayDateText.Text = time.ToString("dddd, MMMM dd, yyyy");

            AlarmNotificationWindowTime.Text = time.ToString("hh:mm:ss tt");
            AlarmNotificationWindowDate.Text = time.ToString("dddd, MMMM dd, yyyy");
        }

        public async void pageLoaded(Object sender, RoutedEventArgs e)
        {
            await loadAlarmsFromJSON();
            currentTime = DateTime.Now;
            foreach (AlarmUserControl u in AlarmList_Panel.Children)
            {
                if (u.alarm.currentState == AlarmState.FIRST_TO_GO_OFF)
                {
                    openAlarmNotificationWindow(u.alarm.label);
                    ALARM_NOTIFICATION_OPEN = true;
                }
            }
        }

        public async Task saveAlarmsToJSON(List<Alarm> alarms)
        {
            var serializer = new DataContractJsonSerializer(typeof(List<Alarm>));
            using (var stream = await ApplicationData.Current.LocalFolder.OpenStreamForWriteAsync(
                          ALARMS_FILE,
                          CreationCollisionOption.ReplaceExisting))
            {
                serializer.WriteObject(stream, alarms);
            }
        }

        public List<Alarm> getAlarms()
        {
            List<Alarm> ret = new List<Alarm>();
            foreach (AlarmUserControl u in AlarmList_Panel.Children)
            {
                ret.Add(u.alarm);
            }
            return ret;
        }

        private async Task loadAlarmsFromJSON()
        {
            List<Alarm> alarms;
            var jsonSerializer = new DataContractJsonSerializer(typeof(List<Alarm>));
            try
            {
                var myStream = await ApplicationData.Current.LocalFolder.OpenStreamForReadAsync(ALARMS_FILE);

                alarms = (List<Alarm>)jsonSerializer.ReadObject(myStream);

                foreach (var a in alarms)
                {
                    AlarmUserControl alarmDisplay = new AlarmUserControl(this, a);
                    alarmDisplay.updateDisplay();
                    AlarmList_Panel.Children.Add(alarmDisplay);
                }
            }
            catch (Exception)
            {
                //Do nothing, file doesn't exist
            }
        }

        private void DispatcherTimer_Tick(object sender, object e)
        {
            currentTime = DateTime.Now;
            updateTimeDisplay(currentTime);

            foreach (AlarmUserControl u in AlarmList_Panel.Children)
            {
                u.requestAlarmWithCheck(currentTime);
            }
        }

        private void PhoneAddAlarmButton_Click(object sender, RoutedEventArgs e)
        {
            AlarmUserControl alarmControl = new AlarmUserControl(this, new Alarm(DEFAULT_ALARM_SOUND, snoozeTime));
            AlarmList_Panel.Children.Add(alarmControl);
        }

        private async void Settings_Clicked(object sender, RoutedEventArgs e)
        {
            await saveAlarmsToJSON(getAlarms());
            dispatcherTimer.Tick -= DispatcherTimer_Tick;
            Frame.Navigate(typeof(SettingsPage));
        }

        private void SideBarButtonClick(object sender, RoutedEventArgs e)
        {
            /*
            if(AlarmList_Panel.Visibility ==  Visibility.Visible)
            {
                TimeDateGrid.Margin = new Thickness(40, 208, 58, 300);
                AlarmList_Panel.Visibility = Visibility.Collapsed;
            }
            else if(AlarmList_Panel.Visibility == Visibility.Collapsed)
            {
                TimeDateGrid.Margin = new Thickness(40, 90, 58, 420);
                AlarmList_Panel.Visibility = Visibility.Visible;
            }
            */
        }

        internal void destroy()
        {
            dispatcherTimer.Tick -= DispatcherTimer_Tick;
        }

        private async void pageUnloaded(object sender, RoutedEventArgs e)
        {
            await saveAlarmsToJSON(getAlarms());
        }

        //Edit Alarm Window
        internal void openEditPage()
        {
            EditAlarmPage.Visibility = Visibility.Visible;
        }

        private void DoneButtonClicked(object sender, RoutedEventArgs e)
        {
            foreach (AlarmUserControl u in AlarmList_Panel.Children)
            {
                if (u.currentState == State.EDIT)
                {
                    u.currentState = State.IDLE;
                    TimeSpan ts = timePicker.Time;
                    string label = AlarmLabelTextbox.Text;
                    if (!repeatCheckbox.IsChecked == true)
                    {
                        u.setOneTimeAlarm(DateTime.Today.Add(ts), label);
                    }
                    else if (Monday.IsChecked == true)
                    {
                        u.setWeeklyAlarm(DayOfWeek.Monday, ts, label);
                    }
                    else if (Tuesday.IsChecked == true)
                    {
                        u.setWeeklyAlarm(DayOfWeek.Tuesday, ts, label);
                    }
                    else if (Wednesday.IsChecked == true)
                    {
                        u.setWeeklyAlarm(DayOfWeek.Wednesday, ts, label);
                    }
                    else if (Daily.IsChecked == true)
                    {
                        u.setDailyAlarm(ts, label);
                    }
                }
            }
            EditAlarmPage.Visibility = Visibility.Collapsed;
        }

        private void CancelButtonClicked(object sender, RoutedEventArgs e)
        {
            foreach (AlarmUserControl u in AlarmList_Panel.Children)
            {
                if (u.currentState == State.EDIT) u.currentState = State.IDLE;
            }
            EditAlarmPage.Visibility = Visibility.Collapsed;
        }

        private void repeatCheckboxChecked(object sender, RoutedEventArgs e)
        {
            datePicker.Visibility = Visibility.Collapsed;
            repeatedAlarms.Visibility = Visibility.Visible;
        }

        private void repeatCheckboxUnchecked(object sender, RoutedEventArgs e)
        {
            datePicker.Visibility = Visibility.Visible;
            repeatedAlarms.Visibility = Visibility.Collapsed;
        }

        // Alarm Notification Window
        internal void openAlarmNotificationWindow(string text)
        {
            AlarmLabel.Text = text;
            AlarmNotifyMessage.Text = "An alarm has gone off at " + currentTime.ToString("hh:mm:ss tt");
            AlarmNotification.Visibility = Visibility.Visible;
        }

        private void DismissButtonClick(object sender, RoutedEventArgs e)
        {
            if (!ALARM_NOTIFICATION_OPEN)
                throw new Exception("Alarm notification window was not open but dismiss button somehow got clicked on");
            foreach (AlarmUserControl u in AlarmList_Panel.Children)
            {
                if (u.alarm.currentState.Equals(AlarmState.FIRST_TO_GO_OFF))
                    u.alarm.updateAlarmTime();
            }
            AlarmNotification.Visibility = Visibility.Collapsed;
            MainPage.ALARM_NOTIFICATION_OPEN = false;
        }

        private void SnoozeButtonClick(object sender, RoutedEventArgs e)
        {
            if (!ALARM_NOTIFICATION_OPEN)
                throw new Exception("Alarm notification window was not open but snooze button somehow got clicked on");
            foreach (AlarmUserControl u in AlarmList_Panel.Children)
                if (u.alarm.currentState == AlarmState.FIRST_TO_GO_OFF)
                    u.alarm.snooze();
            AlarmNotification.Visibility = Visibility.Collapsed;
            MainPage.ALARM_NOTIFICATION_OPEN = false;
        }

    }
}
