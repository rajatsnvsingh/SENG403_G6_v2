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
using System.Windows.Shapes;

namespace SENG403_AlarmClock_V2
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary> 

    public partial class NotificationWindow : Window
    {

        private AlarmUserControl alarmControl;

        public NotificationWindow(AlarmUserControl alarm)
        {
            InitializeComponent();
            alarmControl = alarm;
            if (!alarmControl.alarm.label.Equals(""))
                AlarmLabel.Content = alarmControl.alarm.label;
        }

        private void dismissButton_Click(object sender, RoutedEventArgs e)
        {
            alarmControl.dismissAlarm();
            Close();
        }

        private void snoozeButton_Click(object sender, RoutedEventArgs e)
        {
            alarmControl.snoozeAlarm();
            Close();
        }
        private void Global_Mice(object sender, MouseButtonEventArgs e)
        {

            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }
    }
}
