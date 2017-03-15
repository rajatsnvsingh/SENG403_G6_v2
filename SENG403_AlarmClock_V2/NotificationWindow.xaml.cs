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

        private Alarm alarm;

        public NotificationWindow(Alarm alarm)
        {
            this.alarm = alarm;
            InitializeComponent();
        }

        private void dismissButton_Click(object sender, RoutedEventArgs e)
        {
            alarm.update();
            Close();
        }

        private void snoozeButton_Click(object sender, RoutedEventArgs e)
        {
            alarm.snooze();
            Close();
        }
    }
}
