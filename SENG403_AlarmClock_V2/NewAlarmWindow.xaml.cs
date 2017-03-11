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
    /// Interaction logic for NewAlarmWindow.xaml
    /// </summary>
    public partial class NewAlarmWindow : Window
    {
        public NewAlarmWindow()
        {
            InitializeComponent();
            RadioGrid.Visibility = Visibility.Collapsed;
            OtherProps.Margin = new Thickness(26, 149, 21, 34);

        }

        private void Done_Click(object sender, RoutedEventArgs e)
        {
            
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void radioButton_Thu_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void repeat_checkBox_Checked(object sender, RoutedEventArgs e)
        {
            RadioGrid.Visibility = Visibility.Visible;
            OtherProps.Margin = new Thickness(26, 230, 21, 34);
        }

        private void Checkbox_Unchecked(object sender, RoutedEventArgs e)
        {

            RadioGrid.Visibility = Visibility.Collapsed;
            OtherProps.Margin = new Thickness(26, 149, 21, 34);
        }
    }
}
