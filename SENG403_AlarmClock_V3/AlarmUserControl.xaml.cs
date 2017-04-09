using System;
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
        public Alarm alarm { get; set; }

        public AlarmUserControl(StackPanel parent, Page page, Alarm alarm)
        {
            this.InitializeComponent();
            _parent = parent;
            _page = page;
            this.alarm = alarm;
        }

        private void EnableDisableAlarm_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditAlarm_Click(object sender, RoutedEventArgs e)
        {
            ((MainPage)_page).destroy();
            _page.Frame.Navigate(typeof(EditAlarmPage), new AlarmParameter(_page, alarm));
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

        internal void update()
        {
            if (alarm.repeatIntervalDays == 7)
            {
                if (alarm.defaultAlarmTime.DayOfWeek == DayOfWeek.Monday) AlarmType_label.Text = "Monday";
                if (alarm.defaultAlarmTime.DayOfWeek == DayOfWeek.Tuesday) AlarmType_label.Text = "Tuesday";
                if (alarm.defaultAlarmTime.DayOfWeek == DayOfWeek.Wednesday) AlarmType_label.Text = "Wednesday";
                if (alarm.defaultAlarmTime.DayOfWeek == DayOfWeek.Thursday) AlarmType_label.Text = "Thursday";
                if (alarm.defaultAlarmTime.DayOfWeek == DayOfWeek.Friday) AlarmType_label.Text = "Friday";
                if (alarm.defaultAlarmTime.DayOfWeek == DayOfWeek.Saturday) AlarmType_label.Text = "Saturday";
                if (alarm.defaultAlarmTime.DayOfWeek == DayOfWeek.Sunday) AlarmType_label.Text = "Sunday";
            }
            else if (alarm.repeatIntervalDays == 1)
            {
                AlarmType_label.Text = "Daily";
            }
            else if (alarm.repeatIntervalDays == -1)
            {
                AlarmType_label.Text = alarm.defaultAlarmTime.Date.ToString();
            }
            else
            {
                throw new NotSupportedException();
            }
            AlarmTime_label.Text = alarm.defaultAlarmTime.TimeOfDay.ToString();
        }
    }

    internal class AlarmParameter
    {
        public Alarm alarm;
        public Page _page;

        public AlarmParameter(Page _page, Alarm alarm)
        {
            this._page = _page;
            this.alarm = alarm;
        }
    }
}
