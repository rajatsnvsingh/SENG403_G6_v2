using SENG403_AlarmClock_V2;
using System;
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
        private Alarm alarm;

        public EditAlarmPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            alarm = e.Parameter as Alarm;
        }

        private void ClickDoneButton(object sender, RoutedEventArgs e)
        {
            TimeSpan ts = timePicker.Time;
            if (MondayButton.IsChecked == true) {
                alarm.setWeeklyAlarm(DayOfWeek.Monday, ts);
            }
            else if (TuesdayButton.IsChecked == true)
            {

            }
            else if (TuesdayButton.IsChecked == true)
            {

            }
            else if (DailyButton.IsChecked == true)
            {

            }

            Frame.Navigate(typeof(MainPage));
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
