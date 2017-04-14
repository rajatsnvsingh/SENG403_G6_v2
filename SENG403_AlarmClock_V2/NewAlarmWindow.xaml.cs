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
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SENG403_AlarmClock_V2
{
    /// <summary>
    /// Interaction logic for NewAlarmWindow.xaml
    /// </summary>
    public partial class NewAlarmWindow : Window
    {
        private List<String> alarmSounds = new List<string>();
        private AlarmUserControl alarmControl;
        private BinaryFormatter formatter;

        /// <summary>
        /// Initializes edit alarm window with alarm data
        /// </summary>
        public NewAlarmWindow(AlarmUserControl alarmControl)
        {
            InitializeComponent();
            RadioGrid.Visibility = Visibility.Collapsed;
            OtherProps.Margin = new Thickness(26, 149, 21, 34);
            this.alarmControl = alarmControl;
            alarmSounds.Add(@"..\..\Sounds\missileAlert.wav");
            alarmSounds.Add(@"..\..\Sounds\fogHorn.wav");
            AlarmTone_comboBox.ItemsSource = alarmSounds;
            AlarmTone_comboBox.SelectedIndex = 0;
            Alarm_TimePicker.Value = alarmControl.alarm.notifyTime;
            AlarmMessage.Text = alarmControl.alarm.GetLabel();

            
            formatter = new BinaryFormatter();

            
            

        }

        /// <summary>
        /// Confirms settings and updates the user controls alarm when done button is clicked
        /// </summary>
        private void Done_Click(object sender, RoutedEventArgs e)
        { 
            DateTime alarmTime;
            DateTime.TryParse(Alarm_TimePicker.Text, out alarmTime);
            int mask = 0;
            if(repeat_checkBox.IsChecked == true)
            {
                if (CheckBox_Sun.IsChecked == true) mask |= (1 << (int)DayOfWeek.Sunday);
                if (CheckBox_Mon.IsChecked == true) mask |= (1 << (int)DayOfWeek.Monday);
                if (CheckBox_Tue.IsChecked == true) mask |= (1 << (int)DayOfWeek.Tuesday);
                if (CheckBox_Wed.IsChecked == true) mask |= (1 << (int)DayOfWeek.Wednesday);
                if (CheckBox_Thu.IsChecked == true) mask |= (1 << (int)DayOfWeek.Thursday);
                if (CheckBox_Fri.IsChecked == true) mask |= (1 << (int)DayOfWeek.Friday);
                if (CheckBox_Sat.IsChecked == true) mask |= (1 << (int)DayOfWeek.Saturday);
                alarmControl.alarm.setNotificationTime(mask, alarmTime);
            }
            else
            {
                if (Alarm_DatePicker.SelectedDate == null) return;
                DateTime singleAlarmTime = (DateTime)Alarm_DatePicker.SelectedDate;
                singleAlarmTime = singleAlarmTime.Add(alarmTime.TimeOfDay);
                alarmControl.alarm.oneTimeAlarm = true;
                alarmControl.alarm.defaultAlarmTime = alarmControl.alarm.notifyTime = singleAlarmTime;
            }

            alarmControl.alarm.firstcreation = true;
            
            if (String.IsNullOrEmpty(AlarmMessage.Text) || String.IsNullOrWhiteSpace(AlarmMessage.Text))
                alarmControl.alarm.label = "Alarm";
            else
                alarmControl.alarm.label = AlarmMessage.Text;
           
            alarmControl.updateDisplay();
            Console.WriteLine((string)AlarmTone_comboBox.SelectedItem);
            alarmControl.alarm.SetSound((string)AlarmTone_comboBox.SelectedItem);
            Close();
        }

        /// <summary>
        /// Ignores any changes and closes edit window when Cancel button is clicked
        /// </summary>
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Displays the days of week for repeat when repeat check box is checked
        /// </summary>
        private void repeat_checkBox_Checked(object sender, RoutedEventArgs e)
        {
            RadioGrid.Visibility = Visibility.Visible;
            Alarm_DatePicker.Visibility = Visibility.Collapsed;
            OtherProps.Margin = new Thickness(26, 230, 21, 34);
        }

        /// <summary>
        /// Hides the days of week for repeat when repeat check box is unchecked
        /// </summary>
        private void Checkbox_Unchecked(object sender, RoutedEventArgs e)
        {

            RadioGrid.Visibility = Visibility.Collapsed;
            Alarm_DatePicker.Visibility = Visibility.Visible;
            OtherProps.Margin = new Thickness(26, 149, 21, 34);
        }

        /// <summary>
        /// Moves window with mouse when dragged
        /// </summary>
        private void Global_Mice(object sender, MouseButtonEventArgs e)
        {

            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

    }
}
