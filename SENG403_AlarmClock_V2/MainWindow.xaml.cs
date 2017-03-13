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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double snoozeTime = 5;
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        
        
        public MainWindow()
        {
            InitializeComponent();
            hourLabel.Content = DateTime.Now.ToString("hh : mm");
            minuteLabel.Content = DateTime.Now.ToString(": ss");
            AMPM_thing.Content = DateTime.Now.ToString("tt");
            DayDate.Content = DateTime.Now.ToString("dddd, MMMM dd, yyyy");
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            hourLabel.Content = DateTime.Now.ToString("hh : mm");
            minuteLabel.Content = DateTime.Now.ToString(": ss");
            AMPM_thing.Content = DateTime.Now.ToString("tt");
            DayDate.Content = DateTime.Now.ToString("dddd, MMMM dd, yyyy");
            foreach (AlarmUserControl u in AlarmList_Panel.Children)
            {
                if (DateTime.Now.CompareTo(u.alarm.GetTime()) > 0)
                {
                    u.alarm.play();
                }
            }
           
        }

        /// <summary>
        /// Opens Window for New Alarm settings, and adds new alarm UserControl to StackPanel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddAlarmButton_Click(object sender, RoutedEventArgs e)
        {
            Alarm newAlarm = new Alarm("pack://application:,,,/Sounds/missileAlert.wav", snoozeTime);
            AlarmList_Panel.Children.Add(new AlarmUserControl(AlarmList_Panel, newAlarm));

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {

            new GlobalSettings((int)snoozeTime).ShowDialog();
        }

        private void AlarmSideBarButton_Click(object sender, RoutedEventArgs e)
        {
            if (AlarmList_Panel.Visibility == Visibility.Visible) {
                AlarmList_Panel.Visibility = Visibility.Collapsed;
            }
            else
            {
                AlarmList_Panel.Visibility = Visibility.Visible;
            }
        }
    }
}
