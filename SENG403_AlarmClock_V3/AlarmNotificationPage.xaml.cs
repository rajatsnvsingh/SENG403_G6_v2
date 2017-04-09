using SENG403_AlarmClock_V2;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SENG403_AlarmClock_V3
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AlarmNotificationPage : Page
    {
        private Alarm alarm;

        public AlarmNotificationPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            alarm = e.Parameter as Alarm;
        }

        private void DismissButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            alarm.update();
            Frame.Navigate(typeof(MainPage));
        }

        private void SnoozeButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            alarm.snooze();
            Frame.Navigate(typeof(MainPage));
        }
    }
}
