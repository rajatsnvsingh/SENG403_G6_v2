using SENG403_AlarmClock_V2;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace SENG403_AlarmClock_V3
{
    public sealed partial class AlarmUserControl : UserControl
    {
        private StackPanel _parent;
        private Page _page;
        private Alarm _alarm;

        public AlarmUserControl(StackPanel parent, Page page, Alarm alarm)
        {
            this.InitializeComponent();
            _parent = parent;
            _page = page;
            _alarm = alarm;
        }

        private void EnableDisableAlarm_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditAlarm_Click(object sender, RoutedEventArgs e)
        {
            _page.Frame.Navigate(typeof(EditAlarmPage), _alarm);
        }

        private void DeleteAlarm_Click(object sender, RoutedEventArgs e)
        {
            foreach(AlarmUserControl alarm in _parent.Children)
            {
                if(this.Equals(alarm))
                {
                    _parent.Children.Remove(this);
                    break;
                }
            }
        }
    }
}
