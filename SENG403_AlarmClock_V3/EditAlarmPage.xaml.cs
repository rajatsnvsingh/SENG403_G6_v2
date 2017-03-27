using SENG403_AlarmClock_V2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SENG403_AlarmClock_V3
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EditAlarmPage : Page
    {
        private MainPage mainPage;
        private Alarm alarm;

        public EditAlarmPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            AlarmParameter param = e.Parameter as AlarmParameter;
            mainPage = (MainPage)param._page;
            alarm = param.alarm;
        }

        private async void ClickDoneButton(object sender, RoutedEventArgs e)
        {
            TimeSpan ts = timePicker.Time;
            if (MondayButton.IsChecked == true) {
                alarm.setWeeklyAlarm(DayOfWeek.Monday, ts);
            }
            else if (TuesdayButton.IsChecked == true)
            {
                alarm.setWeeklyAlarm(DayOfWeek.Tuesday, ts);
            }
            else if (WednesdayButton.IsChecked == true)
            {
                alarm.setWeeklyAlarm(DayOfWeek.Wednesday, ts);
            }
            else if (DailyButton.IsChecked == true)
            {
                alarm.setDailyAlarm(ts);
            }
            else if (!repeatCheckbox.IsChecked == true)
            {
                
            } 
            List<Alarm> temp = mainPage.getAlarms();
            List<Alarm> alarms = new List<Alarm>();
            foreach (Alarm a in temp)
            {
                if (a == alarm) alarms.Add(alarm);
                else alarms.Add(a);
            }
            await saveAlarmsToJSON(alarms);
            Frame.Navigate(typeof(MainPage));
        }

        private async Task saveAlarmsToJSON(List<Alarm> alarms)
        {
            var serializer = new DataContractJsonSerializer(typeof(List<Alarm>));
            using (var stream = await ApplicationData.Current.LocalFolder.OpenStreamForWriteAsync(
                          MainPage.ALARMS_FILE,
                          CreationCollisionOption.ReplaceExisting))
            {
                serializer.WriteObject(stream, alarms);
            }
        }
        
        private void ClickRepeat(object sender, RoutedEventArgs e)
        {
            if (repeatCheckbox.IsChecked == true)
            {
                MondayButton.Visibility = Visibility.Visible;
                TuesdayButton.Visibility = Visibility.Visible;
                WednesdayButton.Visibility = Visibility.Visible;
                DailyButton.Visibility = Visibility.Visible;
            } else
            {
                MondayButton.Visibility = Visibility.Collapsed;
                TuesdayButton.Visibility = Visibility.Collapsed;
                WednesdayButton.Visibility = Visibility.Collapsed;
                DailyButton.Visibility = Visibility.Collapsed;
            }
        }
    }
}
