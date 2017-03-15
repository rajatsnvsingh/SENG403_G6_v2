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
    /// Interaction logic for GlobalSettings.xaml
    /// </summary>
    public partial class GlobalSettings : Window
    {
        MainWindow mainWindow;
        public GlobalSettings(MainWindow parent)
        {
            InitializeComponent();
            mainWindow = parent;
            Snooze_Selector.DefaultValue = (int)mainWindow.GetSnoozeTime();
        }

        private void Done_Click(object sender, RoutedEventArgs e)
        {
            foreach (AlarmUserControl u in mainWindow.AlarmList_Panel.Children)
            {
                u.alarm.setSnooze((Double)Snooze_Selector.Value);
            }
            MainWindow.snoozeTime = (Double)Snooze_Selector.Value;
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Global_Mice(object sender, MouseButtonEventArgs e)
        {

            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }
    }
}
