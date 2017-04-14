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
    /// The point of access to the mobile alarm app. Because we haven't figured out how to do page navigation well (in UWP),
    /// this class contains the main page of the application (where the user can view the time, view the list of set alarms,
    /// modify existing alarms, and create new alarms), the edit alarm page, and the alarm notification page (which is the
    /// page that pops up when an alarm goes off). 
    /// 
    /// This class is also responsible for handling persistent storage (loading and saving alarms to local storage), and
    /// handling alarm notification when the first alarm goes off (subsequent alarm notifications are handled by the 
    /// AlarmUserControl class because those notifications would only change the AlarmUserControlUI).
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public static DateTime currentTime;
        public static String[] AlarmSoundsList= new String[] {"Careless Whisper", "Missile Alert", "Fog Horn"};
        public DispatcherTimer dispatcherTimer = new DispatcherTimer();
       
        /// <summary>
        /// The name of the file which stores alarm information.
        /// </summary>
        public static string ALARMS_FILE = "alarms.txt";

        public MainPage()
        {
            InitializeComponent();
            alarmtoneSelector.ItemsSource = AlarmSoundsList;
            alarmtoneSelector.SelectedIndex = 0;
            
            currentTime = DateTime.Now;
            updateTimeDisplay(currentTime);
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1000);
            dispatcherTimer.Start();
        }

        /// <summary>
        /// A helper method for updating the current date and time displayed displayed in the main page GUI.
        /// </summary>
        /// <param name="time"> Current time </param>
        private void updateTimeDisplay(DateTime time)
        {
            HourText.Text = time.ToString("hh:mm");
            MinuteText.Text = time.ToString(":ss");
            AMPMText.Text = time.ToString("tt");
            DayDateText.Text = time.ToString("dddd, MMMM dd, yyyy");

            AlarmNotificationWindowTime.Text = time.ToString("hh:mm:ss tt");
            AlarmNotificationWindowDate.Text = time.ToString("dddd, MMMM dd, yyyy");
        }

        /// <summary>
        /// Updates the system every second.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DispatcherTimer_Tick(object sender, object e)
        {
            currentTime = DateTime.Now;
            updateTimeDisplay(currentTime);

            foreach (AlarmUserControl u in AlarmList_Panel.Children)
            {
                u.requestAlarmWithCheck(currentTime);
            }
        }

        /// Persistent Storage

        /// <summary>
        /// This method is called whenever the user navigates to this page so that the most up-to-date
        /// list of alarms will be loaded from persistent storage and displayed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void pageLoaded(Object sender, RoutedEventArgs e)
        {
            await loadAlarmsFromJSON();
            currentTime = DateTime.Now;
            foreach (AlarmUserControl u in AlarmList_Panel.Children)
            {
                if (u.alarm.currentState == AlarmState.FIRST_TO_GO_OFF)
                {
                    openAlarmNotificationWindow(u.alarm.label);
                    u.alarm.playAlarmSound();
                    AlarmsManager.IS_ALARM_NOTIFICATION_OPEN = true;
                }
            }
        }

        private async void pageUnloaded(object sender, RoutedEventArgs e)
        {
            await saveAlarmsToJSON(getAlarms());
        }

        /// <summary>
        /// Save the current list of alarms into the ALARMS_FILE.
        /// </summary>
        /// <param name="alarms"> List of alarms to be saved to ALARMS_FILE. </param>
        /// <returns></returns>
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

        /// <summary>
        /// Load Alarm data from ALARM_FILE and uses that data to intialize the StackPanel that displays the alarms.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Gets the list of currently set alarms.
        /// </summary>
        /// <returns>The list of alarms currently in the StackPanel which displays the list of alarms set by the user. </returns>
        public List<Alarm> getAlarms()
        {
            List<Alarm> ret = new List<Alarm>();
            foreach (AlarmUserControl u in AlarmList_Panel.Children)
            {
                if (u.alarm.initialized)
                    ret.Add(u.alarm);
            }
            return ret;
        }

        /// General user input handling
        private void AddAlarmButton_Click(object sender, RoutedEventArgs e)
        {
            AlarmUserControl alarmControl = new AlarmUserControl(this, new Alarm(AlarmsManager.SNOOZE_TIME));
            AlarmList_Panel.Children.Add(alarmControl);
        }

        private async void AlarmsSettingsButton_Clicked(object sender, RoutedEventArgs e)
        {
            await saveAlarmsToJSON(getAlarms());
            dispatcherTimer.Tick -= DispatcherTimer_Tick;
            Frame.Navigate(typeof(SettingsPage));
        }

        internal void openEditPage()
        {
            EditAlarmPage.Visibility = Visibility.Visible;
            AlarmLabelTextbox.Text = "";
        }

        //Edit Alarm Window
        private void DoneEditAlarmButtonClicked(object sender, RoutedEventArgs e)
        {
            foreach (AlarmUserControl u in AlarmList_Panel.Children)
            {
                if (u.currentState == State.EDIT)
                {
                    u.currentState = State.IDLE;
                    u.alarm.label = AlarmLabelTextbox.Text;
                    int index = alarmtoneSelector.SelectedIndex;
                    if (repeatCheckbox.IsChecked == true)
                    {
                        DateTime alarmTime = currentTime.Date.Date.Add(timePicker.Time);
                        int mask = 0;
                        if (CheckBox_Sun.IsChecked == true) mask |= (1 << (int)DayOfWeek.Sunday);
                        if (CheckBox_Mon.IsChecked == true) mask |= (1 << (int)DayOfWeek.Monday);
                        if (CheckBox_Tue.IsChecked == true) mask |= (1 << (int)DayOfWeek.Tuesday);
                        if (CheckBox_Wed.IsChecked == true) mask |= (1 << (int)DayOfWeek.Wednesday);
                        if (CheckBox_Thu.IsChecked == true) mask |= (1 << (int)DayOfWeek.Thursday);
                        if (CheckBox_Fri.IsChecked == true) mask |= (1 << (int)DayOfWeek.Friday);
                        if (CheckBox_Sat.IsChecked == true) mask |= (1 << (int)DayOfWeek.Saturday);
                        u.alarm.setRepeatingNotificationTime(mask, alarmTime);
                        u.updateDisplay();
                    }
                    else
                    {
                        DateTime alarmTime = datePicker.Date.Date.Add(timePicker.Time);
                        u.alarm.setOnetimeAlarm(alarmTime);
                        u.updateDisplay();
                    }
                    u.alarm.alarmToneIndex = index;
                }
            }
            EditAlarmPage.Visibility = Visibility.Collapsed;
        }

        private void CancelEditAlarmButtonClicked(object sender, RoutedEventArgs e)
        {
            foreach (AlarmUserControl u in AlarmList_Panel.Children)
            {
                if (u.currentState == State.EDIT) u.currentState = State.IDLE;
            }
            EditAlarmPage.Visibility = Visibility.Collapsed;
        }

        private void RepeatingAlarmCheckboxChecked(object sender, RoutedEventArgs e)
        {
            datePicker.Visibility = Visibility.Collapsed;
            repeatedAlarms.Visibility = Visibility.Visible;
        }

        private void RepeatingAlarmCheckboxUnchecked(object sender, RoutedEventArgs e)
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
            foreach (AlarmUserControl u in AlarmList_Panel.Children)
            {
                if (u.alarm.currentState.Equals(AlarmState.FIRST_TO_GO_OFF))
                {
                    u.updateAlarmTime();
                    try { u.alarm.mediaPlayer.Pause(); } catch (Exception exc) { }
                }
            }
            AlarmNotification.Visibility = Visibility.Collapsed;
            AlarmsManager.IS_ALARM_NOTIFICATION_OPEN = false;
        }

        private void SnoozeButtonClick(object sender, RoutedEventArgs e)
        {
            foreach (AlarmUserControl u in AlarmList_Panel.Children)
                if (u.alarm.currentState == AlarmState.FIRST_TO_GO_OFF)
                {
                    u.alarm.snooze();
                    try { u.alarm.mediaPlayer.Pause(); } catch (Exception exc) { }
                }
            AlarmNotification.Visibility = Visibility.Collapsed;
            AlarmsManager.IS_ALARM_NOTIFICATION_OPEN = false;
        }

        private void DailyCheckboxChecked(object sender, RoutedEventArgs e)
        {
            CheckBox_Sun.IsChecked = CheckBox_Mon.IsChecked = 
                CheckBox_Tue.IsChecked = CheckBox_Wed.IsChecked = CheckBox_Thu.IsChecked =
                CheckBox_Fri.IsChecked = CheckBox_Sat.IsChecked = true;
        }

        private void DailyCheckboxUnchecked(object sender, RoutedEventArgs e)
        {
            CheckBox_Sun.IsChecked = CheckBox_Mon.IsChecked =
                CheckBox_Tue.IsChecked = CheckBox_Wed.IsChecked = CheckBox_Thu.IsChecked =
                CheckBox_Fri.IsChecked = CheckBox_Sat.IsChecked = false;
        }
    }
}
