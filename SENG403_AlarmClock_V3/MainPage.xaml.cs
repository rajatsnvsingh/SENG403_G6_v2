using SENG403_AlarmClock_V2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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

        private DispatcherTimer dispatcherTimer = new DispatcherTimer();
        private string defaultAlarmSound = "";

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
            TimeDateGrid.Margin = new Thickness(40, 90, 58, 420);
            AlarmUserControl alarmControl = new AlarmUserControl(AlarmList_Panel, this, new Alarm(defaultAlarmSound, snoozeTime));
            AlarmList_Panel.Children.Add(alarmControl);
            //DebugText.Text = AlarmList_Panel.Children.Count.ToString();
        }

        private void PhoneAlarmSideBarButton_Click(object sender, RoutedEventArgs e)
        {
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
        }

        private void Settings_Clicked(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SettingsPage));
        }

    }
}
