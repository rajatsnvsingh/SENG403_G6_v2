using SENG403_AlarmClock_V2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SENG403_AlarmClock_V3
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public static DateTime currentTime = DateTime.Now;
        public static double snoozeTime = 1.0;

        //prevent timer task from being dispatched twice
        //TODO: fix this hack
        private static bool firstTime = true;
        public DispatcherTimer dispatcherTimer = new DispatcherTimer();

        //constants
        public static string DEFAULT_ALARM_SOUND = "";
        public static string ALARMS_FILE = "alarms.txt";

        //debug
        private int init_count = 0;

        public MainPage()
        {
            InitializeComponent();
            currentTime = DateTime.Now;
            HourText.Text = currentTime.ToString("hh:mm");
            MinuteText.Text = currentTime.ToString(":ss");
            AMPMText.Text = currentTime.ToString("tt");
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1000);
            dispatcherTimer.Start();
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

                DebugText.Text = alarms.Count.ToString();

                foreach (var a in alarms)
                {
                    AlarmUserControl alarmDisplay = new AlarmUserControl(AlarmList_Panel, this, a);
                    alarmDisplay.update();
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
            currentTime = currentTime.AddSeconds(1);
            HourText.Text = currentTime.ToString("hh:mm");
            MinuteText.Text = currentTime.ToString(":ss");
            AMPMText.Text = currentTime.ToString("tt");
            DayDateText.Text = currentTime.ToString("dddd, MMMM dd, yyyy");
        }

        private void PhoneAddAlarmButton_Click(object sender, RoutedEventArgs e)
        {
            AlarmUserControl alarmControl = new AlarmUserControl(AlarmList_Panel, this, new Alarm(DEFAULT_ALARM_SOUND, snoozeTime));
            AlarmList_Panel.Children.Add(alarmControl);
        }

        private void Settings_Clicked(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SettingsPage));
        }

        private async void SideBarButtonClick(object sender, RoutedEventArgs e)
        {
            await loadAlarmsFromJSON();
            DebugText.Text = init_count.ToString();
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
    }
}
