using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SENG403_AlarmClock_V2
{
    /// <summary>
    /// Interaction logic for AlarmUserControl.xaml
    /// </summary>
    public partial class AlarmUserControl : UserControl
    {

        private Panel _parent;
        public Alarm alarm;
        public AlarmUserControl(StackPanel parent, Alarm alarm)
        {
            InitializeComponent();
            _parent = parent;
            this.alarm = alarm;
            AlarmTime_label.Content = alarm.GetTime().ToString("hh:mm tt");
        }

        private void EnableDisableRadioButton_Checked(object sender, RoutedEventArgs e)
        {
           
        }

        private void EnableDisableAlarm_Button_Click(object sender, RoutedEventArgs e)
        {
            if (EnableDisableAlarm_Button.Content.Equals("Enable"))
            {
                EnableDisableAlarm_Button.Background = new SolidColorBrush(Colors.Red);
                EnableDisableAlarm_Button.Content = "Disable";
                alarm.disable();

            }
            else if (EnableDisableAlarm_Button.Content.Equals("Disable"))
            {
                EnableDisableAlarm_Button.Background = new SolidColorBrush(Colors.Green);
                EnableDisableAlarm_Button.Content = "Enable";
                alarm.enable();

            }
        }

        private void DeleteAlarm_Click(object sender, RoutedEventArgs e)
        {
            foreach (AlarmUserControl u in _parent.Children)
                if (this.Equals(u))
                {
                    _parent.Children.Remove(this);
                    break;
                }
        }

        private void EditAlarm_Click(object sender, RoutedEventArgs e)
        {
            new NewAlarmWindow(this,alarm).ShowDialog();
        }

        public void refresh()
        {
            AlarmTime_label.Content = alarm.GetTime().ToString("hh:mm tt");
        }
    }
}
